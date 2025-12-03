using CGYY_YSC.Entity.DB;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace CGYY_YSC.Model.OracleDB
{
    internal class DepartmentODB : BaseDB
    {
        private string tableName = "VRMSDPTD";

        public List<DepEntity> FetchODB()
        {
            string column = "CODE, NAME";

            string sqlText = "SELECT " + column + " FROM " + tableName + " ORDER BY CODE ASC";
            var paramters = new DynamicParameters();

            List<DepEntity> list = this.FetchMultiple<DepEntity>(DpConnString, sqlText, paramters);
            return list;

        }
    }
    
}