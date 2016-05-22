using KnkCore;
using KnkInterfaces.Interfaces;
using KnkSolutionMovies.Entities;

namespace KnkSolutionMovies.Lists
{
    public class Movies:KnkList<Movie,Movie>
    {
        public Movies():this(new KnkConnection())
        {
        }

        public Movies(KnkConnectionItf aConnection) : base(aConnection)
        {
        }

        public Movies(KnkConnectionItf aConnection, KnkCriteriaItf<Movie,Movie> aCriteria) : base(aConnection, aCriteria)
        {
        }
    }
}
