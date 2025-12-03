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

        public void DataInsert(List<FinanceEntity> list)
        {
            var column = "ORGN, JOBID, DTID, LOC, IT, " +
                "BG, MG, SG, EMPNO, FBEL, " +
                "FID, DPID, NO, SHNO, XREM, " +
                "AMT1, XREM1, " ;

            string sqlText = string.Format("INSERT INTO {0} ({1}) VALUES ({2})", tableName, column, GenerateInsertValueString(column));

            List<OracleParameter[]> listpa = new List<OracleParameter[]>();

            foreach (FinanceEntity FinanceEntity in list)
            {
                OracleParameter[] parameters = new OracleParameter[] {
                             new OracleParameter(":ORGN", OracleDbType.Varchar2, FinanceEntity.ORGN, ParameterDirection.Input),
                             new OracleParameter(":JOBID", OracleDbType.Varchar2, FinanceEntity.JOBID, ParameterDirection.Input),
                             new OracleParameter(":DTID", OracleDbType.Varchar2, FinanceEntity.DTID, ParameterDirection.Input),
                             new OracleParameter(":LOC", OracleDbType.Varchar2, FinanceEntity.LOC, ParameterDirection.Input),
                             new OracleParameter(":IT", OracleDbType.Varchar2, FinanceEntity.IT, ParameterDirection.Input),

                             new OracleParameter(":BG", OracleDbType.Varchar2, FinanceEntity.BG, ParameterDirection.Input),
                             new OracleParameter(":MG", OracleDbType.Varchar2, FinanceEntity.MG, ParameterDirection.Input),
                              new OracleParameter(":SG", OracleDbType.Varchar2, FinanceEntity.SG, ParameterDirection.Input),
                             new OracleParameter(":EMPNO", OracleDbType.Varchar2, FinanceEntity.EMPNO, ParameterDirection.Input),
                             new OracleParameter(":FBEL" ,OracleDbType.Varchar2, FinanceEntity.FBEL, ParameterDirection.Input),

                             new OracleParameter(":FID", OracleDbType.Varchar2, FinanceEntity.FID, ParameterDirection.Input),
                             new OracleParameter(":DPID", OracleDbType.Varchar2, FinanceEntity.DPID, ParameterDirection.Input),
                             new OracleParameter(":NO", OracleDbType.Varchar2, FinanceEntity.NO, ParameterDirection.Input),
                             new OracleParameter(":SHNO", OracleDbType.Varchar2, FinanceEntity.SHNO, ParameterDirection.Input),
                             new OracleParameter(":XREM", OracleDbType.Varchar2, FinanceEntity.XREM, ParameterDirection.Input),

                             new OracleParameter(":AMT1", OracleDbType.Decimal, FinanceEntity.AMT1, ParameterDirection.Input),
                             //new OracleParameter(":AMT2", OracleDbType.Decimal, FinanceEntity.AMT2, ParameterDirection.Input),
                             //new OracleParameter(":AMT3", OracleDbType.Decimal, FinanceEntity.AMT3, ParameterDirection.Input),
                             //new OracleParameter(":AMT4", OracleDbType.Decimal, FinanceEntity.AMT4, ParameterDirection.Input),

                             new OracleParameter(":XREM1", OracleDbType.Varchar2, FinanceEntity.XREM1, ParameterDirection.Input)
                };
                listpa.Add(parameters);
            }
            ExcuteMultiMid(sqlText, listpa);
        }        

        public List<MiddleTableEntity> DataConfirmation(string labno, string ipempnm)
        {
            String colunm = "LABNO, LABRESUVAL";
            string condition = "IPEMPNM = :IPEMPNM AND LABNO = :LABNO";

            var paramters = new DynamicParameters();
            paramters.Add("IPEMPNM", ipempnm);
            paramters.Add("LABNO", labno);

            string sqlText = string.Format("SELECT {0} FROM {1} WHERE {2}", colunm, tableName, condition);

            List<MiddleTableEntity> list = FetchMultiple<MiddleTableEntity>(MidOrConnString, sqlText, paramters);

            return list;
        }

        public void DataUpSert(List<MiddleTableEntity> list)
        {
            var column = "LABNO, LABIT, SQNO, SPCM, LABSH1IT, " +
                "AFDAT, AFTM, CHTNO, LABNMABV, IPDAT, " +
                "IPTM, IPEMPID, IPEMPNM, LABRESUVAL, INSNAME, " +
                "INSITEM, INSITEM2, AFAMK, REGLOT, REGBLOT, " +
                "REGEXP, MK1, QCNAME, QCLOT, QCLEVEL ";

            string onText = " (LABNO = :LABNO " +
                "AND LABIT = :LABIT " +
                "AND SQNO = :SQNO " +
                "AND LABSH1IT = :LABSH1IT " +
                "AND AFDAT = :AFDAT" +
                "AND AFTM = :AFTM)";

            string insertText = string.Format("INSERT ({0}) VALUES ({1})", column, GenerateInsertValueString(column));
            string sqlText = string.Format("MERGE INTO {0} USING dual ON {1} WHEN NOT MATCHED THEN {2}", tableName, onText, insertText);

            List<OracleParameter[]> listpa = new List<OracleParameter[]>();

            foreach (MiddleTableEntity middleTableEntity in list)
            {
                OracleParameter[] parameters = new OracleParameter[] {
                             new OracleParameter(":LABNO", OracleDbType.Varchar2, middleTableEntity.LABNO, ParameterDirection.Input),
                             new OracleParameter(":LABIT", OracleDbType.Varchar2, middleTableEntity.LABIT, ParameterDirection.Input),
                             new OracleParameter(":SQNO", OracleDbType.Varchar2, middleTableEntity.SQNO, ParameterDirection.Input),
                             new OracleParameter(":SPCM", OracleDbType.Varchar2, middleTableEntity.SPCM, ParameterDirection.Input),
                             new OracleParameter(":LABSH1IT", OracleDbType.Varchar2, middleTableEntity.LABSH1IT, ParameterDirection.Input),

                             new OracleParameter(":AFDAT", OracleDbType.Varchar2, middleTableEntity.AFDAT, ParameterDirection.Input),
                             new OracleParameter(":AFTM", OracleDbType.Varchar2, middleTableEntity.AFTM, ParameterDirection.Input),
                              new OracleParameter(":CHTNO", OracleDbType.Varchar2, middleTableEntity.CHTNO, ParameterDirection.Input),
                             new OracleParameter(":LABNMABV", OracleDbType.Varchar2, middleTableEntity.LABNMABV, ParameterDirection.Input),
                             new OracleParameter(":IPDAT" ,OracleDbType.Varchar2, middleTableEntity.IPDAT, ParameterDirection.Input),

                             new OracleParameter(":IPTM", OracleDbType.Varchar2, middleTableEntity.IPTM, ParameterDirection.Input),
                             new OracleParameter(":IPEMPID", OracleDbType.Varchar2, middleTableEntity.IPEMPID, ParameterDirection.Input),
                             new OracleParameter(":IPEMPNM", OracleDbType.Varchar2, middleTableEntity.IPEMPNM, ParameterDirection.Input),
                             new OracleParameter(":LABRESUVAL", OracleDbType.Varchar2, middleTableEntity.LABRESUVAL, ParameterDirection.Input),
                             new OracleParameter(":INSNAME", OracleDbType.Varchar2, middleTableEntity.INSNAME, ParameterDirection.Input),

                             new OracleParameter(":INSITEM", OracleDbType.Varchar2, middleTableEntity.INSITEM, ParameterDirection.Input),
                             new OracleParameter(":INSITEM2", OracleDbType.Varchar2, middleTableEntity.INSITEM2, ParameterDirection.Input),
                             new OracleParameter(":AFAMK", OracleDbType.Varchar2, middleTableEntity.AFAMK, ParameterDirection.Input),
                             new OracleParameter(":REGLOT", OracleDbType.Varchar2, middleTableEntity.REGLOT, ParameterDirection.Input),
                             new OracleParameter(":REGBLOT", OracleDbType.Varchar2, middleTableEntity.REGBLOT, ParameterDirection.Input),

                             new OracleParameter(":REGEXP", OracleDbType.Varchar2, middleTableEntity.REGEXP, ParameterDirection.Input),
                             new OracleParameter(":MK1", OracleDbType.Varchar2, middleTableEntity.MK1, ParameterDirection.Input),
                             new OracleParameter(":QCNAME", OracleDbType.Varchar2, middleTableEntity.QCNAME, ParameterDirection.Input),
                             new OracleParameter(":QCLOT", OracleDbType.Varchar2, middleTableEntity.QCLOT, ParameterDirection.Input),
                             new OracleParameter(":QCLEVEL", OracleDbType.Varchar2, middleTableEntity.QCLEVEL, ParameterDirection.Input)
                };
                listpa.Add(parameters);
            }
            ExcuteMultiMid(sqlText, listpa);
        }

        public void DeleteDataByTime(string dtTime)
        {
            string sqlText = string.Format("DELETE FROM {0} WHERE IPDAT < {1} ", tableName, dtTime);
            this.ExcuteMid(sqlText, null);
        }
    }
}