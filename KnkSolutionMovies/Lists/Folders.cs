using KnkCore;
using KnkSolutionMovies.Entities;

namespace KnkSolutionMovies.Lists
{
    public class Folders : KnkList<Folder>
    {
        public Folders():base(new KnkConnection())
        {
            Connection.FillList(this);
        }

        public Folders(Folder aFolder) : base(aFolder.Connection)
        {
            var lLstFiles = Connection.GetList(new KnkCriteria<Folder, Folder>(aFolder));
            FillFromList(lLstFiles.Items);
        }

    }
}
