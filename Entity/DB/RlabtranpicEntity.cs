using Oracle.ManagedDataAccess.Types;
using System;
using System.Security.Cryptography;

namespace CGYY_YSC.Entity.DB
{
    class RlabtranpicEntity : BaseEntity
    {
        public string LABNO { get; set; }
        //检验项目
        public string LABIT { get; set; }

        public string SQNO { get; set; }
        
        public string IPDAT { get; set; }

        public string IPTM { get; set; }        

        public string CHTNO { get; set; }

        public string MK1 { get; set; }

        public string MK2 { get; set; }

        public string MK3 { get; set; }

        public string MK4 { get; set; }

        public DateTime LISDAT { get; set; }

        public DateTime LISVRFDAT { get; set; }

        public string LISVRFSTATUS { get; set; }        

        public byte[] PIC { get; set; }

        public string  TYPE { get; set; }

        public string FILENM { get; set; }
    }
}
