using KnkCore;
using KnkSolutionMovies.Entities;
using System.Collections.Generic;
using System.Linq;

namespace KnkSolutionMovies.Lists
{
    //public class Files : KnkList<File>
    //{
    //    public Files() : base(new KnkConnection())
    //    {
    //        Connection.FillList(this);
    //    }
    //}

    public class MovieFiles : KnkList<Movie, File>
    {
        public MovieFiles(Movie aMovie)
        : base(aMovie.Connection, new KnkCriteria<Movie, File>(aMovie, new KnkTableEntity("vieMovieFiles", "IdFile")))
        {
        }

    }
}
