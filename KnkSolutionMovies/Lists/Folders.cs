using KnkCore;
using KnkSolutionMovies.Entities;
using System.Collections.Generic;
using System.Linq;

namespace KnkSolutionMovies.Lists
{
    public class Folders : KnkList<Folder>
    {
        public Folders():base(new KnkConnection())
        {
            Connection.FillList(this);
        }

        public Folders(Folder aFolder) : base(new KnkConnection())
        {
            var lLstFiles = Connection.GetList(new KnkCriteria<Folder, Folder>(aFolder, aFolder.SourceEntity.PrimaryKey));
            FillFromList((from f in lLstFiles.Items select f).ToList());
        }

    }
}
