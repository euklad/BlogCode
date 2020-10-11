using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoCatleProxy.RestartableFlow
{
    public static class ObjectExtension
    {
        public static T CloneObject<T>(this T source)
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
                TypeNameHandling = TypeNameHandling.Objects
            };

            var json = JsonConvert.SerializeObject(source, jsonSerializerSettings);
            var result = JsonConvert.DeserializeObject<T>(json, jsonSerializerSettings);
            return result;
        }
    }
}
