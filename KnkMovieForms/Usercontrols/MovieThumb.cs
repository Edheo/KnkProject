using System;
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

        public MovieThumb(Movie aMovie, int aWidth)
        {
            InitializeComponent();
            SetSize(aWidth);
            SetMovie(aMovie);
        }

        private void SetMovie(Movie aMovie)
        {
            _Movie = aMovie;
            lblTitle.Font = new Font(lblTitle.Font, FontStyle.Bold);
            lblTitle.Text = $"{_Movie.Title} ({_Movie.Year})";
            picPoster.Filename = _Movie.Extender.Poster.Extender.GetFileName();
            string lText = $"Votes:{_Movie.Votes} Rating:{_Movie.Rating:0.0}";
            if (_Movie.UserRating != null)
            {
                lText = lText + $"\r\nUser Rating:{_Movie.UserRating:0.0} Value:{_Movie.Extender.AveragedRate:0.0}";
            }
            if (_Movie.Extender.Plays.Count()>0)
            { 
                lText = lText + $"\r\nLast:{_Movie.Extender.LastPlayed():dd/MM/yyyy} Views:{_Movie.Extender.Plays.Count()}";
            }
            picVals.Text = lText;

        }

        public static Size NormalSize()
        {
            //new Size(240, 410);
            return new Size(200, 360);
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
