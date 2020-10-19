using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace DemoSystemTextJson.Tests
{
    public class JsonModelConverterTests
    {
        private MyStateModified GetSampleData()
        {
            return new MyStateModified
            {
                Id = 11,
                Name = "CurrentState",
                IsReady = true,
                LastUpdated = new DateTime(2015, 10, 21),
                Model = new MyModel { FirstName = "Alex", LastName = "Brown", BirthDate = new DateTime(1990, 1, 12) }
            };
        }

        private readonly ITestOutputHelper _output;

        public JsonModelConverterTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void JsonModelConverterSerializeTest()
        {
            var data = GetSampleData();

            var converter = new JsonModelConverter();
            var json = converter.Serialize(data, data.Model.GetType());
            var restored = converter.Deserialize<MyStateModified>(json);

            Assert.NotNull(restored.Model);
            Assert.True(restored.Model.GetType() == typeof(MyModel));
        }

        [Fact]
        public void JsonModelConverterPerformanceTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            var converter = new JsonModelConverter();

            for (int i = 0; i < 1000000; i++)
            {
                var data = GetSampleData();
                var json = converter.Serialize(data, data.Model.GetType());
                var restored = converter.Deserialize<MyStateModified>(json);
            }

            sw.Stop();
            _output.WriteLine($"JsonModelConverterPerformanceTest elapsed {sw.ElapsedMilliseconds} ms");
        }

        
    }
}
