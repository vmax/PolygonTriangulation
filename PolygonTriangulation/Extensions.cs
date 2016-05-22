using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonTriangulation
{
    public static class Extensions
    {
        public static PolygonEdge findEdgeStartingIn (this List<PolygonEdge> edges, PolygonVertex v)
        {
            return edges.Find(e => e.edge1 == v);
        }
        public static PolygonEdge findEdgeEndingIn (this List<PolygonEdge> edges, PolygonVertex v)
        {
            return edges.Find(e => e.edge2 == v);
        }
    
    }
}
