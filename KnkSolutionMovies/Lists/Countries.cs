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
    public class Countries : KnkList<Country, Country>
    {
        public Countries(KnkConnectionItf aConnection)
        : base(aConnection)
        {

        }

        public override List<Country> Datasource()
        {
            return (from c in Items orderby c.CountryName select c).ToList();
        }
    }

    public class MovieCountries : KnkList<Movie, Country>
    {
        public MovieCountries(Movie aMovie)
        : base(aMovie.Connection(), new KnkCriteria<Movie, Country>(aMovie, new KnkTableEntityRelation<Movie>("vieMovieCountries", "MovieCountries")))
        {
        }

        public MovieCountries(KnkConnectionItf aConnection, KnkCriteria<Movie, Country> aCriteria)
        : base(aConnection, aCriteria)
        {
        }

        public MovieCountries(KnkConnectionItf aConnection, string aCountry)
        : base(aConnection, KnkCore.Utilities.KnkCoreUtils.BuildLikeCriteria<Movie, Country>("Country", aCountry, "vieMovieCountries", "IdCountry"))
        {
        }
    }
}
