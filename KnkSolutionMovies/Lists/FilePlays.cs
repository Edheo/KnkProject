using KnkCore;
using KnkSolutionMovies.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkSolutionMovies.Lists
{
    public class FilePlays : KnkList<FilePlay>
    {
        public FilePlays():base(new KnkConnection())
        {
            Connection.FillList(this);
        }

        public FilePlays(Movie aMovie) : base(aMovie.Connection)
        {
            var lLstFiles = Connection.GetList(new KnkCriteria<Movie, FilePlay>(aMovie, aMovie.SourceEntity.PrimaryKey, "vieMoviePlays"));
            FillFromList(lLstFiles.Items);
        }
    }
}
