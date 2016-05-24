namespace KnkMovieForms.Usercontrols
{
    partial class MovieThumb
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlBar = new System.Windows.Forms.Panel();
            this.picVals = new KnkMovieForms.Usercontrols.MoviePictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.picPoster = new KnkMovieForms.Usercontrols.MoviePictureBox();
            this.panel1.SuspendLayout();
            this.pnlBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picVals)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPoster)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.pnlBar);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.ForeColor = System.Drawing.Color.LightSkyBlue;
            this.panel1.Location = new System.Drawing.Point(0, 360);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 50);
            this.panel1.TabIndex = 1;
            // 
            // pnlBar
            // 
            this.pnlBar.Controls.Add(this.picVals);
            this.pnlBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBar.Location = new System.Drawing.Point(0, 14);
            this.pnlBar.Name = "pnlBar";
            this.pnlBar.Size = new System.Drawing.Size(240, 36);
            this.pnlBar.TabIndex = 4;
            // 
            // picVals
            // 
            this.picVals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picVals.Filename = null;
            this.picVals.FontName = "Verdana";
            this.picVals.Fontstyle = System.Drawing.FontStyle.Bold;
            this.picVals.LineAlignment = System.Drawing.StringAlignment.Center;
            this.picVals.Location = new System.Drawing.Point(0, 0);
            this.picVals.Name = "picVals";
            this.picVals.Size = new System.Drawing.Size(240, 36);
            this.picVals.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picVals.TabIndex = 3;
            this.picVals.TabStop = false;
            this.picVals.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(240, 14);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // picPoster
            // 
            this.picPoster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picPoster.Filename = null;
            this.picPoster.FontName = "Verdana";
            this.picPoster.Fontstyle = System.Drawing.FontStyle.Bold;
            this.picPoster.LineAlignment = System.Drawing.StringAlignment.Center;
            this.picPoster.Location = new System.Drawing.Point(0, 0);
            this.picPoster.Name = "picPoster";
            this.picPoster.Size = new System.Drawing.Size(240, 360);
            this.picPoster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPoster.TabIndex = 0;
            this.picPoster.TabStop = false;
            this.picPoster.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // MovieThumb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.picPoster);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MinimumSize = new System.Drawing.Size(240, 410);
            this.Name = "MovieThumb";
            this.Size = new System.Drawing.Size(240, 410);
            this.MouseEnter += new System.EventHandler(this.MovieThumb_MouseEnter);
            this.panel1.ResumeLayout(false);
            this.pnlBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picVals)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPoster)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MoviePictureBox picPoster;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlBar;
        private MoviePictureBox picVals;
    }
}
