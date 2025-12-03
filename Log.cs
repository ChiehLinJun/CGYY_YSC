using System;
using System.Collections.Generic;
using System.IO;

namespace CGYY_YSC
{
    class Log
    {
        private static string DIRNAME = @"..\Log\INFO\\";
        private static Dictionary<string, string> logFiles = new Dictionary<string, string>
        {
            {"DEBUG", "_DEBUG.txt"},
            {"ERROR", "_ERROR.txt"},
            {"DBINFO", "_DBINFO.txt"},
            {"DBWARN", "_DBWARN.txt"},
            {"FINANCE", "_FINANCE.txt"}
            // Add other log files here...
        };

        public static void DebugLog(string? msg = null) => _WriteLog("DEBUG", msg, true);

        public static void ErrorLog(string? msg = null) => _WriteLog("ERROR", msg, true);

        public static void DBINFOLog(string? msg = null) => _WriteLog("DBINFO", msg, true);

        public static void DBWARNLog(string? msg = null) => _WriteLog("DBWARN", msg, true);
        
        public static void FINANCELog(string? msg = null) => _WriteLog("FINANCE", msg, true);

        

        private static void _WriteLog(string logType, string? msg = null, bool general = false)
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
