using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Maps
{
    public class TraceInfo
    {
        public List<Point> Points { get; set; }
        public double Length { get; set; }
        public double Time { get; set; }
        public Point? FromPoint { get; set; }
        public Point? ToPoint { get; set; }
    }
}
