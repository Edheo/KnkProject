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
            this.tblPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.picHead = new KnkMovieForms.Usercontrols.MovieLabel();
            this.btnClose = new System.Windows.Forms.Button();
            this.picPoster = new KnkMovieForms.Usercontrols.MoviePicture();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
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
            this.tblPanel.Location = new System.Drawing.Point(18, 46);
            this.tblPanel.Name = "tblPanel";
            this.tblPanel.RowCount = 1;
            this.tblPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 449F));
            this.tblPanel.Size = new System.Drawing.Size(168, 118);
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
            this.picHead.FontColorCaption = System.Drawing.Color.DeepSkyBlue;
            this.picHead.FontColorText = System.Drawing.Color.PaleTurquoise;
            this.picHead.FontName = "Verdana";
            this.picHead.FontSizeCaption = null;
            this.picHead.FontSizeText = null;
            this.picHead.FontstyleCaption = System.Drawing.FontStyle.Bold;
            this.picHead.FontstyleText = System.Drawing.FontStyle.Bold;
            this.picHead.Horizontal = true;
            this.picHead.Location = new System.Drawing.Point(0, 0);
            this.picHead.Name = "picHead";
            this.picHead.Size = new System.Drawing.Size(455, 32);
            this.picHead.TabIndex = 4;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BackgroundImage = global::KnkMovieForms.Properties.Resources.Close;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(455, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(5);
            this.btnClose.Size = new System.Drawing.Size(32, 32);
            this.btnClose.TabIndex = 3;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.button1_Click);
            // 
            // picPoster
            // 
            this.picPoster.BackColor = System.Drawing.Color.Black;
            this.picPoster.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picPoster.Dock = System.Windows.Forms.DockStyle.Left;
            this.picPoster.FactorSize = 0F;
            this.picPoster.Filename = null;
            this.picPoster.IsButton = false;
            this.picPoster.Location = new System.Drawing.Point(0, 32);
            this.picPoster.Name = "picPoster";
            this.picPoster.RemarkColor = System.Drawing.Color.Red;
            this.picPoster.ResourceImage = null;
            this.picPoster.Size = new System.Drawing.Size(200, 449);
            this.picPoster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal;
            this.picPoster.TabIndex = 3;
            this.picPoster.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(200, 32);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(287, 449);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tblPanel);
            this.tabPage1.ForeColor = System.Drawing.Color.DarkGray;
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(279, 423);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(192, 74);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // MovieControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.picPoster);
            this.Controls.Add(this.panel1);
            this.Name = "MovieControl";
            this.Size = new System.Drawing.Size(487, 481);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private MoviePicture picPoster;
        private System.Windows.Forms.TableLayoutPanel tblPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnClose;
        private MovieLabel picHead;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}
