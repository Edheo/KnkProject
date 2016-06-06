using KnkCore;
using KnkCore.Utilities;
using KnkInterfaces.Interfaces;
using KnkSolutionMovies.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkSolutionMovies.Lists
{
    public class MovieMovieSets : KnkList<Movie, MovieSet>
    {
        public MovieMovieSets(KnkConnectionItf aConnection, string aName)
        : base(aConnection, KnkCoreUtils.BuildLikeCriteria<Movie, MovieSet>("Name", aName, "vieMovieMovieSets", "IdSet"))
        {
        }
    }
}
