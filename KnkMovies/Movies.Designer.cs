namespace KnkMovies
{
    partial class Movies
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

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.wallMovies = new KnkMovieForms.Usercontrols.MovieWall();
            this.metroStyleManager = new MetroFramework.Components.MetroStyleManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager)).BeginInit();
            this.SuspendLayout();
            // 
            // wallMovies
            // 
            this.wallMovies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wallMovies.Location = new System.Drawing.Point(20, 60);
            this.wallMovies.Name = "wallMovies";
            this.wallMovies.Size = new System.Drawing.Size(852, 511);
            this.wallMovies.TabIndex = 0;
            // 
            // metroStyleManager
            // 
            this.metroStyleManager.Owner = this;
            this.metroStyleManager.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // Movies
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 591);
            this.Controls.Add(this.wallMovies);
            this.Name = "Movies";
            this.Text = "Form1";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager)).EndInit();
            this.ResumeLayout(false);

        }


        private KnkMovieForms.Usercontrols.MovieWall wallMovies;
        #endregion

        private MetroFramework.Components.MetroStyleManager metroStyleManager;
    }
}

