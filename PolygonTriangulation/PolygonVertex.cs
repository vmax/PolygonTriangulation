using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonTriangulation
{
    public enum PolygonVertexType
    {
        start,
        end,
        regular,
        split,
        merge
    }
    public class PolygonVertex
    {
        public static bool operator < (PolygonVertex p, PolygonVertex q)
        {
            return (p.Y < q.Y) || (p.Y == q.Y && p.X > q.X);
        }
        public static bool operator >(PolygonVertex p, PolygonVertex q)
        {
            return (p.Y > q.Y) || (p.Y == q.Y && p.X < q.X);
        }

        // as of : http://stackoverflow.com/questions/20252845/best-algorithm-for-detecting-interior-and-exterior-angles-of-an-arbitrary-shape
        public static double Angle(double pX, double pY, double qX, double qY)
        {
            double n = pX * qX + pY * qY;
            double d = Math.Sqrt((pX * pX + pY * pY) * (qX * qX + qY * qY));
            return Math.Acos(n / d);
        }
        public static bool CrossProductSign(double pX, double pY, double qX, double qY)
        {
            return pX * qY > qX * pY;
        }

        public void classifySelf ()
        {
            // we do need to compute the interior angle

            double pX, pY, qX, qY;

            pX = neighboor1.X - this.X;
            pY = neighboor1.Y - this.Y;
            qX = neighboor2.X - this.X;
            qY = neighboor2.Y - this.Y;

            double angle = PolygonVertex.Angle(pX, pY, qX, qY);
            if (PolygonVertex.CrossProductSign(pX, pY, qX, qY))
            {
                // it is exterior angle
                angle = 2 * Math.PI - angle;
            }
            if (neighboor1 < this && neighboor2 < this)
            {
                // start or split?
                type = (angle < Math.PI) ? PolygonVertexType.start : PolygonVertexType.split;           
            }
            else if (neighboor1 > this && neighboor2 > this)
            {
                // end or merge?
                type = (angle < Math.PI) ? PolygonVertexType.end : PolygonVertexType.merge;
            }
            else
            {
                type = PolygonVertexType.regular;
            }
        }

        public int index { get; set; }
        public PolygonVertex neighboor1 { get; set;}
        public PolygonVertex neighboor2 { get; set; }
        public PolygonVertexType type { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public PolygonVertex(int _x, int _y)
        {
            X = _x;
            Y = _y;
        }
    }
}
