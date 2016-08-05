using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Serializer
{
    /// <summary>
    /// loads and saves the application settings into json format.
    /// </summary>
    public class JsonApplicationSettingSerializer
    {
        public JsonContent Save(ApplicationSettings applicationSettings)
        {
            var jsonString = JsonConvert.SerializeObject(
               applicationSettings,
               Formatting.Indented,
               new JsonSerializerSettings
               {
                   DefaultValueHandling = DefaultValueHandling.Ignore
               });
            return new JsonContent(jsonString);
        }

        public ApplicationSettings Load(JsonContent jsonContent)
        {
            var settings = JsonConvert.DeserializeObject<ApplicationSettings>(jsonContent.Value);
            return settings;
        }
    }
}
