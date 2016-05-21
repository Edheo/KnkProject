using KnkCore;
using KnkSolutionMovies.Entities;
using System.Collections.Generic;
using System.Linq;

namespace KnkSolutionMovies.Lists
{
    public class Files : KnkList<File>
    {
        public Files():base(new KnkConnection())
        {
            Connection.FillList(this);
        }

        public Files(Movie aMovie) : base(aMovie.Connection)
        {
            KnkTableEntity lEntity = new KnkTableEntity("vieMovieFiles", "IdFile");
            var lLstFiles = Connection.GetList(new KnkCriteria<Movie, File>(aMovie, lEntity));
            FillFromList((from f in lLstFiles.Items select f).ToList());
        }

        public override List<File> Datasource()
        {
            return this.Items;
        }
    }
}
