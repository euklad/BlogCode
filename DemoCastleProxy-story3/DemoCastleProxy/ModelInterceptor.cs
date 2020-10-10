using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DemoCastleProxy
{
    public class ModelInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();

            var method = invocation.Method.Name;

            if (method.StartsWith("set_"))
            {
                var field = method.Replace("set_", "");

                var proxy = invocation.Proxy as IModel;

                if (proxy != null)
                {
                    proxy.PropertyChangeList.Add(field);
                }

                // rule execution
                var model = ProxyUtil.GetUnproxiedInstance(proxy) as IModel;

                var ruleAttribute = model.GetType().GetProperty(field).GetCustomAttribute(typeof(ModelRuleAttribute)) as ModelRuleAttribute;

                if (ruleAttribute != null)
                {
                    var rule = Activator.CreateInstance(ruleAttribute.Rule) as IModelRule; 

                    if (rule != null)
                    {
                        rule.Execute(invocation.Proxy, field);
                    }
                }
            }
        }
    }
}

