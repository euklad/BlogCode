using System;
using Xunit;
using System.Text.Json;

namespace DemoSystemTextJson.Tests
{
    public class JsonSerializationTests
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

        [Fact]
        public void CanDeserializeMyStateTest()
        {
            var data = GetSampleData();
            Assert.Equal(typeof(MyModel), data.Model.GetType());
            var json = JsonSerializer.Serialize(data);
            var restoredData = JsonSerializer.Deserialize<MyState>(json);
            Assert.NotNull(restoredData.Model);
            Assert.Equal(typeof(MyModel), restoredData.Model.GetType());
        }

        [Fact]
        public void CanDeserializeMyStateWithJsonElementTest()
        {
            var data = GetSampleData();
            Assert.Equal(typeof(MyModel), data.Model.GetType());
            var json = JsonSerializer.Serialize(data);
            var restoredData = JsonSerializer.Deserialize<MyState>(json);
            Assert.NotNull(restoredData.Model);
            Assert.Equal(typeof(JsonElement), restoredData.Model.GetType());
            var modelJsonElement = (JsonElement)restoredData.Model;
            var modelJson = modelJsonElement.GetRawText();
            restoredData.Model = JsonSerializer.Deserialize<MyModel>(modelJson);
            Assert.Equal(typeof(MyModel), restoredData.Model.GetType());
        }

    }
}
