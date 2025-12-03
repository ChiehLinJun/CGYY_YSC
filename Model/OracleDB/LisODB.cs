using CGYY_YSC.Entity.DB;
using Dapper;
using System;
using System.Collections.Generic;

namespace CGYY_YSC.Model.OracleDB
{
    internal class LisODB : BaseDB
    {
        private string tableName = "LAB_WORK_LIST_VIEW";

        public List<LisEntity> GetInstitLisODB(string labno, string instid)
        {
            string column = "DISTINCT(INSTIT)";
            string condition = string.Format("LABNO = :LABNO AND INSTID = :INSTID ", labno, instid);
            var paramters = new DynamicParameters();
            paramters.Add("LABNO", labno);
            paramters.Add("INSTID", instid);

            string sqlText = string.Format("SELECT {0} FROM {1} WHERE {2}", column, tableName, condition);

            List<LisEntity> list = this.FetchMultiple<LisEntity>(LisOrConnString, sqlText, paramters);
            return list;
        }
        /*
        string time = entity.cltdat.ToString("yyyyMMddHHmm").Trim();
        string writeFile = entity.labno + '|' + entity.instit + '|' + entity.chtno + '|';
        writeFile += entity.cnm + '|' + entity.sex + '|' + age + '|' + entity.spcm + '|' + time;
        */

        public LisEntity? GetByLabnoAndInstid(string labno, string instid)
        {
            string column = "LABNO, INSTIT, CHTNO, CNM, SEX, BRNDAT, SPCM, INSTID,  TO_DATE(CLTDAT, 'yyyyMMdd') as CLTDAT, TO_DATE(CLTTM, 'hhmi') as CLTTM";
            string condition = "LABNO = :LABNO AND INSTID = :INSTID";
            var paramters = new DynamicParameters();
            paramters.Add("LABNO", labno);
            paramters.Add("INSTID", instid);
            string sqlText = string.Format("SELECT {0} FROM {1} WHERE {2}", column, tableName, condition);

            return this.FetchSingle<LisEntity>(LisOrConnString, sqlText, paramters);
        }

        //有group by 谨慎使用
        public LisEntity? GetByTitTidAndLabno(string? instit, string? instid, string? labno, bool ndGroupBy = false)
        {
            string column = "LABIT, SQNO, SPCM, LABSH1IT, LABNMABV, LABNO, CHTNO, INSTID";
            string condition = string.Empty;
            string gpst = string.Empty;

            var paramters = new DynamicParameters();
            if (!ReferenceEquals(instit, null))
            {
                condition += "INSTIT = :INSTIT";
                paramters.Add("INSTIT", instit);
            }
            if (!ReferenceEquals(instid, null))
            {
                if (!Equals(condition, string.Empty)) condition += " AND ";
                condition += "INSTID = :INSTID";
                paramters.Add("INSTID", instid);
            }

            if (!ReferenceEquals(labno, null))
            {
                if (!Equals(condition, string.Empty)) condition += " AND ";
                condition += "LABNO = :LABNO";
                paramters.Add("LABNO", labno);
            }

            if (ndGroupBy) gpst = string.Format("GROUP BY {0}", column);

            string sqlText = string.Format("SELECT {0} FROM {1} WHERE 1=1 AND {2} {3}", column, tableName, condition, gpst);
            LisEntity? lisEntity = this.FetchSingle<LisEntity>(LisOrConnString, sqlText, paramters);

            return lisEntity;
        }
        /*
        public LisEntity GetLisData(string labno, string insname, string instit)
        {
            var paramters = new DynamicParameters();

            string condition = "INSTID = :INSTID AND  INSTIT = :INSTIT";
            paramters.Add("INSTID", insname);
            paramters.Add("INSTIT", instit);

            if (labno != null)
            {
                condition += " ADN LABNO = :LABNO ";
                paramters.Add("LABNO", labno);
            }

            string column = "LABIT, SQNO, SPCM, LABSH1IT, LABNMABV, CHTNO";

            string sqlText = string.Format("SELECT {0} FROM {1} WHERE {2}", column, tableName, condition);

            LisEntity entity = this.fetchSingle<LisEntity>(lisConn, sqlText);

            return entity;
        }
        */

