using KnkCore;
using KnkInterfaces.PropertyAtributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkSolutionMovies.Entities
{
    public class MovieCompany : KnkItem
    {
        #region Interface/Implementation
        public MovieCompany():base(new KnkTableEntity("vieMovieCompanies", "MovieCompanies"))
        {
        }
        #endregion Interface/Implementation

        #region Class Properties
        [AtributePrimaryKey]
        public KnkEntityIdentifier IdMovieCompany { get; set; }
        public KnkEntityReference<Movie> IdMovie { get; set; }
        public KnkEntityReference<Company> IdCompany { get; set; }
        #endregion Class Properties

        public Movie Movie { get { return IdMovie?.Reference; } }
        public Company Company { get { return IdCompany?.Reference; } }

        public override string ToString()
        {
            return IdCompany?.Reference.ToString();
        }
    }
}
