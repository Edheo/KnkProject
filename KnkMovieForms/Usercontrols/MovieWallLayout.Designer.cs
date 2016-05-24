namespace KnkMovieForms.Usercontrols
{
    partial class MovieWallLayout
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
            this.flowMovies = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // flowMovies
            // 
            this.flowMovies.AutoScroll = true;
            this.flowMovies.BackColor = System.Drawing.Color.Black;
            this.flowMovies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowMovies.Location = new System.Drawing.Point(0, 0);
            this.flowMovies.Margin = new System.Windows.Forms.Padding(0);
            this.flowMovies.Name = "flowMovies";
            this.flowMovies.Size = new System.Drawing.Size(611, 460);
            this.flowMovies.TabIndex = 0;
            // 
            // MovieWallLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowMovies);
            this.Name = "MovieWallLayout";
            this.Size = new System.Drawing.Size(611, 460);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowMovies;
    }
}
