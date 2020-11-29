using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAppDragAndDrop
{
    public class BoundingClientRect
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Top { get; set; }
        public double Right { get; set; }
        public double Bottom { get; set; }
        public double Left { get; set; }
    }

    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

    public class Line
    {
        public ElementReference Start { get; set; }
        public ElementReference End { get; set; }

        public double X1 { get; set; }
        public double Y1 { get; set; }
        public double X2 { get; set; }
        public double Y2 { get; set; }
    }

    public class DiagramPanelParameters
    {
        public double Width { get; set; }
        public double Height { get; set; }
    }

    public class DiagramItemModel
    {
        public double ConvX { get; set; }
        public double ConvY { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double OffsetX { get; set; }
        public double OffsetY { get; set; }
    }
}
