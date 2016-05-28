using KnkCore;
using KnkInterfaces.Classes;
using KnkInterfaces.Interfaces;
using KnkSolutionMovies.Entities;

namespace KnkSolutionMovies.Relationships
{
    class MovieGenre : KnkItemBase
    {
        #region Interface/Implementation
        public MovieGenre():base(new KnkTableEntity("MovieGenres"))
        {
        }
        #endregion Interface/Implementation

        #region Class Properties
        public KnkEntityIdentifier IdMovieGenre { get; set; }
        public KnkEntityIdentifier IdMovie { get; set; }
        public KnkEntityIdentifier IdGenre { get; set; }
        #endregion Class Properties

        public override string ToString()
        {
            return IdGenre.ToString();
        }

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
