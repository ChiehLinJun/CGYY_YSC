using Oracle.ManagedDataAccess.Types;
using System;
using System.Security.Cryptography;

namespace CGYY_YSC.Entity.DB
{
    class MiddleTableEntity : BaseEntity
    {
        public string LABNO { get; set; }
        //检验项目
        public string LABIT { get; set; }

        public string SQNO { get; set; }

        public string SPCM { get; set; }

        public string LABSH1IT { get; set; }

        public string LABNMABV { get; set; }

        public string IPDAT { get; set; }

        public string IPTM { get; set; }

        public string IPEMPID { get; set; }

        public string IPEMPNM { get; set; }

        public string LABRESUVAL { get; set; }

        public string CHTNO { get; set; }

        public string INSNAME { get; set; }

        public string INSITEM { get; set; }

        public string INSITEM2 { get; set; }

        public string INSITEM3 { get; set; }

        public string AFDAT { get; set; }

        public string AFTM { get; set; }

        public string AFVMK { get; set; }

        public string AFAMK { get; set; }

        public string QCNAME { get; set; }

        public string QCLOT { get; set; }

        public string QCLEVEL { get; set; }

        public string QCEXP { get; set; }

        public string QCVALUE { get; set; }

        public string REGLOT { get; set; }

        public string REGBLOT { get; set; }

        public string REGEXP { get; set; }

        public string MK1 { get; set; }

        public string MK2 { get; set; }

        public string MK3 { get; set; }

        public string MK4 { get; set; }

        public DateTime LISDAT { get; set; }

        public DateTime LISVRFDAT { get; set; }

        public string LISVRFSTATUS { get; set; }

        public string HEALCOID { get; set; }

        public string HEALCOIDDEPT { get; set; }

        public string ODRDPT { get; set; }

        public string WORKBAR { get; set; }

        public string DILUIONRATE { get; set; }

        public string HemolysisValue { get; set; }

        public string HemolysisTiter { get; set; }

        public string LipemiaValue { get; set; }

        public string LipemiaTiter { get; set; }

        public string IctericValue { get; set; }

        public string? IctericTiter { get; set; }

        public string INSTIT { get; set; }

        public string QueryDay { get; set; }

        public string QueryTm { get; set; }
    }
}
