using KnkCore;
using KnkInterfaces.Classes;
using KnkInterfaces.PropertyAtributes;

namespace KnkSolutionMovies.Entities
{
    public class GenreClass : KnkItemBase
    {
        #region Interface/Implementation
        public GenreClass():base(new KnkTableEntity("Genres"))
        {
        }
        #endregion Interface/Implementation

        #region Class Properties
        [AtributePrimaryKey]
        public KnkEntityIdentifier IdGenre { get; set; }

        public string Genre { get; set; }
        #endregion Class Properties

        public override string ToString()
        {
            return Genre;
        }

    }
}
