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
            vertices.Add(pv);
            graphics.FillEllipse(Brushes.Black, pv.X, pv.Y, 5, 5);
            if (vertices.Count > 1)
            {
                PolygonVertex prevVertex = vertices[vertices.Count - 2];
                graphics.DrawLine(Pens.Black, pv.X, pv.Y, prevVertex.X, prevVertex.Y);
            }
        }

        public void finishBuilding()
        {
            // FIXME: probably we don't want to get a new graphics on each painting
            graphics = polygonBox.CreateGraphics();
            PolygonVertex first, last;
            first = vertices[0];
            last = vertices[vertices.Count - 1];
            graphics.DrawLine(Pens.Black, first.X, first.Y, last.X, last.Y);
            //polygonBox.Enabled = false;
        }
    }
}
