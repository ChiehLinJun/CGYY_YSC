using CGYY_YSC.Entity;
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
