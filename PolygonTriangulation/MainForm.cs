using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolygonTriangulation
{
    public partial class MainForm : Form
    {
        private Graphics polygonGraphics;
        private PolygonVertexManager vertexManager;

        public MainForm()
        {
            InitializeComponent();
            polygonGraphics = polygonBox.CreateGraphics();
            polygonGraphics.ScaleTransform(1f, -1f);
            vertexManager = new PolygonVertexManager(polygonBox);
        }

        private void polygonBox_MouseClick(object sender, MouseEventArgs e)
        {
            vertexManager.addVertex(new PolygonVertex(e.X, e.Y));
        }

        private void btnFinishBuilding_Click(object sender, EventArgs e)
        {
            vertexManager.finishBuilding();
        }

        private void btnDoTriangulate_Click(object sender, EventArgs e)
        {
            vertexManager.performTriangulation();
        }
    }
}
