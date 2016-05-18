namespace PolygonTriangulation
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.polygonBox = new System.Windows.Forms.PictureBox();
            this.btnDoTriangulate = new System.Windows.Forms.Button();
            this.btnFinishBuilding = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.polygonBox)).BeginInit();
            this.SuspendLayout();
            // 
            // polygonBox
            // 
            this.polygonBox.BackColor = System.Drawing.Color.White;
            this.polygonBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.polygonBox.Location = new System.Drawing.Point(0, 0);
            this.polygonBox.Name = "polygonBox";
            this.polygonBox.Size = new System.Drawing.Size(577, 450);
            this.polygonBox.TabIndex = 0;
            this.polygonBox.TabStop = false;
            this.polygonBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.polygonBox_MouseClick);
            // 
            // btnDoTriangulate
            // 
            this.btnDoTriangulate.Location = new System.Drawing.Point(606, 378);
            this.btnDoTriangulate.Name = "btnDoTriangulate";
            this.btnDoTriangulate.Size = new System.Drawing.Size(156, 60);
            this.btnDoTriangulate.TabIndex = 1;
            this.btnDoTriangulate.Text = "Triangulate!";
            this.btnDoTriangulate.UseVisualStyleBackColor = true;
            this.btnDoTriangulate.Click += new System.EventHandler(this.btnDoTriangulate_Click);
            // 
            // btnFinishBuilding
            // 
            this.btnFinishBuilding.Location = new System.Drawing.Point(605, 12);
            this.btnFinishBuilding.Name = "btnFinishBuilding";
            this.btnFinishBuilding.Size = new System.Drawing.Size(156, 23);
            this.btnFinishBuilding.TabIndex = 2;
            this.btnFinishBuilding.Text = "Finish building polygon";
            this.btnFinishBuilding.UseVisualStyleBackColor = true;
            this.btnFinishBuilding.Click += new System.EventHandler(this.btnFinishBuilding_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(615, 82);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 3;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(615, 108);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(615, 134);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(615, 176);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(134, 173);
            this.listBox1.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(774, 450);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnFinishBuilding);
            this.Controls.Add(this.btnDoTriangulate);
            this.Controls.Add(this.polygonBox);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PolygonTriangulation";
            ((System.ComponentModel.ISupportInitialize)(this.polygonBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox polygonBox;
        private System.Windows.Forms.Button btnDoTriangulate;
        private System.Windows.Forms.Button btnFinishBuilding;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBox1;
    }
}

