using KnkCore;
using KnkSolutionMovies.Entities;
using System.Linq;

namespace KnkSolutionMovies.Lists
{
    public class MoviePictures : KnkList<Movie, MediaLinks>
    {
        public MoviePictures(Movie aMovie) 
        : base(aMovie.Connection(), new KnkCriteria<Movie, MediaLinks>(aMovie))
        {
        }

        private MediaLinks _MediaThumb;

        public MediaLinks Poster
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
