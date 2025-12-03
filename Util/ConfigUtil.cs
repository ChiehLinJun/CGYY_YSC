using CGYY_YSC.Entity;
using CGYY_YSC.Entity.MsgMapping;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace CGYY_YSC.Util
{
    public class ConfigUtil
    {
        public static ConfigEntity GetConfig()
        {
            return JsonFileReader<ConfigEntity>(System.AppDomain.CurrentDomain.BaseDirectory + "config.json");
        }

        public static DeviceInfoEntity GetDevice()
        {
            return JsonFileReader<DeviceInfoEntity>(System.AppDomain.CurrentDomain.BaseDirectory + "deviceInfo.json");
        }

        public static List<LimitVMsgCompareEntity> GetLimitVMsgCompare(string msgFIleName)
        {
            return JsonFileReader<List<LimitVMsgCompareEntity>>(System.AppDomain.CurrentDomain.BaseDirectory + $"{msgFIleName}.json");
        }

        private static T JsonFileReader<T>(string filePath)
        {

            using (StreamReader file = File.OpenText(filePath))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                return JsonConvert.DeserializeObject<T>(JToken.ReadFrom(reader).ToString());
            }
        }
    }
}
