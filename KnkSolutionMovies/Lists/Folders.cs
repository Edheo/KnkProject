using KnkCore;
using KnkInterfaces.Interfaces;
using KnkSolutionMovies.Entities;

namespace KnkSolutionMovies.Lists
{
    public class Folders : KnkList<Folder>
    {
        public Folders(KnkConnectionItf aConnection) 
        : base(aConnection)
        {
        }
    }

    public class SubFolders : KnkList<Folder>
    {
        public SubFolders(Folder aFolder)
        : base(aFolder.Connection())
        {
            Criteria = new KnkCriteria<Folder, Folder>(this, new KnkTableEntityRelation<Folder>("vieFolders", "IdParentPath"));
        }
    }
}
