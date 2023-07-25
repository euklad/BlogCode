using Microsoft.Msagl.Core.Layout;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.Layout.Layered;
using Microsoft.Msagl.Layout.MDS;
using SvgLayerSample.Svg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorForms.Rendering.Flows
{
    public class FlowDiagramViewModel
    {
        public async Task<string> GetFlowDiagramSvg(string flowId, Type flowType)
        {
            var graph = GenerateGraph();
            var doc = new Diagram(graph);
            doc.Run();
            var svg = doc.ToString();
            return svg;
        }

        private Graph GenerateGraph()
        {
            //var graph = new Graph("Session State Machine");
            //graph.LayoutAlgorithmSettings = new MdsLayoutSettings();

            //graph.AddNode(new ComponentNode("A", technology: "State"));
            //graph.AddNode(new ComponentNode("B", technology: "State"));
            //graph.AddNode(new ComponentNode("C", technology: "State"));
            //graph.AddEdge("A", "B");
            //graph.AddEdge("B", "A");
            //graph.AddEdge("B", "C");
            //graph.AddEdge("A", "C").Attr.Color = Microsoft.Msagl.Drawing.Color.Green;

            Graph graph = new Graph();
            var sugiyamaSettings = (SugiyamaLayoutSettings)graph.LayoutAlgorithmSettings;
            sugiyamaSettings.NodeSeparation *= 2;
            graph.AddNode(new ComponentNode("A", technology: "State"));
            graph.AddNode(new ComponentNode("B", technology: "State"));
            graph.AddNode(new ComponentNode("C", technology: "State"));
            graph.AddNode(new ComponentNode("D", technology: "State"));
            graph.AddEdge("A", "B");
            graph.AddEdge("B", "A");
            graph.AddEdge("A", "C");
            graph.AddEdge("A", "D");
            //graph.LayerConstraints.PinNodesToSameLayer(new[] { graph.FindNode("A"), graph.FindNode("B"), graph.FindNode("C") });
            //graph.LayerConstraints.AddSameLayerNeighbors(graph.FindNode("A"), graph.FindNode("B"), graph.FindNode("C"));

            return graph;
        }
    }
}
