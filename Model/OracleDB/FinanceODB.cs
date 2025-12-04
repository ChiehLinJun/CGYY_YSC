using CGYY_YSC.Entity.DB;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace CGYY_YSC.Model.OracleDB
{
    internal class FinanceODB : BaseDB
    {
        private string tableName = "REXS0000K";

        private const string Columns =
            "ORGN, JOBID, DTID, LOC, IT, " +
            "BG, MG, SG, EMPNO, FBEL, " +
            "FID, DPID, NO, SHNO, XREM, " +
            "AMT1, XREM1";

        private OracleParameter[] CreateParameters(FinanceEntity financeEntity)
        {
            return new OracleParameter[]
            {
                new OracleParameter(":ORGN", OracleDbType.Varchar2, financeEntity.ORGN, ParameterDirection.Input),
                new OracleParameter(":JOBID", OracleDbType.Varchar2, financeEntity.JOBID, ParameterDirection.Input),
                new OracleParameter(":DTID", OracleDbType.Varchar2, financeEntity.DTID, ParameterDirection.Input),
                new OracleParameter(":LOC", OracleDbType.Varchar2, financeEntity.LOC, ParameterDirection.Input),
                new OracleParameter(":IT", OracleDbType.Varchar2, financeEntity.IT, ParameterDirection.Input),

                new OracleParameter(":BG", OracleDbType.Varchar2, financeEntity.BG, ParameterDirection.Input),
                new OracleParameter(":MG", OracleDbType.Varchar2, financeEntity.MG, ParameterDirection.Input),
                new OracleParameter(":SG", OracleDbType.Varchar2, financeEntity.SG, ParameterDirection.Input),
                new OracleParameter(":EMPNO", OracleDbType.Varchar2, financeEntity.EMPNO, ParameterDirection.Input),
                new OracleParameter(":FBEL", OracleDbType.Varchar2, financeEntity.FBEL, ParameterDirection.Input),

                new OracleParameter(":FID", OracleDbType.Varchar2, financeEntity.FID, ParameterDirection.Input),
                new OracleParameter(":DPID", OracleDbType.Varchar2, financeEntity.DPID, ParameterDirection.Input),
                new OracleParameter(":NO", OracleDbType.Varchar2, financeEntity.NO, ParameterDirection.Input),
                new OracleParameter(":SHNO", OracleDbType.Varchar2, financeEntity.SHNO, ParameterDirection.Input),
                new OracleParameter(":XREM", OracleDbType.Varchar2, financeEntity.XREM, ParameterDirection.Input),

                new OracleParameter(":AMT1", OracleDbType.Decimal, financeEntity.AMT1, ParameterDirection.Input),
                //new OracleParameter(":AMT2", OracleDbType.Decimal, financeEntity.AMT2, ParameterDirection.Input),
                //new OracleParameter(":AMT3", OracleDbType.Decimal, financeEntity.AMT3, ParameterDirection.Input),
                //new OracleParameter(":AMT4", OracleDbType.Decimal, financeEntity.AMT4, ParameterDirection.Input),

                new OracleParameter(":XREM1", OracleDbType.Varchar2, financeEntity.XREM1, ParameterDirection.Input)
            };
        }

        public void DataInsert(List<FinanceEntity> list)
        {
            string sqlText = string.Format("INSERT INTO {0} ({1}) VALUES ({2})", tableName, Columns, GenerateInsertValueString(Columns));
            Log.DBINFOLog(sqlText);
            List<OracleParameter[]> listpa = new List<OracleParameter[]>();

            foreach (FinanceEntity financeEntity in list)
            {
                OracleParameter[] parameters = CreateParameters(financeEntity);
                listpa.Add(parameters);
            }
            Log.DBINFOLog("ENT LIST BUILD");
            ExcuteMultiMid(sqlText, listpa);
        }        

    }
}