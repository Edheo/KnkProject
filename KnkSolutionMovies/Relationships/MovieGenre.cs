using KnkCore;
using KnkInterfaces.Interfaces;
using KnkSolutionMovies.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkSolutionMovies.Relationships
{
    class MovieGenre : KnkItemBase
    {
        #region Interface/Implementation
        public MovieGenre():base()
        {
            SourceEntity.SourceTable = "MovieGenres";
            SourceEntity.PrimaryKey = "IdMovieGenre";
        }
        #endregion Interface/Implementation

        #region Class Properties
        public int? IdMovieGenre { get; set; }
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
