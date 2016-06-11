using KnkCore;
using KnkInterfaces.Interfaces;
using KnkSolutionMovies.Entities;

namespace KnkSolutionMovies.Lists
{
    public class MoviesMissing : KnkList<MissingMovieFile>
    {
        public MoviesMissing(KnkConnectionItf aConnection)
        : base(aConnection)
        {
        }
    }
}
