using KnkCore;
using KnkInterfaces.PropertyAtributes;

namespace KnkSolutionMovies.Entities
{
    public class MovieCasting : KnkItem
    {
        #region Interface/Implementation
        public MovieCasting():base(new KnkTableEntity("vieMovieCasting", "MovieCasting"))
        {
        }
        #endregion Interface/Implementation

        #region Class Properties
        [AtributePrimaryKey]
        public KnkEntityIdentifier IdMovieCasting { get; set; }
        public KnkEntityReference<Movie> IdMovie { get; set; }
        public KnkEntityReference<Casting> IdCast { get; set; }
        public KnkEntityReference<CastingType> IdCastingType { get; set; }
        public int Ordinal { get; set; }
        public string Role { get; set; }
        public string ArtistName { get; set; }
        #endregion Class Properties

        public Movie Movie { get { return IdMovie?.Value; } set { IdMovie = new KnkEntityReference<Movie>(value); } }
        public Casting Casting { get { return IdCast?.Value; } set { IdCast = new KnkEntityReference<Casting>(value); } }
        public CastingType CastingType { get { return IdCastingType?.Value; } set { IdCastingType = new KnkEntityReference<CastingType>(value); } }

        public override string ToString()
        {
            return $"{ArtistName} ({Role})";
        }


    }
}
