using System;
using System.Collections.Generic;
using System.Text;

namespace DemoCatleProxy.RestartableFlow
{
    public interface IFlowEngine
    {
        FlowData RunFlow(IFlow flow);
        FlowData RestartFlow(IFlow flow, FlowData flowData);
    }
}
