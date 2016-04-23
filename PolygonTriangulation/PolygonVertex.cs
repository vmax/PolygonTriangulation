using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonTriangulation
{
    class PolygonVertex
    {
        public float X { get; set; }
        public float Y { get; set; }
        public PolygonVertex(float _x, float _y)
        {
            X = _x;
            Y = _y;
        }
    }
}
