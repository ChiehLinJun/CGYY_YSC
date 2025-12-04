using System;

namespace CGYY_YSC.Entity.DB
{
    class FinanceEntity : BaseEntity
    {
        public string ORGN { get; set; } = "G";

        public string JOBID { get; set; } = "s";

        public string DTID { get; set; } = "~";

        public string LOC { get; set; } = "3";

        public string IT { get; set; } = "70";

        public string BG { get; set; } = "*";

        public string MG { get; set; } = "*";

        public string SG { get; set; } = "~";

        public string EMPNO { get; set; } = "~";

        public string FBEL { get; set; } = "~";

        public string FID { get; set; } = "~";

        public string DPID { get; set; } = "~";

        public string NO { get; set; } = "~";

        public string SHNO { get; set; } = "~";

        public string XREM { get; set; } = "~";

        public Decimal AMT1 { get; set; }

        //public Decimal AMT2 { get; set; }

        //public Decimal AMT3 { get; set; }

        //public Decimal AMT4 { get; set; }

        public string XREM1 { get; set; } = DateTime.Now.ToString("yyyyMMdd");
    }
}