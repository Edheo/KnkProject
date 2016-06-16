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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnScan = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPage1 = new MetroFramework.Controls.MetroTabPage();
            this.tblPanel = new System.Windows.Forms.TableLayoutPanel();
            this.metroTabPage3 = new MetroFramework.Controls.MetroTabPage();
            this.floCrew = new System.Windows.Forms.FlowLayoutPanel();
            this.metroTabPage2 = new MetroFramework.Controls.MetroTabPage();
            this.txtSummary = new System.Windows.Forms.TextBox();
            this.picPoster = new KnkMovieForms.Usercontrols.MoviePicture();
            this.metroTabPage4 = new MetroFramework.Controls.MetroTabPage();
            this.floVideos = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1.SuspendLayout();
            this.metroTabControl1.SuspendLayout();
            this.metroTabPage1.SuspendLayout();
            this.metroTabPage3.SuspendLayout();
            this.metroTabPage2.SuspendLayout();
            this.metroTabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnScan);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(455, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(32, 481);
            this.panel1.TabIndex = 0;
            // 
            // btnScan
            // 
            this.btnScan.BackColor = System.Drawing.Color.Transparent;
            this.btnScan.BackgroundImage = global::KnkMovieForms.Properties.Resources.btnScan_ResourceImage;
            this.btnScan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnScan.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnScan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnScan.Location = new System.Drawing.Point(0, 32);
            this.btnScan.Name = "btnScan";
            this.btnScan.Padding = new System.Windows.Forms.Padding(5);
            this.btnScan.Size = new System.Drawing.Size(32, 32);
            this.btnScan.TabIndex = 6;
            this.btnScan.TabStop = false;
            this.btnScan.UseVisualStyleBackColor = false;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BackgroundImage = global::KnkMovieForms.Properties.Resources.Close;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(0, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(5);
            this.btnClose.Size = new System.Drawing.Size(32, 32);
            this.btnClose.TabIndex = 3;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.button1_Click);
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Controls.Add(this.metroTabPage1);
            this.metroTabControl1.Controls.Add(this.metroTabPage3);
            this.metroTabControl1.Controls.Add(this.metroTabPage2);
            this.metroTabControl1.Controls.Add(this.metroTabPage4);
            this.metroTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroTabControl1.Location = new System.Drawing.Point(200, 0);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 0;
            this.metroTabControl1.Size = new System.Drawing.Size(255, 481);
            this.metroTabControl1.TabIndex = 5;
            this.metroTabControl1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroTabControl1.UseStyleColors = true;
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.Controls.Add(this.tblPanel);
            this.metroTabPage1.HorizontalScrollbarBarColor = true;
            this.metroTabPage1.Location = new System.Drawing.Point(4, 35);
            this.metroTabPage1.Name = "metroTabPage1";
            this.metroTabPage1.Size = new System.Drawing.Size(247, 442);
            this.metroTabPage1.TabIndex = 0;
            this.metroTabPage1.Text = "Credits";
            this.metroTabPage1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroTabPage1.VerticalScrollbarBarColor = true;
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
            this.tblPanel.Location = new System.Drawing.Point(0, 0);
            this.tblPanel.Name = "tblPanel";
            this.tblPanel.RowCount = 1;
            this.tblPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 449F));
            this.tblPanel.Size = new System.Drawing.Size(247, 442);
            this.tblPanel.TabIndex = 4;
            // 
            // metroTabPage3
            // 
            this.metroTabPage3.Controls.Add(this.floCrew);
            this.metroTabPage3.HorizontalScrollbarBarColor = true;
            this.metroTabPage3.Location = new System.Drawing.Point(4, 35);
            this.metroTabPage3.Name = "metroTabPage3";
            this.metroTabPage3.Size = new System.Drawing.Size(247, 442);
            this.metroTabPage3.TabIndex = 2;
            this.metroTabPage3.Text = "Crew";
            this.metroTabPage3.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroTabPage3.VerticalScrollbarBarColor = true;
            // 
            // floCrew
            // 
            this.floCrew.AutoScroll = true;
            this.floCrew.BackColor = System.Drawing.Color.Black;
            this.floCrew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.floCrew.Location = new System.Drawing.Point(0, 0);
            this.floCrew.Name = "floCrew";
            this.floCrew.Size = new System.Drawing.Size(247, 442);
            this.floCrew.TabIndex = 6;
            // 
            // metroTabPage2
            // 
            this.metroTabPage2.Controls.Add(this.txtSummary);
            this.metroTabPage2.HorizontalScrollbarBarColor = true;
            this.metroTabPage2.Location = new System.Drawing.Point(4, 35);
            this.metroTabPage2.Name = "metroTabPage2";
            this.metroTabPage2.Size = new System.Drawing.Size(247, 442);
            this.metroTabPage2.TabIndex = 1;
            this.metroTabPage2.Text = "Summary";
            this.metroTabPage2.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroTabPage2.VerticalScrollbarBarColor = true;
            // 
            // txtSummary
            // 
            this.txtSummary.BackColor = System.Drawing.Color.Black;
            this.txtSummary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSummary.ForeColor = System.Drawing.Color.White;
            this.txtSummary.Location = new System.Drawing.Point(0, 0);
            this.txtSummary.Multiline = true;
            this.txtSummary.Name = "txtSummary";
            this.txtSummary.Size = new System.Drawing.Size(247, 442);
            this.txtSummary.TabIndex = 2;
            // 
            // picPoster
            // 
            this.picPoster.BackColor = System.Drawing.Color.Black;
            this.picPoster.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picPoster.Dock = System.Windows.Forms.DockStyle.Left;
            this.picPoster.FactorSize = 0F;
            this.picPoster.Filename = null;
            this.picPoster.IsButton = false;
            this.picPoster.Location = new System.Drawing.Point(0, 0);
            this.picPoster.Name = "picPoster";
            this.picPoster.RemarkColor = System.Drawing.Color.Red;
            this.picPoster.ResourceImage = null;
            this.picPoster.Size = new System.Drawing.Size(200, 481);
            this.picPoster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal;
            this.picPoster.TabIndex = 3;
            this.picPoster.TabStop = false;
            // 
            // metroTabPage4
            // 
            this.metroTabPage4.Controls.Add(this.floVideos);
            this.metroTabPage4.HorizontalScrollbarBarColor = true;
            this.metroTabPage4.Location = new System.Drawing.Point(4, 35);
            this.metroTabPage4.Name = "metroTabPage4";
            this.metroTabPage4.Size = new System.Drawing.Size(247, 442);
            this.metroTabPage4.TabIndex = 3;
            this.metroTabPage4.Text = "Media";
            this.metroTabPage4.VerticalScrollbarBarColor = true;
            // 
            // floVideos
            // 
            this.floVideos.AutoScroll = true;
            this.floVideos.BackColor = System.Drawing.Color.Black;
            this.floVideos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.floVideos.Location = new System.Drawing.Point(0, 0);
            this.floVideos.Name = "floVideos";
            this.floVideos.Size = new System.Drawing.Size(247, 442);
            this.floVideos.TabIndex = 7;
            // 
            // MovieControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.metroTabControl1);
            this.Controls.Add(this.picPoster);
            this.Controls.Add(this.panel1);
            this.Name = "MovieControl";
            this.Size = new System.Drawing.Size(487, 481);
            this.panel1.ResumeLayout(false);
            this.metroTabControl1.ResumeLayout(false);
            this.metroTabPage1.ResumeLayout(false);
            this.metroTabPage3.ResumeLayout(false);
            this.metroTabPage2.ResumeLayout(false);
            this.metroTabPage2.PerformLayout();
            this.metroTabPage4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private MoviePicture picPoster;
        private System.Windows.Forms.TableLayoutPanel tblPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnClose;
        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private MetroFramework.Controls.MetroTabPage metroTabPage1;
        private MetroFramework.Controls.MetroTabPage metroTabPage2;
        private System.Windows.Forms.TextBox txtSummary;
        private MetroFramework.Controls.MetroTabPage metroTabPage3;
        private System.Windows.Forms.FlowLayoutPanel floCrew;
        private System.Windows.Forms.Button btnScan;
        private MetroFramework.Controls.MetroTabPage metroTabPage4;
        private System.Windows.Forms.FlowLayoutPanel floVideos;
    }
}
