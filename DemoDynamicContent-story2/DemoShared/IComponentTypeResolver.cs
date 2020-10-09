using System;

namespace DemoShared
{
    public interface IComponentTypeResolver
    {
        Type GetComponentTypeByName(string name);
    }
}
