namespace KnkMovieForms.Forms
{
    partial class ScanLibrariesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScanLibrariesForm));
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grdRoots = new System.Windows.Forms.DataGridView();
            this.grdResults = new System.Windows.Forms.DataGridView();
            this.btnUpdates = new KnkMovieForms.Usercontrols.MoviePicture();
            this.btnDelete = new KnkMovieForms.Usercontrols.MoviePicture();
            this.btnScan = new KnkMovieForms.Usercontrols.MoviePicture();
            this.pnlButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdRoots)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdResults)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlButtons
            // 
            this.pnlButtons.BackColor = System.Drawing.Color.Black;
            this.pnlButtons.Controls.Add(this.btnUpdates);
            this.pnlButtons.Controls.Add(this.btnDelete);
            this.pnlButtons.Controls.Add(this.btnScan);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlButtons.Location = new System.Drawing.Point(0, 0);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(674, 41);
            this.pnlButtons.TabIndex = 7;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 41);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grdRoots);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grdResults);
            this.splitContainer1.Size = new System.Drawing.Size(674, 422);
            this.splitContainer1.SplitterDistance = 375;
            this.splitContainer1.TabIndex = 8;
            // 
            // grdRoots
            // 
            this.grdRoots.AllowUserToAddRows = false;
            this.grdRoots.AllowUserToDeleteRows = false;
            this.grdRoots.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.grdRoots.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdRoots.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdRoots.Location = new System.Drawing.Point(0, 0);
            this.grdRoots.Name = "grdRoots";
            this.grdRoots.ReadOnly = true;
            this.grdRoots.Size = new System.Drawing.Size(375, 422);
            this.grdRoots.TabIndex = 0;
            // 
            // grdResults
            // 
            this.grdResults.AllowUserToAddRows = false;
            this.grdResults.AllowUserToDeleteRows = false;
            this.grdResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.grdResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdResults.Location = new System.Drawing.Point(0, 0);
            this.grdResults.Name = "grdResults";
            this.grdResults.Size = new System.Drawing.Size(295, 422);
            this.grdResults.TabIndex = 1;
            // 
            // btnUpdates
            // 
            this.btnUpdates.BackColor = System.Drawing.Color.Black;
            this.btnUpdates.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnUpdates.Caption = null;
            this.btnUpdates.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnUpdates.FactorSize = 1F;
            this.btnUpdates.Filename = null;
            this.btnUpdates.FontColorCaption = System.Drawing.Color.White;
            this.btnUpdates.FontColorText = System.Drawing.Color.White;
            this.btnUpdates.FontName = "Verdana";
            this.btnUpdates.FontSize = 10;
            this.btnUpdates.FontstyleCaption = System.Drawing.FontStyle.Bold;
            this.btnUpdates.FontstyleText = System.Drawing.FontStyle.Bold;
            this.btnUpdates.IsButton = true;
            this.btnUpdates.LineAlignment = System.Drawing.StringAlignment.Far;
            this.btnUpdates.Location = new System.Drawing.Point(82, 0);
            this.btnUpdates.Name = "btnUpdates";
            this.btnUpdates.Padding = new System.Windows.Forms.Padding(5);
            this.btnUpdates.RemarkColor = System.Drawing.Color.Black;
            this.btnUpdates.ResourceImage = ((System.Drawing.Image)(resources.GetObject("btnUpdates.ResourceImage")));
            this.btnUpdates.Size = new System.Drawing.Size(41, 41);
            this.btnUpdates.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnUpdates.TabIndex = 8;
            this.btnUpdates.TabStop = false;
            this.btnUpdates.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btnUpdates.Click += new System.EventHandler(this.btnUpdates_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Black;
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnDelete.Caption = null;
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDelete.FactorSize = 1F;
            this.btnDelete.Filename = null;
            this.btnDelete.FontColorCaption = System.Drawing.Color.White;
            this.btnDelete.FontColorText = System.Drawing.Color.White;
            this.btnDelete.FontName = "Verdana";
            this.btnDelete.FontSize = 10;
            this.btnDelete.FontstyleCaption = System.Drawing.FontStyle.Bold;
            this.btnDelete.FontstyleText = System.Drawing.FontStyle.Bold;
            this.btnDelete.IsButton = true;
            this.btnDelete.LineAlignment = System.Drawing.StringAlignment.Far;
            this.btnDelete.Location = new System.Drawing.Point(41, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Padding = new System.Windows.Forms.Padding(5);
            this.btnDelete.RemarkColor = System.Drawing.Color.Black;
            this.btnDelete.ResourceImage = ((System.Drawing.Image)(resources.GetObject("btnDelete.ResourceImage")));
            this.btnDelete.Size = new System.Drawing.Size(41, 41);
            this.btnDelete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnDelete.TabIndex = 7;
            this.btnDelete.TabStop = false;
            this.btnDelete.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btnDelete.Click += new System.EventHandler(this.btnDeletes_Click);
            // 
            // btnScan
            // 
            this.btnScan.BackColor = System.Drawing.Color.Black;
            this.btnScan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnScan.Caption = null;
            this.btnScan.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnScan.FactorSize = 1F;
            this.btnScan.Filename = null;
            this.btnScan.FontColorCaption = System.Drawing.Color.White;
            this.btnScan.FontColorText = System.Drawing.Color.White;
            this.btnScan.FontName = "Verdana";
            this.btnScan.FontSize = 10;
            this.btnScan.FontstyleCaption = System.Drawing.FontStyle.Bold;
            this.btnScan.FontstyleText = System.Drawing.FontStyle.Bold;
            this.btnScan.IsButton = true;
            this.btnScan.LineAlignment = System.Drawing.StringAlignment.Far;
            this.btnScan.Location = new System.Drawing.Point(0, 0);
            this.btnScan.Name = "btnScan";
            this.btnScan.Padding = new System.Windows.Forms.Padding(5);
            this.btnScan.RemarkColor = System.Drawing.Color.Black;
            this.btnScan.ResourceImage = ((System.Drawing.Image)(resources.GetObject("btnScan.ResourceImage")));
            this.btnScan.Size = new System.Drawing.Size(41, 41);
            this.btnScan.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnScan.TabIndex = 5;
            this.btnScan.TabStop = false;
            this.btnScan.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btnScan.Click += new System.EventHandler(this.butScan_Click);
            // 
            // ScanLibrariesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 463);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.pnlButtons);
            this.Name = "ScanLibrariesForm";
            this.Text = "ScanLibrariesForm";
            this.pnlButtons.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdRoots)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdResults)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlButtons;
        private Usercontrols.MoviePicture btnDelete;
        private Usercontrols.MoviePicture btnScan;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView grdRoots;
        private System.Windows.Forms.DataGridView grdResults;
        private Usercontrols.MoviePicture btnUpdates;
    }
}