using KnkCore;
using KnkInterfaces.Interfaces;
using KnkSolutionMovies.Entities;

namespace KnkSolutionMovies.Lists
{
    public class Movies:KnkList<Movie>
    {
        public Movies(KnkConnectionItf aConnection)
        : base(aConnection)
        {
        }

        //public Movies(KnkConnectionItf aConnection) 
        //: base(aConnection, 
        //    new KnkCriteria<Movie, Movie>(new KnkTableEntity("vieMovies", "Movies"), typeof(int), "IdUser", KnkInterfaces.Enumerations.OperatorsEnu.Equal, aConnection.CurrentUserId()))
        //{
        //}

        public Movies(KnkConnectionItf aConnection, KnkCriteriaItf<Movie,Movie> aCriteria) 
        : base(aConnection, aCriteria)
        {
        }

    }
}
