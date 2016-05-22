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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblArtist = new System.Windows.Forms.Label();
            this.cmbArtist = new System.Windows.Forms.ComboBox();
            this.btnSearch = new KnkMovieForms.Usercontrols.MoviePictureBox();
            this.moviesWall = new KnkMovieForms.Usercontrols.MovieWallLayout();
            this.chkViewed = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSearch)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(810, 47);
            this.panel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.lblSearch);
            this.flowLayoutPanel1.Controls.Add(this.txtSearch);
            this.flowLayoutPanel1.Controls.Add(this.lblArtist);
            this.flowLayoutPanel1.Controls.Add(this.cmbArtist);
            this.flowLayoutPanel1.Controls.Add(this.chkViewed);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(762, 47);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(6, 6);
            this.lblSearch.Margin = new System.Windows.Forms.Padding(6);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(41, 13);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Search";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(56, 3);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(208, 20);
            this.txtSearch.TabIndex = 1;
            // 
            // lblArtist
            // 
            this.lblArtist.AutoSize = true;
            this.lblArtist.Location = new System.Drawing.Point(273, 6);
            this.lblArtist.Margin = new System.Windows.Forms.Padding(6);
            this.lblArtist.Name = "lblArtist";
            this.lblArtist.Size = new System.Drawing.Size(30, 13);
            this.lblArtist.TabIndex = 2;
            this.lblArtist.Text = "Artist";
            // 
            // cmbArtist
            // 
            this.cmbArtist.FormattingEnabled = true;
            this.cmbArtist.Location = new System.Drawing.Point(312, 3);
            this.cmbArtist.Name = "cmbArtist";
            this.cmbArtist.Size = new System.Drawing.Size(177, 21);
            this.cmbArtist.TabIndex = 3;
            // 
            // btnSearch
            // 
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSearch.Filename = null;
            this.btnSearch.FontName = "Verdana";
            this.btnSearch.Fontstyle = System.Drawing.FontStyle.Bold;
            this.btnSearch.Image = global::KnkMovieForms.Properties.Resources.search;
            this.btnSearch.LineAlignment = System.Drawing.StringAlignment.Center;
            this.btnSearch.Location = new System.Drawing.Point(762, 0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(48, 47);
            this.btnSearch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnSearch.TabIndex = 2;
            this.btnSearch.TabStop = false;
            this.btnSearch.Text = "Search";
            this.btnSearch.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // moviesWall
            // 
            this.moviesWall.Dock = System.Windows.Forms.DockStyle.Fill;
            this.moviesWall.Location = new System.Drawing.Point(0, 47);
            this.moviesWall.Name = "moviesWall";
            this.moviesWall.Size = new System.Drawing.Size(810, 490);
            this.moviesWall.TabIndex = 1;
            // 
            // chkViewed
            // 
            this.chkViewed.AutoSize = true;
            this.chkViewed.Location = new System.Drawing.Point(495, 3);
            this.chkViewed.Name = "chkViewed";
            this.chkViewed.Size = new System.Drawing.Size(61, 17);
            this.chkViewed.TabIndex = 4;
            this.chkViewed.Text = "Viewed";
            this.chkViewed.ThreeState = true;
            this.chkViewed.UseVisualStyleBackColor = true;
            // 
            // MovieWall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.moviesWall);
            this.Controls.Add(this.panel1);
            this.Name = "MovieWall";
            this.Size = new System.Drawing.Size(810, 537);
            this.SizeChanged += new System.EventHandler(this.MovieWall_SizeChanged);
            this.panel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSearch)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private MovieWallLayout moviesWall;
        private MoviePictureBox btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label lblArtist;
        private System.Windows.Forms.ComboBox cmbArtist;
        private System.Windows.Forms.CheckBox chkViewed;
    }
}
