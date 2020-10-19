using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using SJ = System.Text.Json;

namespace DemoSystemTextJson
{
    

    public class JsonModelWrapper
    {
        public string ModelFullName { get; set; }
    }

    public interface IJsonWrapper
    {
    }

    public class JsonWrapper : IJsonWrapper
    {
        //public static JsonWrapper CreateInstance(object source, Type modelType)
        //{
        //    Type generic = typeof(JsonWrapper<>);
        //    var targetType = generic.MakeGenericType(new Type[] { modelType });
        //    var dto = Activator.CreateInstance(targetType, source, source.LastModel) as JsonWrapper;
        //    return dto;
        //}

        private readonly Dictionary<Type, Type> _wrapperByTypeDictionary;

        public JsonWrapper()
        {
            _wrapperByTypeDictionary = new Dictionary<Type, Type>();
        }

        public void Add<W, T>()
            where W : class, IJsonModelWrapper, new()
            where T : class, new()
        {
            _wrapperByTypeDictionary.Add(typeof(W), typeof(T));
        }

        public static IJsonModelWrapper CreateInstance(object source, Type wrapperType, Type modelType)
        {
            var json = SJ.JsonSerializer.Serialize(source);
            var wrapper = SJ.JsonSerializer.Deserialize(json, wrapperType) as IJsonModelWrapper;
            wrapper.ModelFullName = modelType.FullName;
            return wrapper;
        }

        public static string Serialize(object source, Type wrapperType, Type modelType)
        {
            var wrapper = CreateInstance(source, wrapperType, modelType);
            var json = SJ.JsonSerializer.Serialize(wrapper, wrapperType);
            return json;
        }

        public static string Serialize(object source, Type modelType)
        {
            var wrapperType = source.GetType();
            var wrapper = CreateInstance(source, wrapperType, modelType);
            var json = SJ.JsonSerializer.Serialize(wrapper, wrapperType);
            return json;
        }

        public T Deserialize<T>(string json)
            where T : class, new()
        {
            Type wrapperType = (typeof(IJsonModelWrapper).IsAssignableFrom(typeof(T))) 
                ? typeof(T)
                : _wrapperByTypeDictionary[typeof(T)];

            var wrapper = SJ.JsonSerializer.Deserialize(json, wrapperType) as IJsonModelWrapper;
            var modelName = wrapper.ModelFullName;
            return null as T;
        }
    }

    public class JsonWrapper<T> : JsonWrapper
        where T: class, new()
    {
        public string ModelFullName { get; set; }
    }

    public class JsonModelWrapper<T> : IJsonModelWrapper
        where T : class, new()
    {
        public string ModelFullName { get; set; }
    }


    
}
