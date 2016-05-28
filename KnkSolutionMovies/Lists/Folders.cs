using KnkCore;
using KnkInterfaces.Interfaces;
using KnkSolutionMovies.Entities;

namespace KnkSolutionMovies.Lists
{
    public class Folders : KnkList<Folder,Folder>
    {
        public Folders(KnkConnectionItf aConnection) 
        : base(aConnection)
        {
        }
    }

    public class SubFolders : KnkList<Folder,Folder>
    {
        public SubFolders(Folder aFolder)
        : base(aFolder.Connection, new KnkCriteria<Folder, Folder>(aFolder, new KnkTableEntityRelation<Folder>("vieFolders", "IdParentPath")))
        {
        }
    }
}
