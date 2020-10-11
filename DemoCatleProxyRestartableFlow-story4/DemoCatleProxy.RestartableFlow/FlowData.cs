using System;
using System.Collections.Generic;
using System.Text;

namespace DemoCatleProxy.RestartableFlow
{
    public class FlowData
    {
        public bool IsFinished { get; set; }
        public List<string> CallHistory { get; set; } = new List<string>();
        public List<object> ModelHistory { get; set; } = new List<object>();
        public bool IsStopped { get; set; }
        public Exception LastException { get; set; }
    }
}
