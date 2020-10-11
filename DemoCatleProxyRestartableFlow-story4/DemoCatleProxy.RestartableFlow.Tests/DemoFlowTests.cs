using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;
using Moq;

namespace DemoCatleProxy.RestartableFlow.Tests
{
    public class DemoFlowTests
    {
        [Fact]
        public void RunStopRestartFlowTest()
        {
            var flowEngine = new FlowEngine(new ProxyGenerator());

            var demoService = new Mock<IDemoDataService>();
            var flow = new DemoFlow1(demoService.Object);
            int approveTimes = 0;

            demoService.Setup(s => s.LoadReceivedMessage()).Returns("Important message 1");
            demoService.Setup(s => s.GetSignature(It.IsAny<string>())).Returns("0xAABBEF");
            demoService.Setup(s => s.Submit(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            
            // the first time it returns false, the second time it returns true
            demoService.Setup(s => s.IsMessageApproved(It.IsAny<string>()))
                .Returns(() => 
                {
                    approveTimes++;
                    return approveTimes == 2; 
                });

            var flowData = flowEngine.RunFlow(flow);
            Assert.True(flowData.IsStopped);
            Assert.False(flowData.IsFinished);

            // assume we saved flowData to a database and rerun the flow one day after
            var clonedFlowData = flowData.CloneObject();
            var newFlow = new DemoFlow1(demoService.Object);
            clonedFlowData = flowEngine.RestartFlow(newFlow, clonedFlowData);
            Assert.False(clonedFlowData.IsStopped);
            Assert.True(clonedFlowData.IsFinished);
        }
    }
}
