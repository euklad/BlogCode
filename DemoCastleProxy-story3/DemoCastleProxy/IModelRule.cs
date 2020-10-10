using System;
using System.Collections.Generic;
using System.Text;

namespace DemoCastleProxy
{
    public interface IModelRule
    {
        void Execute(object model, string fieldName);
    }
}

