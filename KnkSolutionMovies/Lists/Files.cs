using KnkCore;
using KnkInterfaces.Interfaces;
using KnkSolutionMovies.Entities;

namespace KnkSolutionMovies.Lists
{
    public class Files : KnkList<File>
    {
        public Files(KnkConnectionItf aConnection)
        : base(aConnection)
        {
        }
    }
}
