using KnkCore;
using KnkInterfaces.Interfaces;
using KnkSolutionMovies.Entities;

namespace KnkSolutionMovies.Lists
{
    public class Files : KnkList<File, File>
    {
        public Files() : this(new KnkConnection())
        {
        }

        public Files(KnkConnectionItf aConnection)
        : base(aConnection)
        {
        }

        public Files(KnkConnectionItf aConnection, KnkCriteriaItf<File, File> aCriteria)
        : base(aConnection, aCriteria)
        {
        }

    }

    public class MissingMovies : KnkList<MissingMovieFile, MissingMovieFile>
    {
        public MissingMovies(KnkConnectionItf aConnection)
        : base(aConnection)
        {
        }
    }

}
