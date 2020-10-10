using System;
using System.Collections.Generic;
using System.Text;

namespace DemoCastleProxy
{
    public class ModelRuleAttribute : Attribute
    {
        public Type Rule { get; private set; }

        public ModelRuleAttribute(Type rule)
        {
            Rule = rule;
        }
    }
}

