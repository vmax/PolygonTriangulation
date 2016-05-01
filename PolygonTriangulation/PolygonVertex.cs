using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonTriangulation
{
    enum PolygonVertexType
    {
        start,
        end,
        regular,
        split,
        merge
    }

    class PolygonVertex
    {
        public PolygonVertex neighboor1 { get; set;}
        public PolygonVertex neighboor2 { get; set; }
        public PolygonVertexType type { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public PolygonVertex(float _x, float _y)
        {
            X = _x;
            Y = _y;
        }
    }
}
