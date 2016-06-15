namespace KnkMovieForms.Usercontrols
{
    partial class MediaThumb
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
            this.picPoster = new KnkMovieForms.Usercontrols.MoviePicture();
            this.picVals = new KnkMovieForms.Usercontrols.MovieLabel();
            this.panel1.SuspendLayout();
            this.pnlBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.pnlBar);
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
            this.pnlBar.Location = new System.Drawing.Point(0, 0);
            this.pnlBar.Name = "pnlBar";
            this.pnlBar.Size = new System.Drawing.Size(240, 50);
            this.pnlBar.TabIndex = 4;
            // 
            // picPoster
            // 
            this.picPoster.BackColor = System.Drawing.Color.Black;
            this.picPoster.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picPoster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picPoster.FactorSize = 0F;
            this.picPoster.Filename = null;
            this.picPoster.IsButton = false;
            this.picPoster.Location = new System.Drawing.Point(0, 0);
            this.picPoster.Name = "picPoster";
            this.picPoster.RemarkColor = System.Drawing.Color.Red;
            this.picPoster.ResourceImage = null;
            this.picPoster.Size = new System.Drawing.Size(240, 360);
            this.picPoster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPoster.TabIndex = 0;
            this.picPoster.TabStop = false;
            // 
            // picVals
            // 
            this.picVals.BackColor = System.Drawing.Color.Black;
            this.picVals.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picVals.Caption = null;
            this.picVals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picVals.FontColorCaption = System.Drawing.Color.LightSkyBlue;
            this.picVals.FontColorText = System.Drawing.Color.White;
            this.picVals.FontName = "Verdana";
            this.picVals.FontSizeCaption = 12;
            this.picVals.FontSizeText = 10;
            this.picVals.FontstyleCaption = System.Drawing.FontStyle.Bold;
            this.picVals.FontstyleText = System.Drawing.FontStyle.Regular;
            this.picVals.Horizontal = true;
            this.picVals.Location = new System.Drawing.Point(0, 0);
            this.picVals.Margin = new System.Windows.Forms.Padding(0);
            this.picVals.Name = "picVals";
            this.picVals.Size = new System.Drawing.Size(240, 50);
            this.picVals.TabIndex = 3;
            this.picVals.TabStop = false;
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
            this.panel1.ResumeLayout(false);
            this.pnlBar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MoviePicture picPoster;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlBar;
        private MovieLabel picVals;
    }
}
