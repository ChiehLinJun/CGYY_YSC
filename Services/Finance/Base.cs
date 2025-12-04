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
            Log.FINANCELog($"[DB][test start] ");
            var list = new List<FinanceEntity>
            {
                new FinanceEntity { IT = "70", EMPNO = "BA0T-1201", AMT1 = 8900, XREM1 = "20251201" },
                new FinanceEntity { IT = "70", EMPNO = "BA0T-1201", AMT1 = 10000, XREM1 = "20251201" },
                new FinanceEntity { IT = "75", AMT1 = 300, XREM1 = "20251202" },
                new FinanceEntity { IT = "69", AMT1 = 8600, XREM1 = "20251202" },
                new FinanceEntity { IT = "71", EMPNO = "*", AMT1 = 300, XREM1 = "20251202" },
                new FinanceEntity { IT = "72", EMPNO = "BA0T-1203", AMT1 = 300, XREM1 = "20251203" },
                new FinanceEntity { IT = "68", EMPNO = "BA0T-1204", AMT1 = 200, XREM1 = "20251204" },
                new FinanceEntity { IT = "70", EMPNO = "BA0T-1204", AMT1 = -400, XREM1 = "20251204" }
            };

            FinanceODB fo = new FinanceODB();
            fo.DataInsert(list);

            Log.FINANCELog($"[DB][InsertDone] ");
        }

        public void DisplayInstidName()
        {
            string msg = "No match";
        }
    }
}