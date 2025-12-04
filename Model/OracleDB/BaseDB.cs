using CGYY_YSC.Entity;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CGYY_YSC.Model.OracleDB
{
    public class BaseDB
    {
        private static object _lockObject = new object();

        protected static string MidOrConnString;
        protected static string LisOrConnString;
        protected static string DpConnString;

        public static void Initialize(ConfigEntity config)
        {
            lock (_lockObject)
            {
                MidOrConnString = string.Format("Data Source={0};Persist Security Info=True;User ID={1};Password={2};Connection Timeout=20;", config.DatabaseMid.SOURCE, config.DatabaseMid.USER, config.DatabaseMid.PW);
                LisOrConnString = string.Format("Data Source={0};Persist Security Info=True;User ID={1};Password={2};Connection Timeout=20;", config.DatabaseLis.SOURCE, config.DatabaseLis.USER, config.DatabaseLis.PW);
                DpConnString = LisOrConnString;
            }
        }

        public BaseDB()
        {
        }

        protected void GetConnection(ref OracleConnection conn)
        {
            if (conn == null)
            {
                throw new ArgumentNullException(nameof(conn));
            }

            if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken)
            {
                conn.Open();
            }
        }

        protected string GenerateInsertValueString(string columnst)
        {
            if (string.IsNullOrWhiteSpace(columnst))
            {
                return string.Empty;
            }

            var rawParts = columnst.Split(',');
            var parts = new List<string>(rawParts.Length);

            foreach (var raw in rawParts)
            {
                var name = raw.Trim();
                if (string.IsNullOrEmpty(name))
                {
                    continue;
                }

                parts.Add(":" + name);
            }

            return string.Join(", ", parts);
        }

        protected OracleConnection CreateConnection(string connString)
        {
            if (string.IsNullOrWhiteSpace(connString))
            {
                throw new ArgumentException("Connection string cannot be null or empty.", nameof(connString));
            }

            var connection = new OracleConnection(connString);
            connection.Open();
            return connection;
        }

        protected OracleConnection CreateMidConnection()
        {
            return CreateConnection(MidOrConnString);
        }

        protected OracleConnection CreateLisConnection()
        {
            return CreateConnection(LisOrConnString);
        }

        protected OracleConnection CreateDpConnection()
        {
            return CreateConnection(DpConnString);
        }

        protected T? FetchSingle<T>(string connSt, string sqlText, object parameters = null) where T : BaseEntity
        {
            using (OracleConnection connection = CreateConnection(connSt))
            {
                try
                {
                    try
                    {
                        var data = connection.QueryFirstOrDefault<T>(sqlText, parameters);

                        if (data == null)
                        {
                            Log.DBINFOLog(string.Format("[DB][FETCH_SINGLE][NOT_FOUND] SqlText: {0}", sqlText));
                            return null;
                        }

                        return data;

                    }
                    catch (Exception e)
                    {
                        Log.DBWARNLog(string.Format("[DB][FETCH_SINGLE][EXCEPTION] SqlText: {0}", sqlText));
                        Log.DBWARNLog(string.Format("[DB][FETCH_SINGLE][EXCEPTION_STACK] {0}", e.Message));
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    Log.DBWARNLog(string.Format("[DB][FETCH_SINGLE][ERROR] {0}", ex.Message));
                    throw;
                }
            }
        }

        protected List<T>? FetchMultiple<T>(string connSt, string sqlText, object parameters = null) where T : BaseEntity
        {
            using (OracleConnection connection = CreateConnection(connSt))
            {
                try
                {
                    try
                    {
                        var data = connection.Query<T>(sqlText, parameters);
                        return data?.AsList();

                    }
                    catch (Exception e)
                    {
                        Log.DBWARNLog(string.Format("[DB][FETCH_MULTI][EXCEPTION] SqlText: {0}", sqlText));
                        Log.DBWARNLog(string.Format("[DB][FETCH_MULTI][EXCEPTION_STACK] {0}", e.Message));
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    Log.DBWARNLog(string.Format("[DB][FETCH_MULTI][ERROR] {0}", ex.Message));
                    throw;
                }
            }
        }

        //Excute with ent
        protected void Excute<T>(OracleConnection conn, string sqlText, List<T> list = null) where T : BaseEntity
        {
            try
            {
                //Dictionary<string, object> content = null;
                //if (ent != null) content = DicFromEnt(ent);

                GetConnection(ref conn);

                using (OracleCommand cmd = new OracleCommand(sqlText, conn))
                {
                    //if (content != null) cmd = ConvertEntToParam(cmd, content);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Log.DBWARNLog(string.Format("[DB][EXECUTE][ERROR] {0}", ex.Message));
                throw;
            }
        }

        protected void ExcuteMid(string sqlText, OracleParameter[] parameters)
        {
            using (OracleConnection connection = CreateMidConnection())
            {
                using (OracleTransaction transaction1 = connection.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    using (OracleCommand cmd1 = new OracleCommand(sqlText, connection))
                    {
                        try
                        {
                            cmd1.Transaction = transaction1;

                            if (parameters != null)
                            {
                                cmd1.Parameters.Clear();
                                cmd1.Parameters.AddRange(parameters);
                            }
                            cmd1.ExecuteNonQuery();
                            transaction1.Commit();
                        }
                        catch (Exception ex)
                        {
                            Log.DBWARNLog(string.Format("[DB][EXECUTE_MID][EXCEPTION] {0}", ex));
                            transaction1.Rollback();
                            throw;
                        }
                    }
                }
            }
        }

        protected void ExcuteMultiMid(string sqlText, List<OracleParameter[]> list)
        {
            using (OracleConnection connection = CreateMidConnection())
            {
                using (OracleTransaction transaction1 = connection.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    using (OracleCommand cmd1 = new OracleCommand(sqlText, connection))
                    {
                        try
                        {
                            Log.DBINFOLog("DB ING");
                            cmd1.Transaction = transaction1;

                            foreach (OracleParameter[] parameters in list)
                            {
                                if (parameters != null)
                                {
                                    cmd1.Parameters.Clear();
                                    cmd1.Parameters.AddRange(parameters);
                                    Log.DBINFOLog("DB Excute");
                                    cmd1.ExecuteNonQuery();
                                    Log.DBINFOLog("DB Excute DONE");
                                }
                            }
                            Log.DBINFOLog("DB COMMIT");
                            transaction1.Commit();
                            Log.DBINFOLog("COMMIT DONE");
                        }
                        catch (Exception ex)
                        {
                            Log.DBWARNLog(string.Format("[DB][EXECUTE_MULTI_MID][EXCEPTION] {0}", ex));
                            transaction1.Rollback();
                            throw;
                        }
                    }
                }
            }
        }
    }
}