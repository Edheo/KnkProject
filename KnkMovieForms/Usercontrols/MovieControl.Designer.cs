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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MovieControl));
            this.tblPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.picHead = new KnkMovieForms.Usercontrols.MoviePicture();
            this.btnClose = new KnkMovieForms.Usercontrols.MoviePicture();
            this.picPoster = new KnkMovieForms.Usercontrols.MoviePicture();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            this.tblPanel.RowCount = 1;
            this.tblPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 449F));
            this.tblPanel.Size = new System.Drawing.Size(287, 449);
            this.tblPanel.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.picHead);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(487, 32);
            this.panel1.TabIndex = 0;
            // 
            // picHead
            // 
            this.picHead.BackColor = System.Drawing.Color.Black;
            this.picHead.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picHead.Caption = null;
            this.picHead.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picHead.FactorSize = 0F;
            this.picHead.Filename = null;
            this.picHead.FontColorCaption = System.Drawing.Color.DeepSkyBlue;
            this.picHead.FontColorText = System.Drawing.Color.PaleTurquoise;
            this.picHead.FontName = "Verdana";
            this.picHead.FontSize = null;
            this.picHead.FontstyleCaption = System.Drawing.FontStyle.Bold;
            this.picHead.FontstyleText = System.Drawing.FontStyle.Regular;
            this.picHead.Horizontal = true;
            this.picHead.IsButton = false;
            this.picHead.LineAlignment = System.Drawing.StringAlignment.Far;
            this.picHead.Location = new System.Drawing.Point(0, 0);
            this.picHead.Name = "picHead";
            this.picHead.RemarkColor = System.Drawing.Color.Red;
            this.picHead.ResourceImage = null;
            this.picHead.Size = new System.Drawing.Size(455, 32);
            this.picHead.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal;
            this.picHead.TabIndex = 4;
            this.picHead.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.DimGray;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnClose.Caption = null;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.FactorSize = 1F;
            this.btnClose.Filename = null;
            this.btnClose.FontColorCaption = System.Drawing.Color.White;
            this.btnClose.FontColorText = System.Drawing.Color.White;
            this.btnClose.FontName = "Verdana";
            this.btnClose.FontSize = null;
            this.btnClose.FontstyleCaption = System.Drawing.FontStyle.Bold;
            this.btnClose.FontstyleText = System.Drawing.FontStyle.Regular;
            this.btnClose.Horizontal = false;
            this.btnClose.IsButton = true;
            this.btnClose.LineAlignment = System.Drawing.StringAlignment.Center;
            this.btnClose.Location = new System.Drawing.Point(455, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(5);
            this.btnClose.RemarkColor = System.Drawing.Color.DimGray;
            this.btnClose.ResourceImage = ((System.Drawing.Image)(resources.GetObject("btnClose.ResourceImage")));
            this.btnClose.Size = new System.Drawing.Size(32, 32);
            this.btnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal;
            this.btnClose.TabIndex = 3;
            this.btnClose.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btnClose.Click += new System.EventHandler(this.button1_Click);
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
            this.picPoster.Horizontal = false;
            this.picPoster.IsButton = false;
            this.picPoster.LineAlignment = System.Drawing.StringAlignment.Center;
            this.picPoster.Location = new System.Drawing.Point(0, 32);
            this.picPoster.Name = "picPoster";
            this.picPoster.RemarkColor = System.Drawing.Color.Red;
            this.picPoster.ResourceImage = null;
            this.picPoster.Size = new System.Drawing.Size(200, 449);
            this.picPoster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal;
            this.picPoster.TabIndex = 3;
            this.picPoster.TabStop = false;
            this.picPoster.TextAlignment = System.Drawing.StringAlignment.Center;
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
            this.ResumeLayout(false);

        }

        #endregion
        private MoviePicture picPoster;
        private System.Windows.Forms.TableLayoutPanel tblPanel;
        private System.Windows.Forms.Panel panel1;
        private MoviePicture btnClose;
        private MoviePicture picHead;
    }
}
