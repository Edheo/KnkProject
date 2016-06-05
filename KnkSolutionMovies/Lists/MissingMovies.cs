using KnkCore;
using KnkInterfaces.Interfaces;
using KnkSolutionMovies.Entities;

namespace KnkSolutionMovies.Lists
{
    public class MissingMovies : KnkList<MissingMovieFile>
    {
        public MissingMovies(KnkConnectionItf aConnection)
        : base(aConnection)
        {
        }
    }
}
