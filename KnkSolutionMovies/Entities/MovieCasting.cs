using KnkCore;
using KnkInterfaces.PropertyAtributes;

namespace KnkSolutionMovies.Entities
{
    public class MovieCasting : KnkItem
    {
        #region Interface/Implementation
        public MovieCasting():base(new KnkTableEntity("vieMovieCasting", "MovieCastings"))
        {
        }
        #endregion Interface/Implementation

        #region Class Properties
        [AtributePrimaryKey]
        public KnkEntityIdentifier IdMovieCasting { get; set; }
        public KnkEntityReference<Movie> IdMovie { get; set; }
        public KnkEntityReference<Casting> IdCasting { get; set; }
        public KnkEntityReference<CastingType> IdCastingType { get; set; }
        public int Ordinal { get; set; }
        public string Role { get; set; }
        #endregion Class Properties

        public Movie Movie { get { return IdMovie?.Value; } }
        public Casting Casting { get { return IdCasting?.Value; } }
        public CastingType CastingType { get { return IdCastingType?.Value; } }

        public override string ToString()
        {
            return $"{Casting.ArtistName} ({Role})";
        }
    }
}
