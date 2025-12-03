namespace CGYY_YSC.Entity
{
    public class DeviceInfoEntity
    {
        public DactResulstSetting dactResulstSetting { get; set; }

        public class DactResulstSetting
        {
            public string _72_607_Negative_Info { get; set; }
            public string _72_603_Negative_Info { get; set; }
            public string _Dact_Positive_Info { get; set; }
        }
    }
}
