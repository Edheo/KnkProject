namespace KnkMovieForms.Usercontrols
{
    partial class MovieControl
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.picPoster = new KnkMovieForms.Usercontrols.MoviePicture();
            this.tblPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // picPoster
            // 
            this.picPoster.BackColor = System.Drawing.Color.Black;
            this.picPoster.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picPoster.Caption = null;
            this.picPoster.Dock = System.Windows.Forms.DockStyle.Left;
            this.picPoster.FactorSize = 0F;
            this.picPoster.Filename = null;
            this.picPoster.FontColorCaption = System.Drawing.Color.White;
            this.picPoster.FontColorText = System.Drawing.Color.White;
            this.picPoster.FontName = "Verdana";
            this.picPoster.FontSize = null;
            this.picPoster.FontstyleCaption = System.Drawing.FontStyle.Bold;
            this.picPoster.FontstyleText = System.Drawing.FontStyle.Regular;
            this.picPoster.LineAlignment = System.Drawing.StringAlignment.Center;
            this.picPoster.Location = new System.Drawing.Point(0, 32);
            this.picPoster.Name = "picPoster";
            this.picPoster.ResourceImage = null;
            this.picPoster.Size = new System.Drawing.Size(200, 449);
            this.picPoster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal;
            this.picPoster.TabIndex = 3;
            this.picPoster.TabStop = false;
            this.picPoster.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // tblPanel
            // 
            this.tblPanel.BackColor = System.Drawing.Color.Black;
            this.tblPanel.ColumnCount = 4;
            this.tblPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tblPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tblPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblPanel.Location = new System.Drawing.Point(200, 32);
            this.tblPanel.Name = "tblPanel";
            this.tblPanel.Size = new System.Drawing.Size(287, 449);
            this.tblPanel.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(487, 32);
            this.panel1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.Image = global::KnkMovieForms.Properties.Resources.Close;
            this.btnClose.Location = new System.Drawing.Point(453, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(34, 32);
            this.btnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnClose.TabIndex = 1;
            this.btnClose.TabStop = false;
            this.btnClose.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::KnkMovieForms.Properties.Resources.CD;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(34, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // MovieControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.tblPanel);
            this.Controls.Add(this.picPoster);
            this.Controls.Add(this.panel1);
            this.Name = "MovieControl";
            this.Size = new System.Drawing.Size(487, 481);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private MoviePicture picPoster;
        private System.Windows.Forms.TableLayoutPanel tblPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox btnClose;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
