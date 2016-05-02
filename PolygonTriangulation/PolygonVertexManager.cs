using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolygonTriangulation
{
    class PolygonVertexManager
    {
        List<PolygonVertex> vertices;
        PictureBox polygonBox;
        Graphics graphics;
        public PolygonVertexManager(PictureBox polygonPictureBox)
        {
            vertices = new List<PolygonVertex> ();
            polygonBox = polygonPictureBox;
        }
        public void addVertex(PolygonVertex pv)
        {
            // FIXME: probably we don't want to get a new graphics on each painting
            graphics = polygonBox.CreateGraphics();

            if (vertices.Count > 2 && checkCrossing(pv))
            {
                MessageBox.Show("Добавленная вершина создаст в прямоугольнике самопересечение!", "Ошибка");
                return;
            };

            vertices.Add(pv);
            graphics.FillEllipse(Brushes.Black, pv.X, pv.Y, 5, 5);
            if (vertices.Count > 1)
            {
                PolygonVertex prevVertex = vertices[vertices.Count - 2];
                pv.neighboor1 = prevVertex;
                prevVertex.neighboor2 = pv;
                graphics.DrawLine(Pens.Black, pv.X, pv.Y, prevVertex.X, prevVertex.Y);
            }
        }

        public static void Swap (ref float a, ref float b)
        {
            float c = a;
            a = b;
            b = c;
        }

        public static float area(PolygonVertex a, PolygonVertex b, PolygonVertex c)
        {
            return (b.X - a.X) * (c.Y - a.Y) - (b.Y - a.Y) * (c.X - a.X);
        }

        public static bool intersect_1(float a, float b, float c, float d)
        {
            if (a > b) Swap(ref a,ref b);
            if (c > d) Swap(ref c, ref d);
            return Math.Max(a, c) <= Math.Min(b, d);
        }

        public static bool intersect(PolygonVertex a, PolygonVertex b, PolygonVertex c, PolygonVertex d)
        {
            return intersect_1(a.X, b.X, c.X, d.X)
                && intersect_1(a.Y, b.Y, c.Y, d.Y)
                && area(a, b, c) * area(a, b, d) <= 0
                && area(c, d, a) * area(c, d, b) <= 0;
        }

        public void performVerticesClassification()          
        {
            foreach (PolygonVertex v in vertices)
            {
                v.classifySelf();
            }
            graphics = polygonBox.CreateGraphics();
            foreach (PolygonVertex v in vertices)
            {
                Brush b = Brushes.Black;
                switch(v.type)
                {
                    case PolygonVertexType.start:
                        b = Brushes.Yellow;
                        break;
                    case PolygonVertexType.split:
                        b = Brushes.Red;
                        break;
                    case PolygonVertexType.end:
                        b = Brushes.Green;
                        break;
                    case PolygonVertexType.merge:
                        b = Brushes.Blue;
                        break;
                    case PolygonVertexType.regular:
                        b = Brushes.Black;
                        break;
                }
                graphics.FillEllipse(b, v.X, v.Y, 10, 10);

            }
            // TODO: самопересечение которое образуется при звершении построения
            // TODO: почему координаты перевёрнуты (проверить на наборе точек>)

        }

        public bool checkCrossing(PolygonVertex newVertex)
        {
            PolygonVertex lastPoint = vertices[vertices.Count - 1];
            for (int i = 0; i < vertices.Count - 2; i++)
            {
                if (intersect(vertices[i], vertices[i+1], newVertex, lastPoint))
                {
                    return true;
                }
            }
            return false;
        }

        public void performTriangulation ()
        {
            performVerticesClassification();

        }

        public void finishBuilding()
        {
            // FIXME: probably we don't want to get a new graphics on each painting
            graphics = polygonBox.CreateGraphics();
            PolygonVertex first, last;
            first = vertices[0];
            last = vertices[vertices.Count - 1];
            graphics.DrawLine(Pens.Black, first.X, first.Y, last.X, last.Y);
            last.neighboor2 = first;
            first.neighboor1 = last;
            //polygonBox.Enabled = false;
        }
    }
}