using KnkCore;
using KnkInterfaces.Interfaces;
using KnkSolutionMovies.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkSolutionMovies.Lists
{
    public class MovieSummaries : KnkList<Movie, MovieSummary>
    {
        public MovieSummaries(Movie aMovie)
        : base(aMovie.Connection(), new KnkCriteria<Movie,MovieSummary>(aMovie))
        {

        }
    }
}
