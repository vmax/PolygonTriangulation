using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonTriangulation
{
    public class PolygonEdge
    {
        public Tuple<int, int, int> toLine()
        {
            int x1, x2, y1, y2;
            x1 = edge1.X;
            y1 = edge1.Y;

            x2 = edge2.X;
            y2 = edge2.Y;

            return Tuple.Create<int, int, int>(y1 - y2, x2 - x1, x1 * y2 - x2 * y1);
        }

        public PolygonVertex edge1 { get; set; }
        public PolygonVertex edge2 { get; set; }
        public PolygonEdge(PolygonVertex e1, PolygonVertex e2)
        {
            edge1 = e1;
            edge2 = e2;

        }
    }
}
