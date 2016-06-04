using KnkCore;
using KnkInterfaces.PropertyAtributes;

namespace KnkSolutionMovies.Entities
{
    public class Genre : KnkItemBase
    {
        #region Interface/Implementation
        public Genre():base(new KnkTableEntity("vieGenres", "Genres"))
        {
        }
        #endregion Interface/Implementation

        #region Class Properties
        [AtributePrimaryKey]
        public KnkEntityIdentifier IdGenre { get; set; }
        public string GenreName { get; set; }
        #endregion Class Properties

        public override string ToString()
        {
            return GenreName;
        }

    }
}
