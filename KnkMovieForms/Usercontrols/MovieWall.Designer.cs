﻿namespace KnkMovieForms.Usercontrols
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.movieWallLayout1 = new KnkMovieForms.Usercontrols.MovieWallLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(374, 47);
            this.panel1.TabIndex = 0;
            // 
            // movieWallLayout1
            // 
            this.movieWallLayout1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.movieWallLayout1.Location = new System.Drawing.Point(0, 47);
            this.movieWallLayout1.Name = "movieWallLayout1";
            this.movieWallLayout1.Size = new System.Drawing.Size(374, 306);
            this.movieWallLayout1.TabIndex = 1;
            // 
            // MovieWall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.movieWallLayout1);
            this.Controls.Add(this.panel1);
            this.Name = "MovieWall";
            this.Size = new System.Drawing.Size(374, 353);
            this.SizeChanged += new System.EventHandler(this.MovieWall_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private MovieWallLayout movieWallLayout1;
    }
}
