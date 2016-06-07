using KnkCore;
using KnkInterfaces.PropertyAtributes;
using KnkSolutionMovies.Extenders;

namespace KnkSolutionMovies.Entities
{
    public class MediaLink: KnkItem
    {
        public readonly MediaLinkExtender Extender;

        #region Interface/Implementation
        public MediaLink():base(new KnkTableEntity("vieMediaLinks", "MediaLinks"))
        {
            Extender = new MediaLinkExtender(this);
        }
        #endregion Interface/Implementation

        #region Class Properties
        [AtributePrimaryKey]
        public KnkEntityIdentifier IdLink { get; set; }
        public KnkEntityReference<Movie> IdMovie { get; set; }
        public KnkEntityReference<Casting> IdCasting { get; set; }
        public KnkEntityIdentifier IdType { get; set; }
        public int Ordinal { get; set; }
        public string Site { get; set; }
        public string SiteThumbnail { get; set; }
        public string Value { get; set; }
        #endregion Class Properties

        public Movie Movie { get { return IdMovie?.Reference; } }
        public Casting Casting { get { return IdCasting?.Reference; } }

        public override string ToString()
        {
            return string.Format(Site, Value);
        }

    }
}
