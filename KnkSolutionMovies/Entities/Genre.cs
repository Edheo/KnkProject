using KnkCore;

namespace KnkSolutionMovies.Entities
{
    public class GenreClass : KnkItemBase
    {
        #region Interface/Implementation
        public GenreClass():base(new KnkTableEntity("Genres", "IdGenre"))
        {
        }
        #endregion Interface/Implementation

        #region Class Properties
        public int? IdGenre { get; set; }

        public string Genre { get; set; }
        #endregion Class Properties
    }
}
