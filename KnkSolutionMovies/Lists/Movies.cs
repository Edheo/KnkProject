using KnkCore;
using KnkInterfaces.Interfaces;
using KnkSolutionMovies.Entities;

namespace KnkSolutionMovies.Lists
{
    public class Movies:KnkList<Movie>
    {
        public Movies():this(new KnkConnection())
        {
            Connection.FillList(this);
        }

        public Movies(KnkConnectionItf aConnection) : base(aConnection)
        {
            Connection.FillList(this);
        }

        public Movies(KnkConnectionItf aConnection, KnkCriteriaItf<Movie,Movie> aCriteria) : base(aConnection)
        {
            Connection.FillList(this, aCriteria);
        }
    }
}
