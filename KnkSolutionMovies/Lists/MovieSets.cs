using KnkCore;
using KnkCore.Utilities;
using KnkInterfaces.Enumerations;
using KnkInterfaces.Interfaces;
using KnkSolutionMovies.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkSolutionMovies.Lists
{
    public class MovieSets : KnkList<MovieSet>
    {
        public MovieSets(KnkConnectionItf aConnection)
        : base(aConnection)
        {

        }

        public override List<MovieSet> Datasource()
        {
            return (from c in Items orderby c.ToString() select c).ToList();
        }
    }

    public class MovieMovieSets : KnkList<Movie, MovieSet>
    {
        public MovieMovieSets(Movie aMovie)
        : base(aMovie.Connection(), new KnkCriteria<Movie, MovieSet>(aMovie, new KnkTableEntityRelation<Movie>("vieMovieMovieSets", "MovieSets")))
        {

        }

        public MovieMovieSets(KnkConnectionItf aConnection, string aName)
        : base(aConnection, KnkCoreUtils.BuildLikeCriteria<Movie, MovieSet>("Name", aName, "vieMovieMovieSets", "IdSet"))
        {
        }
    }
}
