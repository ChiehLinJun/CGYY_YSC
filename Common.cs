namespace CGYY_YSC
{
    class Common
    {
        public const string APPNAME = "CGYY_YSC.EXE";
        //column
        public const string TXTLENGTH = "LEN";
        public const string COMMAND = "CMD";
        public const string STATUS = "STU";
        public const string MESSAGEID = "MID";
        public const string REGISTER = "REG";
        public const string DEVICE = "DEV";
        public const string HEARTBEAT = "QHB";

        //error status
        public const string MESSAGESUCC = "ACK";
        public const string MESSAGEFAIL = "NAK";

        //ascii
        public const char STARTTAG = (char)(byte)1;
        public const char ANDTAG = (char)(byte)30;
        public const char ENDTAG = (char)(byte)4;
        //ascii hex
        public const string HEXSTARTTAG = "01";
        public const string HEXANDTAG = "1E";
        public const string HEXENDTAG = "04";

        //schedule timmer
        public const string WORKTIMEPER2SEC = "0/2 * *  * * ?";  //每天2秒刷新一次
        public const string WORKTIMEPER30SEC = "0/30 * *  * * ?";  //每天30秒刷新一次
        public const string RUNONCE = "0 45 1  * * ?";              //每天1:45刷新一次
        public const string WORKTIMEPERHOUR = "0 0 7-17 * * ?";     //每天7-6点一小时刷新一次

    }
}
