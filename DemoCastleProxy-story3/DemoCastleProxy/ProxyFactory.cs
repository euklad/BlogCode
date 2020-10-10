using Castle.DynamicProxy;
using System;

namespace DemoCastleProxy
{
    public class ProxyFactory : IProxyFactory
    {
        private readonly IProxyGenerator _proxyGenerator;
        private readonly IInterceptor _interceptor;

        public ProxyFactory(IProxyGenerator proxyGenerator, IInterceptor interceptor)
        {
            _proxyGenerator = proxyGenerator;
            _interceptor = interceptor;
        }

        public T GetModelProxy<T>(T source) where T : class
        {
            var proxy = _proxyGenerator.CreateClassProxyWithTarget(source.GetType(), source, new IInterceptor[] { _interceptor }) as T;
            return proxy;
        }
    }
}
