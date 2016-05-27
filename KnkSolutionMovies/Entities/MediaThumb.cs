using KnkCore;
using KnkInterfaces.Classes;
using KnkSolutionMovies.Extenders;

namespace KnkSolutionMovies.Entities
{
    public class MediaThumb: KnkItemBase
    {
        public readonly MediaThumbExtender Extender;

        #region Interface/Implementation
        public MediaThumb():base(new KnkTableEntity("MediaThumbs", "IdThumb"))
        {
            Extender = new MediaThumbExtender(this);
        }
        #endregion Interface/Implementation

        #region Class Properties
        public KnkEntityIdentifier IdThumb { get; set; }
        public KnkEntityIdentifier IdMovie { get; set; }
        public KnkEntityIdentifier IdCast { get; set; }
        public KnkEntityIdentifier IdType { get; set; }
        public int Ordinal { get; set; }
        public string Thumb { get; set; }
        #endregion Class Properties

        public override string ToString()
        {
            return Thumb;
        }

    }
}
