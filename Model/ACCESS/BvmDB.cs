using CGYY_YSC.Entity.ACCESS;
using CGYY_YSC.Util;
using System.Collections.Generic;

namespace CGYY_YSC.Model.ACCESS
{
    class BvmDB : BaseDB
    {

        private string tableName = "病人病历";

        public JianSuEntity GetMdb()
        {
            List<string> li = GeneralUtil.GetListWithEntityMeth<JianSuEntity>();

            string column = string.Empty;

            foreach (string name in li)
            {
                if (!Equals(column, string.Empty)) column += ", ";
                column += name;
            }
            if (Equals(column, string.Empty)) column += "*";


            string sqlText = string.Format("SELECT {0} FROM {1} order by 病历号 asc", column, tableName);

            JianSuEntity list = this.FetchSingle<JianSuEntity>(BvmConnString, sqlText);

            return list;
        }

        public List<JianSuEntity> GetNewextMdbByLano(string labno)
        {
            List<string> li = GeneralUtil.GetListWithEntityMeth<JianSuEntity>();
            string column = string.Empty;

            foreach (string name in li)
            {
                if (!Equals(column, string.Empty)) column += ", ";
                column += name;
            }
            if (Equals(column, string.Empty)) column += "*";

            string condition = string.Empty;
            if (!string.IsNullOrEmpty(labno)) condition = string.Format(" AND 标本号 > '{0}' ", labno);

            string sqlText = string.Format("SELECT {0} FROM {1}  WHERE (标本号 IS NOT NULL AND 标本号 <> '')  {2} order by 标本号 ASC", column, tableName, condition);

            List<JianSuEntity> list = this.FetchMultiple<JianSuEntity>(BvmConnString, sqlText);

            return list;
        }


    }
}