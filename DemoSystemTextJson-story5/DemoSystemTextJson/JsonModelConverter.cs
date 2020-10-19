using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace DemoSystemTextJson
{
    public class JsonModelConverter
    {
        private readonly Dictionary<string, Type> _modelTypes;

        public JsonModelConverter()
        {
            _modelTypes = new Dictionary<string, Type>();
        }

        public string Serialize(IJsonModelWrapper source, Type modelType)
        {
            _modelTypes[modelType.FullName] = modelType;
            source.ModelFullName = modelType.FullName;
            var json = JsonSerializer.Serialize(source, source.GetType());
            return json;
        }

        public T Deserialize<T>(string json)
            where T : class, IJsonModelWrapper, new()
        {
            var result = JsonSerializer.Deserialize(json, typeof(T)) as T;
            var modelName = result.ModelFullName;

            var objectProperties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.PropertyType == typeof(object));

            foreach (var property in objectProperties)
            {
                var model = property.GetValue(result);

                if (model is JsonElement)
                {
                    var modelJsonElement = (JsonElement)model;
                    var modelJson = modelJsonElement.GetRawText();
                    var restoredModel = JsonSerializer.Deserialize(modelJson, _modelTypes[modelName]);
                    property.SetValue(result, restoredModel);
                }
            }

            return result as T;
        }
    }
}
