using KnkCore;
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
        public int? IdThumb { get; set; }
        public int? IdMovie { get; set; }
        public int? IdCast { get; set; }
        public int IdType { get; set; }
        public int Ordinal { get; set; }
        public string Thumb { get; set; }
        #endregion Class Properties
    }
}
