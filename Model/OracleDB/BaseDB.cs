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
        private static int _instanceCount = 0;
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
            lock (_lockObject)
            {
                _instanceCount++;
            }
        }

        protected void GetConnection(ref OracleConnection conn)
        {
            if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken)
            {
                conn.Open();
            }
        }

        protected string GenerateInsertValueString(string columnst) => Regex.Replace(":" + columnst.Trim().Replace(" ", ""), @"\b,\b", ", :");

        protected T? FetchSingle<T>(string connSt, string sqlText, object parameters = null) where T : BaseEntity
        {
            using (OracleConnection connection = new OracleConnection(connSt))
            {
                try
                {
                    connection.Open();
                    try
                    {
                        var data = connection.QueryFirst<T>(sqlText, parameters);
                        return (T)Convert.ChangeType(data, typeof(T));

                    }
                    catch (Exception e)
                    {
                        Log.DBINFOLog(string.Format("[DB][FETCH SINGLE][NOT FOUND] SqlText : {0}", sqlText));
                        Log.DBINFOLog(string.Format("[DB][FETCH SINGLE][NOT FOUND] STACK {0}", e.Message));
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    Log.DBINFOLog("[DB][FETCH SINGLE][ERROR] " + ex.Message);
                    throw;
                }
            }
        }

        protected List<T>? FetchMultiple<T>(string connSt, string sqlText, object parameters = null) where T : BaseEntity
        {
            using (OracleConnection connection = new OracleConnection(connSt))
            {
                try
                {
                    connection.Open();
                    try
                    {
                        var data = connection.Query<T>(sqlText, parameters);
                        return (List<T>)Convert.ChangeType(data, typeof(List<T>));

                    }
                    catch (Exception e)
                    {
                        Log.DBINFOLog(string.Format("[DB][FETCH MULTI][NOT FOUND] SqlText : {0}", sqlText));
                        Log.DBINFOLog(string.Format("[DB][FETCH MULTI][NOT FOUND] STACK {0}", e.Message));
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    Log.DBINFOLog("[DB][FETCH MULTI][ERROR] " + ex.Message);
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

                OracleCommand cmd = new OracleCommand(sqlText, conn);

                //ConvertEntToCmd(ref cmd, list);
                //if (content != null) cmd = ConvertEntToParam(cmd, content);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Log.DBINFOLog("[DB][EXCUTE][ERROR] " + ex.Message);
                throw;
            }
        }

        private void ConvertEntToCmd<T>(ref OracleCommand cmd, List<T> list, string column) where T : BaseEntity
        {

            foreach (BaseEntity ent in list)
            {
                cmd.Parameters.Clear();
                //command.Parameters.Add(new OracleParameter("COL1", item.Col1));
                // command.Parameters.Add(new OracleParameter("COL2", item.Col2));


                Type t = ent.GetType();
                PropertyInfo[] props = t.GetProperties();
                //var e = false;
                foreach (PropertyInfo p in props)
                {
                    var value = p.GetValue(ent, null);
                    if (Equals(p.Name, "LISVRFDAT"))
                    {
                        if (Equals(p.GetValue(ent, null), new DateTime()))
                        {
                            //e = true;
                        }
                    }
                    switch (p.PropertyType)
                    {
                        case Type intType when intType == typeof(int):
                            cmd.Parameters.Add(new OracleParameter("COL1", value));
                            // Do something for int type
                            break;
                        case Type stringType when stringType == typeof(string):
                            cmd.Parameters.Add(new OracleParameter("COL1", value));

                            // Do something for string type
                            break;
                        case Type boolType when boolType == typeof(bool):

                            // Do something for bool type
                            break;
                        case Type DateType when DateType == typeof(DateTime):
                            // Do something for bool type
                            if (!Equals(value, new DateTime()) && !ReferenceEquals(value, null))
                            {
                                //e = true;
                            }
                            break;
                        default:
                            // not in type
                            break;

                    }


                    var a = p.GetValue(ent, null);
                    if (p.PropertyType == typeof(string))
                    {
                        var b = p.GetType().ReflectedType;
                    }
                    else if (p.PropertyType == typeof(int))
                    {
                        var b = p.GetType().ReflectedType;
                    }
                    else if (p.PropertyType == typeof(DateTime))
                    {


                    }
                    var c = p.Name;
                }
                cmd.ExecuteNonQuery();
            }
        }


        protected void ExcuteMid(string sqlText, OracleParameter[] parameters)
        {
            using (OracleConnection connection = new OracleConnection(MidOrConnString))
            {
                connection.Open();
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
                            Log.DBINFOLog(ex.ToString());
                            transaction1.Rollback();
                            throw;
                        }
                    }
                }
            }
        }

        protected void ExcuteMultiMid(string sqlText, List<OracleParameter[]> list)
        {
            using (OracleConnection connection = new OracleConnection(MidOrConnString))
            {
                connection.Open();
                using (OracleTransaction transaction1 = connection.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    using (OracleCommand cmd1 = new OracleCommand(sqlText, connection))
                    {
                        try
                        {
                            cmd1.Transaction = transaction1;

                            foreach (OracleParameter[] parameters in list)
                            {
                                if (parameters != null)
                                {
                                    cmd1.Parameters.Clear();
                                    cmd1.Parameters.AddRange(parameters);
                                    cmd1.ExecuteNonQuery();
                                }
                            }

                            transaction1.Commit();
                        }
                        catch (Exception ex)
                        {
                            Log.DBINFOLog(ex.ToString());
                            transaction1.Rollback();
                            throw;
                        }
                    }
                }
            }
        }

        ~BaseDB()
        {
            lock (_lockObject)
            {
                _instanceCount--;
            }
        }
    }
}