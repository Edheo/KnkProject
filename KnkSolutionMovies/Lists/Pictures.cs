using KnkCore;
using KnkSolutionMovies.Entities;
using System.Linq;

namespace KnkSolutionMovies.Lists
{
    public class Pictures : KnkList<MediaThumb>
    {
        private MediaThumb _MediaThumb;

        public Pictures() : base(new KnkConnection())
        {
            Connection.FillList(this);
        }

        public Pictures(Movie aMovie) : base(aMovie.Connection)
        {
            var lLstFiles = Connection.GetList(new KnkCriteria<Movie, MediaThumb>(aMovie));
            FillFromList((from f in lLstFiles.Items select f).ToList());
        }

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
