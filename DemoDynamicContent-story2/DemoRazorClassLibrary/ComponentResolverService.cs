using DemoShared;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoRazorClassLibrary
{
    public class ComponentResolverService : IComponentTypeResolver
    {
        private readonly Dictionary<string, Type> _types = new Dictionary<string, Type>();

        public ComponentResolverService()
        {
            _types["Component1"] = typeof(DemoRazorClassLibrary.Component1);
        }

        public Type GetComponentTypeByName(string name)
        {
            return _types[name];
        }
    }
}
