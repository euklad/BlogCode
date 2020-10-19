using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace DemoSystemTextJson
{
    public class JsonWrapperConverter
    {
        private readonly Dictionary<Type, Type> _wrapperByTypeDictionary;
        private readonly Dictionary<string, Type> _modelTypes;

        public JsonWrapperConverter()
        {
            _wrapperByTypeDictionary = new Dictionary<Type, Type>();
            _modelTypes = new Dictionary<string, Type>();
        }

        public void AddModel<M>()
            where M : class, new()
        {
            _modelTypes[typeof(M).FullName] = typeof(M);
        }

        public void AddWrapper<W, T>()
            where W : class, IJsonModelWrapper, new()
            where T : class, new()
        {
            _wrapperByTypeDictionary[typeof(T)] = typeof(W);
        }

        public IJsonModelWrapper CreateInstance(object source, Type wrapperType, Type modelType)
        {
            var json = JsonSerializer.Serialize(source);
            var wrapper = JsonSerializer.Deserialize(json, wrapperType) as IJsonModelWrapper;
            wrapper.ModelFullName = modelType.FullName;
            return wrapper;
        }

        public string Serialize(object source, Type modelType)
        {
            Type wrapperType = _wrapperByTypeDictionary[source.GetType()];
            var wrapper = CreateInstance(source, wrapperType, modelType);
            var json = JsonSerializer.Serialize(wrapper, wrapperType);
            return json;
        }

        public T Deserialize<T>(string json)
            where T : class, new()
        {
            Type wrapperType = _wrapperByTypeDictionary[typeof(T)];
            var result = JsonSerializer.Deserialize(json, wrapperType) as IJsonModelWrapper;
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
