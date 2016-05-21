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
    public partial class MovieThumb : UserControl
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
            lblTitle.Text = aMovie.Title;
            picPoster.Filename = _Movie.Extender.Poster.Extender.GetFileName();
        }

        public static Size NormalSize()
        {
            return new Size(240, 410);
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
