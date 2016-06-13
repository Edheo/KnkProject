using KnkCore;
using KnkInterfaces.PropertyAtributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkSolutionMovies.Entities
{
    public class MovieGenre : KnkItem
    {
        #region Interface/Implementation
        public MovieGenre():base(new KnkTableEntity("vieMovieGenres", "MovieGenres"))
        {
        }
        #endregion Interface/Implementation

        #region Class Properties
        [AtributePrimaryKey]
        public KnkEntityIdentifier IdMovieGenre { get; set; }
        public KnkEntityReference<Movie> IdMovie { get; set; }
        public KnkEntityReference<Genre> IdGenre { get; set; }
        #endregion Class Properties

        public Movie Movie { get { return IdMovie?.Reference; } }
        public Genre Genre { get { return IdGenre?.Reference; } }

        public override string ToString()
        {
            return $"{Genre.GenreName}";
        }
    }
}
