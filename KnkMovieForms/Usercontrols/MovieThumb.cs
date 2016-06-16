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
    partial class MovieThumb : MediaThumb
    {
        private Movie _Movie;

        public Movie Movie()
        {
            return _Movie;
        }

        private MovieThumb():base()
        {

        }

        public MovieThumb(Movie aMovie, int aWidth)
        : base(aWidth)
        {
            SetMovie(aMovie);
        }

        private void SetMovie(Movie aMovie)
        {
            _Movie = aMovie;
            FileName = _Movie.Extender.Poster?.Extender.GetFileName();
            SetValues(aMovie.Year?.ToString(), aMovie.Title);
        }

    }
}
