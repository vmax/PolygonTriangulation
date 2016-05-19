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
        private PolygonVertexManager vertexManager;

        public MainForm()
        {
            InitializeComponent();
            vertexManager = new PolygonVertexManager(polygonBox);
        }

        private void polygonBox_MouseClick(object sender, MouseEventArgs e)
        {
            // MessageBox.Show();
            listBox1.Items.Add(String.Format("X = {0} Y = {1}\n", e.X, polygonBox.Height - e.Y));
            vertexManager.addVertex(new PolygonVertex(e.X, polygonBox.Height - e.Y));
        }

        private void btnFinishBuilding_Click(object sender, EventArgs e)
        {
            vertexManager.finishBuilding();
        }

        private void btnDoTriangulate_Click(object sender, EventArgs e)
        {
            vertexManager.performTriangulation();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          //  vertexManager.addVertex(new PolygonVertex(float.Parse(textBox1.Text), float.Parse(textBox2.Text)));
        }
    }
}
