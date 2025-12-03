using CGYY_YSC.Entity.DB;
using CGYY_YSC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using CGYY_YSC.Model.OracleDB;
using Microsoft.Extensions.Options;

namespace CGYY_YSC.Services.Finance
{
    class FinanceBase
    {
        private readonly ConfigEntity _config;

        public FinanceBase()
        {
        }

        public void InsertFinance()
        {
            var fent = new FinanceEntity
            {
                AMT1 = 50
            };

            FinanceODB fo = new FinanceODB();
            fo.DataInsert(new List<FinanceEntity> { fent });

            Log.DBINFOLog($"[DB][InsertDone] ");
        }

        public void DisplayInstidName()
        {
            string msg = "No match";
        }
    }
}