        public List<LisEntity>? FetchODBByLabnoAndinsname(string labno, string insname)
        {
            String colunm = "LABNO, BRNDAT, SEX, CNM, CHTNO, INSTIT, PTSOUR";
            string condition = "INSTID = :INSTID AND LABNO = :LABNO";

            var paramters = new DynamicParameters();
            paramters.Add("INSTID", insname);
            paramters.Add("LABNO", labno);

            string sqlText = string.Format("SELECT {0} FROM {1} WHERE {2}", colunm, tableName, condition);

            List<LisEntity>? list = FetchMultiple<LisEntity>(LisOrConnString, sqlText, paramters);

            return list;
        }

        /**
        public List<LisEntity> MultiFetchByLabnoAndInsnameAndGp(string labnoSt, string insnameSt)
        {
            List<LisEntity> list = null;
            if (string.IsNullOrEmpty(labnoSt) && string.IsNullOrEmpty(insnameSt)) return list;

            var paramters = new DynamicParameters();

            string c1 = string.Empty;
            string c2 = string.Empty;
            if (!Equals(labnoSt, "")) c1 = string.Format("LABNO IN ('{0}')", labnoSt.Replace(",", "','"));
            if (!Equals(insnameSt, ""))
            {
                c2 += Equals(c1, "") ? "" : " AND ";
                c2 += string.Format("INSTID IN ('{0}')", insnameSt.Replace(",", "','"));
            }

            string condition = string.Format("{0}{1} ", c1, c2);

            string sqlText = string.Format("SELECT LABNO, LABIT FROM {0}  WHERE {1} ORDER BY LABNO ASC, LABIT ASC", tableName, condition);

            list = this.fetchMultiple<LisEntity>(lisConn, sqlText);

            return list;
        }
        **/

        public List<LisEntity>? MultiFetchByLabnoAndInsnameAndGpNew(List<string> labnoSt, List<string> insnameSt, bool likeFlag = false)
        {
            if (Equals(labnoSt.Count, 0) && Equals(insnameSt.Count, 0)) return null;

            var paramters = new DynamicParameters();

            string c1 = string.Empty;
            string c2 = string.Empty;
            if (!Equals(labnoSt.Count, 0))
            {
                c1 = "LABNO IN :LABNO";
                paramters.Add("LABNO", labnoSt);
            }

            if (!Equals(insnameSt.Count, 0))
            {
                c2 = Equals(c1, "") ? "" : " AND ";

                if (likeFlag == true)
                {
                    c2 += "INSTID LIKE :INSTID";
                    paramters.Add("INSTID", string.Format("{0}%", insnameSt[0]));
                }
                else
                {
                    c2 += " INSTID IN :INSTID";
                    paramters.Add("INSTID", insnameSt);
                }

            }


            string column = " LABNO, LABIT, SQNO, JBSHNO, SPCM, LABSH1IT, CHTNO, CNM, SEX, BRNDAT, LABNMABV, INSTIT ,PTSOUR";
            string condition = string.Format("{0}{1} ", c1, c2);

            string sqlText = string.Format("SELECT {0} FROM {1}  WHERE {2} ORDER BY LABNO ASC, LABIT ASC", column, tableName, condition);

            List<LisEntity>? list = this.FetchMultiple<LisEntity>(LisOrConnString, sqlText, paramters);

            return list;
        }


        public List<LisEntity>? FetchODB(string? labno, string instid)
        {
            string column = "LABNO, LABIT, SQNO, JBSHNO, SPCM, LABSH1IT, CHTNO, CNM, SEX, BRNDAT, LABNMABV, INSTIT, TO_DATE(CONCAT(CLTDAT,CLTTM),'yyyyMMdd hh24:mi') cltdat";

            string condition = string.Empty;

            var paramters = new DynamicParameters();
            if (labno != null)
            {
                condition = "LABNO = : LABNO AND INSTID LIKE :INSTID ";
                paramters.Add(":LABNO", labno);
                paramters.Add(":INSTID", string.Format("{0}%", instid));
            }
            else
            {
                condition = "INSTID = :INSTID";
                paramters.Add(":INSTID", instid);
            }

            string sqlText = String.Format("SELECT DISTINCT {0} FROM {1} WHERE {2} ORDER BY LABSH1IT ASC", column, tableName, condition);

            List<LisEntity> result = this.FetchMultiple<LisEntity>(LisOrConnString, sqlText, paramters);

            return result;

        }
    }
}