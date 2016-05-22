using KnkCore;
using KnkInterfaces.Classes;
using KnkInterfaces.Interfaces;
using KnkSolutionMovies.Entities;

namespace KnkSolutionMovies.Relationships
{
    class MovieGenre : KnkItemBase
    {
        #region Interface/Implementation
        public MovieGenre():base(new KnkTableEntity("MovieGenres", "IdMovieGenre"))
        {
        }
        #endregion Interface/Implementation

        #region Class Properties
        public KnkEntityIdentifier IdMovieGenre { get; set; }
        public int IdMovie { get; set; }
        public int IdGenre { get; set; }
        #endregion Class Properties

        KnkReferenceItf<MovieGenre, GenreClass> _GenreReference = null;

        private KnkReferenceItf<MovieGenre, GenreClass> GenreReference()
        {
            if (_GenreReference == null)
                _GenreReference = Connection.GetReference<MovieGenre, GenreClass>(this, "IdGenre");
            return _GenreReference;
        }

        public GenreClass Genre
        {
            get
            {
                return GenreReference().Value;
            }
        }

    }
}
