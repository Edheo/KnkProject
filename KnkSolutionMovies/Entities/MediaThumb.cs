using KnkCore;
using KnkInterfaces.Classes;
using KnkInterfaces.PropertyAtributes;
using KnkSolutionMovies.Extenders;

namespace KnkSolutionMovies.Entities
{
    public class MediaThumb: KnkItemBase
    {
        public readonly MediaThumbExtender Extender;

        #region Interface/Implementation
        public MediaThumb():base(new KnkTableEntity("vieMediaThumbs", "MediaThumbs"))
        {
            Extender = new MediaThumbExtender(this);
        }
        #endregion Interface/Implementation

        #region Class Properties
        [AtributePrimaryKey]
        public KnkEntityIdentifier IdThumb { get; set; }
        public KnkEntityReference<Movie> IdMovie { get; set; }
        public KnkEntityReference<Casting> IdCast { get; set; }
        public KnkEntityIdentifier IdType { get; set; }
        public int Ordinal { get; set; }
        public string Thumb { get; set; }
        #endregion Class Properties

        public Movie Movie { get { return IdMovie?.Value; } set { IdMovie = new KnkEntityReference<Movie>(value); } }
        public Casting Casting { get { return IdCast?.Value; } set { IdCast = new KnkEntityReference<Casting>(value); } }

        public override string ToString()
        {
            return Thumb;
        }

    }
}
