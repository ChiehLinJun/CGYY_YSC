namespace CGYY_YSC.Entity
{
    public class ConfigEntity
    {
        public DatabaseLis DatabaseLis { get; set; }
        public DatabaseMid DatabaseMid { get; set; }
        public AccessDBJianSu AccessDBJianSu { get; set; }
    }

    public class DatabaseLis
    {
        public string SOURCE { get; set; }
        public string USER { get; set; }
        public string PW { get; set; }
    }
    public class DatabaseMid
    {
        public string SOURCE { get; set; }
        public string USER { get; set; }
        public string PW { get; set; }
    }

    public class AccessDBJianSu
    {
        public string SOURCEFILE { get; set; }
        public string PW { get; set; }
    }

}
