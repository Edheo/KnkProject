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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MovieWall));
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cmbSaga = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbGenres = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbArtist = new System.Windows.Forms.ComboBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.chkViewed = new System.Windows.Forms.CheckBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.lblArtist = new System.Windows.Forms.Label();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.moviesWall = new KnkMovieForms.Usercontrols.MovieWallLayout();
            this.butScan = new KnkMovieForms.Usercontrols.MoviePicture();
            this.btnClear = new KnkMovieForms.Usercontrols.MoviePicture();
            this.btnSearch = new KnkMovieForms.Usercontrols.MoviePicture();
            this.pnlSearch.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.Color.Black;
            this.pnlSearch.Controls.Add(this.tableLayoutPanel1);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSearch.Location = new System.Drawing.Point(0, 41);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(810, 62);
            this.pnlSearch.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.cmbSaga, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmbGenres, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmbArtist, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtSearch, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.chkViewed, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblSearch, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblArtist, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.ForeColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(810, 62);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // cmbSaga
            // 
            this.cmbSaga.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbSaga.FormattingEnabled = true;
            this.cmbSaga.Location = new System.Drawing.Point(408, 33);
            this.cmbSaga.Name = "cmbSaga";
            this.cmbSaga.Size = new System.Drawing.Size(237, 21);
            this.cmbSaga.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(330, 36);
            this.label2.Margin = new System.Windows.Forms.Padding(6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 18);
            this.label2.TabIndex = 7;
            this.label2.Text = "Saga";
            // 
            // cmbGenres
            // 
            this.cmbGenres.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbGenres.FormattingEnabled = true;
            this.cmbGenres.Location = new System.Drawing.Point(84, 33);
            this.cmbGenres.Name = "cmbGenres";
            this.cmbGenres.Size = new System.Drawing.Size(237, 21);
            this.cmbGenres.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(6, 36);
            this.label1.Margin = new System.Windows.Forms.Padding(6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 18);
            this.label1.TabIndex = 5;
            this.label1.Text = "Genre";
            // 
            // cmbArtist
            // 
            this.cmbArtist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbArtist.FormattingEnabled = true;
            this.cmbArtist.Location = new System.Drawing.Point(408, 3);
            this.cmbArtist.Name = "cmbArtist";
            this.cmbArtist.Size = new System.Drawing.Size(237, 21);
            this.cmbArtist.TabIndex = 3;
            // 
            // txtSearch
            // 
            this.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearch.Location = new System.Drawing.Point(84, 3);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(237, 21);
            this.txtSearch.TabIndex = 1;
            // 
            // chkViewed
            // 
            this.chkViewed.AutoSize = true;
            this.chkViewed.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkViewed.Checked = true;
            this.chkViewed.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.chkViewed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkViewed.Location = new System.Drawing.Point(654, 6);
            this.chkViewed.Margin = new System.Windows.Forms.Padding(6);
            this.chkViewed.Name = "chkViewed";
            this.chkViewed.Size = new System.Drawing.Size(150, 18);
            this.chkViewed.TabIndex = 4;
            this.chkViewed.Text = "Viewed";
            this.chkViewed.ThreeState = true;
            this.chkViewed.UseVisualStyleBackColor = true;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(6, 6);
            this.lblSearch.Margin = new System.Windows.Forms.Padding(6);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(52, 13);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Search";
            // 
            // lblArtist
            // 
            this.lblArtist.AutoSize = true;
            this.lblArtist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblArtist.Location = new System.Drawing.Point(330, 6);
            this.lblArtist.Margin = new System.Windows.Forms.Padding(6);
            this.lblArtist.Name = "lblArtist";
            this.lblArtist.Size = new System.Drawing.Size(69, 18);
            this.lblArtist.TabIndex = 2;
            this.lblArtist.Text = "Artist";
            // 
            // pnlButtons
            // 
            this.pnlButtons.BackColor = System.Drawing.Color.Black;
            this.pnlButtons.Controls.Add(this.butScan);
            this.pnlButtons.Controls.Add(this.btnClear);
            this.pnlButtons.Controls.Add(this.btnSearch);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlButtons.Location = new System.Drawing.Point(0, 0);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(810, 41);
            this.pnlButtons.TabIndex = 6;
            // 
            // moviesWall
            // 
            this.moviesWall.Dock = System.Windows.Forms.DockStyle.Fill;
            this.moviesWall.Location = new System.Drawing.Point(0, 103);
            this.moviesWall.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.moviesWall.Name = "moviesWall";
            this.moviesWall.Size = new System.Drawing.Size(810, 434);
            this.moviesWall.TabIndex = 1;
            // 
            // butScan
            // 
            this.butScan.BackColor = System.Drawing.Color.Black;
            this.butScan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.butScan.Caption = null;
            this.butScan.Dock = System.Windows.Forms.DockStyle.Left;
            this.butScan.FactorSize = 1F;
            this.butScan.Filename = null;
            this.butScan.FontColorCaption = System.Drawing.Color.White;
            this.butScan.FontColorText = System.Drawing.Color.White;
            this.butScan.FontName = "Verdana";
            this.butScan.FontSize = 10;
            this.butScan.FontstyleCaption = System.Drawing.FontStyle.Bold;
            this.butScan.FontstyleText = System.Drawing.FontStyle.Bold;
            this.butScan.IsButton = true;
            this.butScan.LineAlignment = System.Drawing.StringAlignment.Far;
            this.butScan.Location = new System.Drawing.Point(0, 0);
            this.butScan.Name = "butScan";
            this.butScan.Padding = new System.Windows.Forms.Padding(5);
            this.butScan.RemarkColor = System.Drawing.Color.Black;
            this.butScan.ResourceImage = ((System.Drawing.Image)(resources.GetObject("butScan.ResourceImage")));
            this.butScan.Size = new System.Drawing.Size(41, 41);
            this.butScan.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.butScan.TabIndex = 7;
            this.butScan.TabStop = false;
            this.butScan.TextAlignment = System.Drawing.StringAlignment.Center;
            this.butScan.Click += new System.EventHandler(this.butScan_Load);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Black;
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnClear.Caption = null;
            this.btnClear.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClear.FactorSize = 1F;
            this.btnClear.Filename = null;
            this.btnClear.FontColorCaption = System.Drawing.Color.White;
            this.btnClear.FontColorText = System.Drawing.Color.White;
            this.btnClear.FontName = "Verdana";
            this.btnClear.FontSize = 10;
            this.btnClear.FontstyleCaption = System.Drawing.FontStyle.Bold;
            this.btnClear.FontstyleText = System.Drawing.FontStyle.Bold;
            this.btnClear.IsButton = true;
            this.btnClear.LineAlignment = System.Drawing.StringAlignment.Far;
            this.btnClear.Location = new System.Drawing.Point(687, 0);
            this.btnClear.Name = "btnClear";
            this.btnClear.Padding = new System.Windows.Forms.Padding(5);
            this.btnClear.RemarkColor = System.Drawing.Color.Black;
            this.btnClear.ResourceImage = ((System.Drawing.Image)(resources.GetObject("btnClear.ResourceImage")));
            this.btnClear.Size = new System.Drawing.Size(41, 41);
            this.btnClear.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnClear.TabIndex = 6;
            this.btnClear.TabStop = false;
            this.btnClear.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Black;
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSearch.Caption = null;
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSearch.FactorSize = 2F;
            this.btnSearch.Filename = null;
            this.btnSearch.FontColorCaption = System.Drawing.Color.White;
            this.btnSearch.FontColorText = System.Drawing.Color.White;
            this.btnSearch.FontName = "Verdana";
            this.btnSearch.FontSize = 10;
            this.btnSearch.FontstyleCaption = System.Drawing.FontStyle.Bold;
            this.btnSearch.FontstyleText = System.Drawing.FontStyle.Bold;
            this.btnSearch.IsButton = true;
            this.btnSearch.LineAlignment = System.Drawing.StringAlignment.Far;
            this.btnSearch.Location = new System.Drawing.Point(728, 0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(5);
            this.btnSearch.RemarkColor = System.Drawing.Color.Black;
            this.btnSearch.ResourceImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.ResourceImage")));
            this.btnSearch.Size = new System.Drawing.Size(82, 41);
            this.btnSearch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnSearch.TabIndex = 5;
            this.btnSearch.TabStop = false;
            this.btnSearch.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // MovieWall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.moviesWall);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.pnlButtons);
            this.Name = "MovieWall";
            this.Size = new System.Drawing.Size(810, 537);
            this.SizeChanged += new System.EventHandler(this.MovieWall_SizeChanged);
            this.pnlSearch.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSearch;
        private MovieWallLayout moviesWall;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Label lblArtist;
        private System.Windows.Forms.ComboBox cmbArtist;
        private System.Windows.Forms.CheckBox chkViewed;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox cmbGenres;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbSaga;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlButtons;
        private MoviePicture btnClear;
        private MoviePicture btnSearch;
        private MoviePicture butScan;
    }
}
