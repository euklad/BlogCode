using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DemoCatleProxy.RestartableFlow
{
    public class FreezableProxyGenerationHook : IProxyGenerationHook
    {
        private IFlow _flow;

        public FreezableProxyGenerationHook(IFlow flow)
        {
            _flow = flow;
        }

        public override int GetHashCode()
        {
            return _flow.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return _flow == (obj as FreezableProxyGenerationHook)._flow;
        }

        public bool ShouldInterceptMethod(Type type, MethodInfo memberInfo)
        {
            return memberInfo.Name != "Execute" && memberInfo.Name != "SetModel";
        }

        public void NonVirtualMemberNotification(Type type, MemberInfo memberInfo)
        {
        }

        public void MethodsInspected()
        {
        }

        public void NonProxyableMemberNotification(Type type, MemberInfo memberInfo)
        {

        }
    }
}
