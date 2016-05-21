namespace KnkMovieForms.Usercontrols
{
    partial class MovieWall
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.movieWallLayout1 = new KnkMovieForms.Usercontrols.MovieWallLayout();
            this.moviePictureBox1 = new KnkMovieForms.Usercontrols.MoviePictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.moviePictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.panel1.Controls.Add(this.moviePictureBox1);
            this.panel1.Controls.Add(this.txtSearch);
            this.panel1.Controls.Add(this.lblSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(810, 47);
            this.panel1.TabIndex = 0;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Location = new System.Drawing.Point(77, 10);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(208, 20);
            this.txtSearch.TabIndex = 1;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(24, 13);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(41, 13);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Search";
            // 
            // movieWallLayout1
            // 
            this.movieWallLayout1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.movieWallLayout1.Location = new System.Drawing.Point(0, 47);
            this.movieWallLayout1.Name = "movieWallLayout1";
            this.movieWallLayout1.Size = new System.Drawing.Size(810, 490);
            this.movieWallLayout1.TabIndex = 1;
            // 
            // moviePictureBox1
            // 
            this.moviePictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.moviePictureBox1.Filename = null;
            this.moviePictureBox1.FontName = "Verdana";
            this.moviePictureBox1.Fontstyle = System.Drawing.FontStyle.Bold;
            this.moviePictureBox1.Image = global::KnkMovieForms.Properties.Resources.search;
            this.moviePictureBox1.LineAlignment = System.Drawing.StringAlignment.Center;
            this.moviePictureBox1.Location = new System.Drawing.Point(762, 0);
            this.moviePictureBox1.Name = "moviePictureBox1";
            this.moviePictureBox1.Size = new System.Drawing.Size(48, 47);
            this.moviePictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.moviePictureBox1.TabIndex = 2;
            this.moviePictureBox1.TabStop = false;
            this.moviePictureBox1.Text = "Search";
            this.moviePictureBox1.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // MovieWall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.movieWallLayout1);
            this.Controls.Add(this.panel1);
            this.Name = "MovieWall";
            this.Size = new System.Drawing.Size(810, 537);
            this.SizeChanged += new System.EventHandler(this.MovieWall_SizeChanged);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.moviePictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private MovieWallLayout movieWallLayout1;
        private MoviePictureBox moviePictureBox1;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
    }
}
