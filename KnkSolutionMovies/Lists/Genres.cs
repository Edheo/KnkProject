using KnkCore;
using KnkInterfaces.Enumerations;
using KnkSolutionMovies.Entities;
using KnkSolutionMovies.Relationships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkSolutionMovies.Lists
{
    public class Genres : KnkList<GenreClass>
    {
        public Genres():base(new KnkConnection())
        {
            Connection.FillList(this);
        }

        public Genres(Movie aMovie) : base(aMovie.Connection)
        {
            var lLstFiles = Connection.GetList(new KnkCriteria<Movie, MovieGenre>(aMovie));
            FillFromList((from f in lLstFiles.Items select f.Genre).ToList());
        }
    }
}
