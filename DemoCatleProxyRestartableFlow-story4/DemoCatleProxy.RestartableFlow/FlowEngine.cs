using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Schema;

namespace DemoCatleProxy.RestartableFlow
{
    public class FlowEngine : IFlowEngine, IInterceptor
    {
        private readonly IProxyGenerator _proxyGenerator;

        private FlowData _flowData;
        private IFlow _flow;
        private int _counter;

        public FlowEngine(IProxyGenerator proxyGenerator)
        {
            _proxyGenerator = proxyGenerator;
        }

        public FlowData RunFlow(IFlow flow)
        {
            _flowData = new FlowData();
            return ProcessFlow(flow);
        }

        public FlowData RestartFlow(IFlow flow, FlowData flowData)
        {
            _flowData = flowData;
            return ProcessFlow(flow);
        }

        private FlowData ProcessFlow(IFlow flow)
        {
            var options = new ProxyGenerationOptions(new FreezableProxyGenerationHook(flow));
            var flowProxy = _proxyGenerator.CreateClassProxyWithTarget(flow.GetType(), flow, options, new IInterceptor[] { this }) as IFlow;
            _flow = flow;

            try
            {
                // clear previous statuses
                _counter = 0;
                _flowData.IsStopped = false;
                _flowData.LastException = null;

                // run flow
                flowProxy.Execute();
                _flowData.IsFinished = true;
            }
            catch (FlowStopException e)
            {
                _flowData.IsStopped = true;
            }
            catch (Exception e)
            {
                _flowData.LastException = e;
            }

            return _flowData;
        }

        public void Intercept(IInvocation invocation)
        {
            var method = invocation.Method.Name;
            _counter++;
            var historyRecord = $"{_counter}:{method}";

            var index = _flowData.CallHistory.IndexOf(historyRecord);

            if (index == -1)
            {
                // new call, proceed and update histories if no exceptions thrown
                invocation.Proceed();
                _flowData.CallHistory.Add(historyRecord);
                // Clone Model to store new independednt instance
                _flowData.ModelHistory.Add(_flow.UntypedModel.CloneObject());
            }
            else
            {
                // replay in vacuum: don't proceed call and substitute model for next call
                _flow.SetModel(_flowData.ModelHistory[index]);
            }
        }
    }
}
