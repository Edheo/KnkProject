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
    public class Countries : KnkList<CountryClass, CountryClass>
    {
        public Countries(KnkConnectionItf aConnection)
        : base(aConnection)
        {

        }

        public override List<CountryClass> Datasource()
        {
            return (from c in Items orderby c.Country select c).ToList();
        }
    }

    public class MovieCountries : KnkList<Movie, CountryClass>
    {
        public MovieCountries(Movie aMovie)
        : base(aMovie.Connection, new KnkCriteria<Movie, CountryClass>(aMovie, new KnkTableEntityRelation<Movie>("vieMovieCountries")))
        {
        }

        public MovieCountries(KnkConnectionItf aConnection, KnkCriteria<Movie, CountryClass> aCriteria)
        : base(aConnection, aCriteria)
        {
        }

        public MovieCountries(KnkConnectionItf aConnection, string aCountry)
        : base(aConnection, KnkCore.Utilities.KnkCoreUtils.BuildLikeCriteria<Movie, CountryClass>("Country", aCountry, "vieMovieCountries", "IdCountry"))
        {
        }
    }
}
