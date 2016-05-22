using KnkCore;
using KnkSolutionMovies.Entities;

namespace KnkSolutionMovies.Lists
{
    public class Folders : KnkList<Folder,Folder>
    {
        public Folders() : base(new KnkConnection())
        {
        }
    }

    public class SubFolders : KnkList<Folder,Folder>
    {
        public SubFolders(Folder aFolder)
        : base(aFolder.Connection, new KnkCriteria<Folder, Folder>(aFolder))
        {
        }
    }
}
