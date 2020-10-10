using Castle.DynamicProxy;
using DemoCastleProxy;
using System;
using Xunit;

namespace DemoCastleProxyTests
{
    public class DemoTests
    {
        private IProxyFactory _factory;

        public DemoTests()
        {
            _factory = new ProxyFactory(new ProxyGenerator(), new ModelInterceptor());
        }

        [Fact]
        public void ModelChangesInterceptedTest()
        {
            PersonModel model = new PersonModel();
            PersonModel proxy = _factory.GetModelProxy(model);
            proxy.FirstName = "John";

            Assert.Single(model.PropertyChangeList);
            Assert.Single(proxy.PropertyChangeList);
            Assert.Equal("FirstName", model.PropertyChangeList[0]);
            Assert.Equal("FirstName", proxy.PropertyChangeList[0]);
        }

        [Fact]
        public void ModelRuleExecutedTest()
        {
            var model = new PersonModel();
            var proxy = _factory.GetModelProxy(model);
            proxy.FirstName = "John";
            Assert.NotEqual("1940-10-09", model.BirthDate?.ToString("yyyy-MM-dd"));

            proxy.LastName = "Lennon";
            Assert.Equal("1940-10-09", model.BirthDate?.ToString("yyyy-MM-dd"));
        }

        [Fact]
        public void ModelRuleNotExecutedTest()
        {
            var model = new PersonModel();
            var proxy = _factory.GetModelProxy(model);
            proxy.FirstName = "John";
            Assert.NotEqual("1940-10-09", model.BirthDate?.ToString("yyyy-MM-dd"));

            proxy.LastName = "Travolta";
            Assert.NotEqual("1940-10-09", model.BirthDate?.ToString("yyyy-MM-dd"));
        }
    }
}

