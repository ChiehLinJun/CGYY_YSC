using CGYY_YSC.Entity;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Runtime.Versioning;

namespace CGYY_YSC.Model.ACCESS
{
    [SupportedOSPlatform("windows")]
    class BaseDB
    {
        protected static string BvmConnString;

        public static void Initialize(ConfigEntity config)
        {
            //BvmConnString = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Jet OLEDB:Database Password={1};Persist Security Info=False;", config.AccessDBJianSu.SOURCEFILE, config.AccessDBJianSu.PW);
        }

        public BaseDB()
        {
        }

        protected T? FetchSingle<T>(string connSt, string sqlText, object parameters = null) where T : BaseEntity
        {
            using (OleDbConnection connection = new OleDbConnection(connSt))
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
                    throw ex;
                }
            }
        }

        protected List<T>? FetchMultiple<T>(string connSt, string sqlText, object parameters = null) where T : BaseEntity
        {
            using (OleDbConnection connection = new OleDbConnection(connSt))
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
                    throw ex;
                }
            }
        }
    }
}