using Microsoft.Msagl.Drawing;
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
            var graph = new Graph();

            graph.AddNode(new ComponentNode("A", technology: "State"));
            graph.AddNode(new ComponentNode("B", technology: "State"));
            graph.AddNode(new ComponentNode("C", technology: "State"));
            graph.AddEdge("A", "B");
            graph.AddEdge("B", "C");
            graph.AddEdge("A", "C").Attr.Color = Microsoft.Msagl.Drawing.Color.Green;
            graph.FindNode("A").Attr.FillColor = Microsoft.Msagl.Drawing.Color.Magenta;
            graph.FindNode("B").Attr.FillColor = Microsoft.Msagl.Drawing.Color.MistyRose;
            graph.FindNode("B").Label.FontStyle = FontStyle.Bold;
            Microsoft.Msagl.Drawing.Node c = graph.FindNode("C");
            c.Attr.FillColor = Microsoft.Msagl.Drawing.Color.PaleGreen;
            c.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Diamond;

            return graph;
        }
    }
}
