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
        delegate void delSetPosterPicture(Image aImg);
         private Size _previousSize;
        private Movie _Movie;

        public MovieThumb(Movie aMovie)
        {
            InitializeComponent();
            _previousSize = Size;
            SetMovie(aMovie);
        }

        private void SetMovie(Movie aMovie)
        {
            _Movie = aMovie;
            lblTitle.Font = new Font(lblTitle.Font, FontStyle.Bold);
            lblTitle.Text = aMovie.Title;
            Thread lThr = new Thread(new ThreadStart(LoadPosterPicture));
            lThr.Start();
        }

        private void LoadPosterPicture()
        {
            Image lImg = Image.FromFile(_Movie.Extender.Poster.Extender.GetFileName());
            picPoster.Invoke(new delSetPosterPicture(SetPosterPicture), lImg);
        }

        private void SetPosterPicture(Image aImg)
        {
            picPoster.Image = aImg;
            lblTitle.Parent = this.picPoster;
            lblTitle.BackColor = Color.Transparent;
        }

        public override Size MaximumSize
        {
            get
            {
                Size lSiz = this.MinimumSize;
                return new Size(lSiz.Width * 2, lSiz.Height * 2);
            }

            set
            {
                base.MaximumSize = this.MaximumSize;
            }
        }

        float Aspect()
        {
            return (float)MinimumSize.Height / (float)MinimumSize.Width;
        }

        private void MovieThumb_SizeChanged(object sender, EventArgs e)
        {
            Size lCurrent = this.Size;
            Size lPrevious = _previousSize;
            _previousSize = this.Size;
            if (lCurrent.Width != lPrevious.Width || lCurrent.Height != lPrevious.Height)
            {
                this.Size = new Size(lCurrent.Width, (int)(lCurrent.Width * Aspect()));
            }
        }
    }
}
