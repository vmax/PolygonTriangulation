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
            // 
            // btnFinishBuilding
            // 
            this.btnFinishBuilding.Location = new System.Drawing.Point(606, 12);
            this.btnFinishBuilding.Name = "btnFinishBuilding";
            this.btnFinishBuilding.Size = new System.Drawing.Size(156, 23);
            this.btnFinishBuilding.TabIndex = 2;
            this.btnFinishBuilding.Text = "Finish building triangle";
            this.btnFinishBuilding.UseVisualStyleBackColor = true;
            this.btnFinishBuilding.Click += new System.EventHandler(this.btnFinishBuilding_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(774, 450);
            this.Controls.Add(this.btnFinishBuilding);
            this.Controls.Add(this.btnDoTriangulate);
            this.Controls.Add(this.polygonBox);
            this.Name = "MainForm";
            this.Text = "PolygonTriangulation";
            ((System.ComponentModel.ISupportInitialize)(this.polygonBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox polygonBox;
        private System.Windows.Forms.Button btnDoTriangulate;
        private System.Windows.Forms.Button btnFinishBuilding;
    }
}

