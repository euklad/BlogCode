using System;
using System.Collections.Generic;
using System.Text;

namespace DemoCastleProxy
{
    public interface IProxyFactory
    {
        T GetModelProxy<T>(T source) where T : class;
    }
}

