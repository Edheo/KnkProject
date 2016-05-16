using KnkCore;
using KnkInterfaces.Enumerations;
using KnkSolutionMovies.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkSolutionMovies.Lists
{
    public class Pictures : KnkList<MediaThumb>
    {
        private MediaThumb _MediaThumb;

        public Pictures() : base(new KnkConnection())
        {
            Connection.FillList(this);
        }

        public Pictures(Movie aMovie) : base(new KnkConnection())
        {
            var lLstFiles = Connection.GetList(new KnkCriteria<Movie, MediaThumb>(aMovie, aMovie.SourceEntity.PrimaryKey));
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
