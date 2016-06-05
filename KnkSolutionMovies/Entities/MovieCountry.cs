using KnkCore;
using KnkInterfaces.PropertyAtributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkSolutionMovies.Entities
{
    public class MovieCountry : KnkItem
    {
        #region Interface/Implementation
        public MovieCountry():base(new KnkTableEntity("vieMovieCountries", "MovieCountries"))
        {
        }
        #endregion Interface/Implementation

        #region Class Properties
        [AtributePrimaryKey]
        public KnkEntityIdentifier IdMovieCompany { get; set; }
        public KnkEntityReference<Movie> IdMovie { get; set; }
        public KnkEntityReference<Country> IdCountry { get; set; }
        #endregion Class Properties

        public Movie Movie { get { return IdMovie?.Value; } set { IdMovie = new KnkEntityReference<Movie>(value); } }
        public Country Country { get { return IdCountry?.Value; } set { IdCountry = new KnkEntityReference<Country>(value); } }

        public override string ToString()
        {
            return IdCountry?.Value.ToString();
        }
    }
}
