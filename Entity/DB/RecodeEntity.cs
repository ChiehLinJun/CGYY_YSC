using System;

namespace CGYY_YSC.Entity.DB
{
    class RecodeEntity : BaseEntity
    {
        public string LABNO { get; set; }

        public string INSNAME { get; set; }

        public DateTime TIMEMK { get; set; } = DateTime.Now;

        public int STATUS { get; set; } = 0;

        public string MK { get; set; }
    }
}