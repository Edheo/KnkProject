﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KnkSolutionMovies.Entities;
using System.Threading;
using System.Windows.Media.Imaging;

namespace KnkMovieForms.Usercontrols
{
    partial class MovieThumb : UserControl
    {
        private Movie _Movie;

        public Movie Movie()
        {
            return _Movie;
        }

        public MovieThumb(Movie aMovie, int aWidth)
        {
            InitializeComponent();
            picPoster.Click += (sender, e) => { this.OnClick(e); };
            picPoster.MouseHover += (sender,e) => { OnRemarkMovie(sender, e); };
            SetSize(aWidth);
            SetMovie(aMovie);
        }

        private void SetMovie(Movie aMovie)
        {
            _Movie = aMovie;
            picPoster.Filename = _Movie.Extender.Poster?.Extender.GetFileName();
            picVals.SetValues(aMovie.Year?.ToString(), aMovie.Title);
        }

        private void OnRemarkMovie(object sender, EventArgs e)
        {
            if (!IsMarked())
            {
                this.Parent.SuspendLayout();
                picPoster.ReMarkMovie(true);
                //Control lContainer = this.Container as Control;
                var controls = from lCtn in Parent.Controls.OfType<MovieThumb>() where lCtn != this && lCtn.IsMarked() select lCtn;
                foreach (var lCtl in controls)
                {
                    lCtl.UnRemarkMovie();
                }
                this.Parent.ResumeLayout();
            }
        }

        public void UnRemarkMovie()
        {
            picPoster.ReMarkMovie(false);
        }

        public bool IsMarked()
        {
            return picPoster.HasBorder();
        }

        public static Size NormalSize()
        {
            return new Size(200, 310 + 50);
        }

        static float Aspect()
        {
            Size lSiz = NormalSize();
            return (float)lSiz.Height / (float)lSiz.Width;
        }

        public static Size GetSize(float aFactor)
        {
            Size lSiz = NormalSize();
            lSiz.Width = (int)(lSiz.Width * aFactor);
            lSiz.Height = GetHeightFromWidth(lSiz.Width);
            return lSiz;
        }

        public static int GetHeightFromWidth(int aWidth)
        {
            return (int)(aWidth * Aspect());
        }

        public static Size GetMinimumSize()
        {
            return GetSize((float)0.75);
        }

        public static Size GetMaximumSize()
        {
            return GetSize((float)1.75);
        }

        public override Size MinimumSize { get { return GetMinimumSize(); } set { base.MinimumSize = GetMinimumSize(); } }

        public override Size MaximumSize { get { return GetMaximumSize(); } set { base.MaximumSize = GetMaximumSize(); } }

        public void SetSize(int aWidth)
        {
            Size = new Size(aWidth, GetHeightFromWidth(aWidth));
        }

    }
}
