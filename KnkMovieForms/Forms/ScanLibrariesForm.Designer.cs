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
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnUpdates = new System.Windows.Forms.Button();
            this.btnScan = new System.Windows.Forms.Button();
            this.grdRoots = new System.Windows.Forms.DataGridView();
            this.grdResults = new System.Windows.Forms.DataGridView();
            this.btnScrap = new System.Windows.Forms.Button();
            this.pnlButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdRoots)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdResults)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlButtons
            // 
            this.pnlButtons.BackColor = System.Drawing.Color.Black;
            this.pnlButtons.Controls.Add(this.btnScrap);
            this.pnlButtons.Controls.Add(this.btnUpdates);
            this.pnlButtons.Controls.Add(this.btnScan);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlButtons.Location = new System.Drawing.Point(0, 0);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(753, 41);
            this.pnlButtons.TabIndex = 7;
            // 
            // btnUpdates
            // 
            this.btnUpdates.BackColor = System.Drawing.Color.Transparent;
            this.btnUpdates.BackgroundImage = global::KnkMovieForms.Properties.Resources.btnUpdates_ResourceImage;
            this.btnUpdates.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUpdates.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnUpdates.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdates.Location = new System.Drawing.Point(41, 0);
            this.btnUpdates.Name = "btnUpdates";
            this.btnUpdates.Padding = new System.Windows.Forms.Padding(5);
            this.btnUpdates.Size = new System.Drawing.Size(41, 41);
            this.btnUpdates.TabIndex = 8;
            this.btnUpdates.TabStop = false;
            this.btnUpdates.UseVisualStyleBackColor = false;
            this.btnUpdates.Click += new System.EventHandler(this.btnUpdates_Click);
            // 
            // btnScan
            // 
            this.btnScan.BackColor = System.Drawing.Color.Transparent;
            this.btnScan.BackgroundImage = global::KnkMovieForms.Properties.Resources.btnScan_ResourceImage;
            this.btnScan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnScan.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnScan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnScan.Location = new System.Drawing.Point(0, 0);
            this.btnScan.Name = "btnScan";
            this.btnScan.Padding = new System.Windows.Forms.Padding(5);
            this.btnScan.Size = new System.Drawing.Size(41, 41);
            this.btnScan.TabIndex = 5;
            this.btnScan.TabStop = false;
            this.btnScan.UseVisualStyleBackColor = false;
            this.btnScan.Click += new System.EventHandler(this.butScan_Click);
            // 
            // grdRoots
            // 
            this.grdRoots.AllowUserToAddRows = false;
            this.grdRoots.AllowUserToDeleteRows = false;
            this.grdRoots.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.grdRoots.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdRoots.Dock = System.Windows.Forms.DockStyle.Left;
            this.grdRoots.Location = new System.Drawing.Point(0, 41);
            this.grdRoots.Name = "grdRoots";
            this.grdRoots.ReadOnly = true;
            this.grdRoots.Size = new System.Drawing.Size(356, 489);
            this.grdRoots.TabIndex = 9;
            // 
            // grdResults
            // 
            this.grdResults.AllowUserToAddRows = false;
            this.grdResults.AllowUserToDeleteRows = false;
            this.grdResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.grdResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdResults.Location = new System.Drawing.Point(356, 41);
            this.grdResults.Name = "grdResults";
            this.grdResults.Size = new System.Drawing.Size(397, 489);
            this.grdResults.TabIndex = 10;
            // 
            // btnScrap
            // 
            this.btnScrap.BackColor = System.Drawing.Color.Transparent;
            this.btnScrap.BackgroundImage = global::KnkMovieForms.Properties.Resources.btnScan_ResourceImage;
            this.btnScrap.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnScrap.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnScrap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnScrap.Location = new System.Drawing.Point(82, 0);
            this.btnScrap.Name = "btnScrap";
            this.btnScrap.Padding = new System.Windows.Forms.Padding(5);
            this.btnScrap.Size = new System.Drawing.Size(41, 41);
            this.btnScrap.TabIndex = 9;
            this.btnScrap.TabStop = false;
            this.btnScrap.UseVisualStyleBackColor = false;
            this.btnScrap.Click += new System.EventHandler(this.btnScrap_Click);
            // 
            // ScanLibrariesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(753, 530);
            this.Controls.Add(this.grdResults);
            this.Controls.Add(this.grdRoots);
            this.Controls.Add(this.pnlButtons);
            this.Name = "ScanLibrariesForm";
            this.Text = "ScanLibrariesForm";
            this.pnlButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdRoots)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdResults)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.Button btnUpdates;
        private System.Windows.Forms.DataGridView grdRoots;
        private System.Windows.Forms.DataGridView grdResults;
        private System.Windows.Forms.Button btnScrap;
    }
}