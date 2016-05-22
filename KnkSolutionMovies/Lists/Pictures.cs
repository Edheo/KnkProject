using KnkCore;
using KnkSolutionMovies.Entities;
using System.Linq;

namespace KnkSolutionMovies.Lists
{
    public class MoviePictures : KnkList<Movie, MediaThumb>
    {
        public MoviePictures(Movie aMovie) 
        : base(aMovie.Connection, new KnkCriteria<Movie, MediaThumb>(aMovie))
        {
        }

        private MediaThumb _MediaThumb;

        public MediaThumb Poster
        {
            get
            {
                if (_MediaThumb == null)
                    _MediaThumb = (from p in this.Items where p.IdType == 1 select p).FirstOrDefault();

                if (_MediaThumb == null)
                    _MediaThumb = (from p in Items select p).FirstOrDefault();

                return _MediaThumb;
            }
        }
    }
}
