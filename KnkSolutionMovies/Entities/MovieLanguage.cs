using KnkCore;
using KnkInterfaces.PropertyAtributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkSolutionMovies.Entities
{
    public class MovieLanguage : KnkItem
    {
        #region Interface/Implementation
        public MovieLanguage() : base(new KnkTableEntity("vieMovieLanguages", "MovieLanguages"))
        {
        }
        #endregion Interface/Implementation

        #region Class Properties
        [AtributePrimaryKey]
        public KnkEntityIdentifier IdMovieLanguage { get; set; }
        public KnkEntityReference<Movie> IdMovie { get; set; }
        public KnkEntityReference<Language> IdLanguage { get; set; }
        #endregion Class Properties

        public Movie Movie { get { return IdMovie?.Value; } set { IdMovie = new KnkEntityReference<Movie>(value); } }
        public Language Language { get { return IdLanguage?.Value; } set { IdLanguage = new KnkEntityReference<Language>(value); } }

        public override string ToString()
        {
            return $"{Language.Name})";
        }
    }
}
