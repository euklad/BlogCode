using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace DemoSystemTextJson.Tests
{
    public class JsonWrapperConverterTests
    {
        private MyState GetSampleData()
        {
            return new MyState
            {
                Id = 11,
                Name = "CurrentState",
                IsReady = true,
                LastUpdated = new DateTime(2015, 10, 21),
                Model = new MyModel { FirstName = "Alex", LastName = "Brown", BirthDate = new DateTime(1990, 1, 12) }
            };
        }

        private readonly ITestOutputHelper _output;

        public JsonWrapperConverterTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void JsonWrapperConverterSerializeTest()
        {
            var data = GetSampleData();

            var converter = new JsonWrapperConverter();
            converter.AddWrapper<MyStateWrapper, MyState>();
            converter.AddModel<MyModel>();

            var json = converter.Serialize(data, data.Model.GetType());
            var restored = converter.Deserialize<MyState>(json);

            Assert.NotNull(restored.Model);
            Assert.True(restored.Model.GetType() == typeof(MyModel));
        }

        [Fact]
        public void JsonWrapperConverterPerformanceTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            var converter = new JsonWrapperConverter();
            converter.AddWrapper<MyStateWrapper, MyState>();
            converter.AddModel<MyModel>();

            for (int i = 0; i < 1000000; i++)
            {
                var data = GetSampleData();
                var json = converter.Serialize(data, data.Model.GetType());
                var restored = converter.Deserialize<MyState>(json);
            }

            sw.Stop();
            _output.WriteLine($"JsonWrapperConverterPerformanceTest elapsed {sw.ElapsedMilliseconds} ms");
        }

        [Fact]
        public void JsonNewtonsoftPerformanceTest()
        {
            var sw = new Stopwatch();
            sw.Start();

            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                TypeNameAssemblyFormatHandling = Newtonsoft.Json.TypeNameAssemblyFormatHandling.Simple,
                TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Objects,
            };

            for (int i = 0; i < 1000000; i++)
            {
                var data = GetSampleData();
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(data, settings);
                var restored = Newtonsoft.Json.JsonConvert.DeserializeObject<MyState>(json);
            }

            sw.Stop();
            _output.WriteLine($"JsonNewtonsoftPerformanceTest elapsed {sw.ElapsedMilliseconds} ms");
        }
    }
}
