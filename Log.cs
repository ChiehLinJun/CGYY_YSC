using System;
using System.Collections.Generic;
using System.IO;

namespace CGYY_YSC
{
    class Log
    {
        private static string DIRNAME = @"..\Log\LAB_DB\\";
        private static Dictionary<string, string> logFiles = new Dictionary<string, string>
        {
            {"DEBUG", "_DEBUG.txt"},
            {"ERROR", "_ERROR.txt"},
            {"DB", "_DBINFO.txt"},
            {"SNIBE", "_SNIBEDEBUG.txt"},
            {"ROCHE", "_ROCHEDEBUG.txt"},
            {"AUTOSTAT", "_AUTOSTATDEBUG.txt"},
            {"SYSMEXP", "_SYSMEXPDEBUG.txt"},
            {"SYSMEXB", "_SYSMEXBDEBUG.txt"},
            {"MINDARY", "_MINDRAYDEBUG.txt"},
            {"NRM411", "_NRM411DEBUG.txt"},
            {"KUF20", "_KUF20DEBUG.txt"},
            {"HLCG11", "_HLCG11DEBUG.txt"},
            {"GEM3500", "_GEM3500DEBUG.txt"},
            {"BV", "_BVDEBUG.txt"},
            {"EROU", "_EROUDEBUG.txt"},
            {"BACT", "_BACTDEBUG.txt"},
            {"AIGEL", "_AIGEL.txt"},
            {"UDBIO", "_UDBIO.txt"},
            {"GETIEN", "_GETIEN.txt"}
            // Add other log files here...
        };

        public static void DebugLog(string msg = null) => _WriteLog("DEBUG", msg, true);

        public static void ErrorLog(string msg = null) => _WriteLog("ERROR", msg, true);

        public static void DBINFOLog(string msg = null) => _WriteLog("DB", msg, true);

        public static void SnibeLog(string msg = null) => _WriteLog("SNIBE", msg);

        public static void AutoStatLog(string msg = null) => _WriteLog("AUTOSTAT", msg);

        public static void RocheLog(string msg = null) => _WriteLog("ROCHE", msg);

        public static void SysmexPLog(string msg = null) => _WriteLog("SYSMEXP", msg);

        public static void SysmexBLog(string msg = null) => _WriteLog("SYSMEXB", msg);

        public static void MindrayLog(string msg = null) => _WriteLog("MINDARY", msg);

        public static void NRM411Log(string msg = null) => _WriteLog("NRM411", msg);

        public static void Kuf20Log(string msg = null) => _WriteLog("KUF20", msg);

        public static void HLCG11Log(string msg = null) => _WriteLog("HLCG11", msg);

        public static void GEM3500Log(string msg = null) => _WriteLog("GEM3500", msg);

        public static void BVLog(string msg = null) => _WriteLog("BV", msg);

        public static void EROULog(string msg = null) => _WriteLog("EROU", msg);

        public static void BACTLog(string msg = null) => _WriteLog("BACT", msg);

        public static void AIGELLog(string msg = null) => _WriteLog("AIGEL", msg);

        public static void UDBIOLog(string msg = null) => _WriteLog("UDBIO", msg);

        public static void GETIENLog(string msg = null) => _WriteLog("GETIEN", msg);

        private static void _WriteLog(string logType, string msg = null, bool general = false)
        {
            try
            {
                string fileName = logFiles[logType];
                string DateString = DateTime.Now.ToString("yyyyMMdd");
                string path = general ? DIRNAME : Path.Combine(DIRNAME, logType);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                string filePath = Path.Combine(path, $"{DateString}{fileName}");

                string logText = $"[{logType}_LOG]\n[DATE] : {DateTime.Now.ToLongDateString()} {DateTime.Now.TimeOfDay}\n[MSG] : {msg} \n";
                File.AppendAllText(filePath, logText);

                /**
                if (!File.Exists(filePath)) File.Create(filePath).Close();

                FileStream fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.Write);
                using (TextWriter tw = new StreamWriter(fileStream))
                {
                    tw.WriteLine($"[{logName}_LOG]");
                    tw.WriteLine($"[DATE] : {DateTime.Now.ToLongDateString()} {DateTime.Now.TimeOfDay.ToString()}");
                    tw.WriteLine($"[MSG] : {msg} \r\n");
                    tw.Close();
                }
                **/
            }
            catch (UnauthorizedAccessException ex)
            {
                _LogError(ex);
            }
            catch (IOException ex)
            {
                _LogError(ex);
            }
        }

        private static void _LogError(Exception ex)
        {
            try
            {
                ErrorLog(ex.Message);
            }
            catch (Exception e)
            {
                // Consider logging this error as well
            }
        }
    }
}
