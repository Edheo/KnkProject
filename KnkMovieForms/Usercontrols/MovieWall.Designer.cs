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
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.floSearch = new System.Windows.Forms.FlowLayoutPanel();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblArtist = new System.Windows.Forms.Label();
            this.cmbArtist = new System.Windows.Forms.ComboBox();
            this.chkViewed = new System.Windows.Forms.CheckBox();
            this.btnClear = new KnkMovieForms.Usercontrols.MoviePicture();
            this.btnSearch = new KnkMovieForms.Usercontrols.MoviePicture();
            this.moviesWall = new KnkMovieForms.Usercontrols.MovieWallLayout();
            this.pnlSearch.SuspendLayout();
            this.floSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.Color.Black;
            this.pnlSearch.Controls.Add(this.floSearch);
            this.pnlSearch.Controls.Add(this.btnClear);
            this.pnlSearch.Controls.Add(this.btnSearch);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSearch.Location = new System.Drawing.Point(0, 0);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(810, 76);
            this.pnlSearch.TabIndex = 0;
            // 
            // floSearch
            // 
            this.floSearch.BackColor = System.Drawing.Color.Transparent;
            this.floSearch.Controls.Add(this.lblSearch);
            this.floSearch.Controls.Add(this.txtSearch);
            this.floSearch.Controls.Add(this.lblArtist);
            this.floSearch.Controls.Add(this.cmbArtist);
            this.floSearch.Controls.Add(this.chkViewed);
            this.floSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.floSearch.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.floSearch.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.floSearch.Location = new System.Drawing.Point(0, 0);
            this.floSearch.Name = "floSearch";
            this.floSearch.Size = new System.Drawing.Size(658, 76);
            this.floSearch.TabIndex = 3;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(6, 6);
            this.lblSearch.Margin = new System.Windows.Forms.Padding(6);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(47, 13);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Search";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(62, 3);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(208, 21);
            this.txtSearch.TabIndex = 1;
            // 
            // lblArtist
            // 
            this.lblArtist.AutoSize = true;
            this.lblArtist.Location = new System.Drawing.Point(279, 6);
            this.lblArtist.Margin = new System.Windows.Forms.Padding(6);
            this.lblArtist.Name = "lblArtist";
            this.lblArtist.Size = new System.Drawing.Size(37, 13);
            this.lblArtist.TabIndex = 2;
            this.lblArtist.Text = "Artist";
            // 
            // cmbArtist
            // 
            this.cmbArtist.FormattingEnabled = true;
            this.cmbArtist.Location = new System.Drawing.Point(325, 3);
            this.cmbArtist.Name = "cmbArtist";
            this.cmbArtist.Size = new System.Drawing.Size(177, 21);
            this.cmbArtist.TabIndex = 3;
            // 
            // chkViewed
            // 
            this.chkViewed.AutoSize = true;
            this.chkViewed.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkViewed.Checked = true;
            this.chkViewed.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.chkViewed.Location = new System.Drawing.Point(511, 6);
            this.chkViewed.Margin = new System.Windows.Forms.Padding(6);
            this.chkViewed.Name = "chkViewed";
            this.chkViewed.Size = new System.Drawing.Size(67, 17);
            this.chkViewed.TabIndex = 4;
            this.chkViewed.Text = "Viewed";
            this.chkViewed.ThreeState = true;
            this.chkViewed.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClear.Filename = null;
            this.btnClear.FontName = "Verdana";
            this.btnClear.FontSize = 10;
            this.btnClear.FontstyleText = System.Drawing.FontStyle.Bold;
            this.btnClear.LineAlignment = System.Drawing.StringAlignment.Far;
            this.btnClear.Location = new System.Drawing.Point(658, 0);
            this.btnClear.Name = "btnClear";
            this.btnClear.ResourceImage = global::KnkMovieForms.Properties.Resources.Ani200_3;
            this.btnClear.Size = new System.Drawing.Size(76, 76);
            this.btnClear.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnClear.TabIndex = 4;
            this.btnClear.TabStop = false;
            this.btnClear.Text = "Clean";
            this.btnClear.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // btnSearch
            // 
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSearch.Filename = null;
            this.btnSearch.FontName = "Verdana";
            this.btnSearch.FontSize = 10;
            this.btnSearch.FontstyleText = System.Drawing.FontStyle.Bold;
            this.btnSearch.LineAlignment = System.Drawing.StringAlignment.Far;
            this.btnSearch.Location = new System.Drawing.Point(734, 0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.ResourceImage = global::KnkMovieForms.Properties.Resources.Ani200_5;
            this.btnSearch.Size = new System.Drawing.Size(76, 76);
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
            this.moviesWall.Location = new System.Drawing.Point(0, 76);
            this.moviesWall.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.moviesWall.Name = "moviesWall";
            this.moviesWall.Size = new System.Drawing.Size(810, 461);
            this.moviesWall.TabIndex = 1;
            // 
            // MovieWall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.moviesWall);
            this.Controls.Add(this.pnlSearch);
            this.Name = "MovieWall";
            this.Size = new System.Drawing.Size(810, 537);
            this.SizeChanged += new System.EventHandler(this.MovieWall_SizeChanged);
            this.pnlSearch.ResumeLayout(false);
            this.floSearch.ResumeLayout(false);
            this.floSearch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSearch;
        private MovieWallLayout moviesWall;
        private MoviePicture btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.FlowLayoutPanel floSearch;
        private System.Windows.Forms.Label lblArtist;
        private System.Windows.Forms.ComboBox cmbArtist;
        private System.Windows.Forms.CheckBox chkViewed;
        private MoviePicture btnClear;
    }
}
