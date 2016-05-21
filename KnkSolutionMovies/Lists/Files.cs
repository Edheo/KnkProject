using KnkCore;
using KnkInterfaces.Enumerations;
using KnkSolutionMovies.Entities;
using KnkSolutionMovies.Relationships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var lLstFiles = Connection.GetList(new KnkCriteria<Movie, File>(aMovie, aMovie.SourceEntity.PrimaryKey, "vieMovieFiles"));
            FillFromList((from f in lLstFiles.Items select f).ToList());
        }

        public override List<File> Datasource()
        {
            return this.Items;
        }
    }
}
