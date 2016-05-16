using KnkInterfaces.Interfaces;
using KnkSolutionMovies.Entities;
using KnkSolutionMovies.Lists;
using KnkSolutionMovies.References;

namespace KnkSolutionMovies.Extenders
{
    public class MovieExtender
    {
        private readonly Movie _Movie;
        public MovieExtender(Movie aMovie)
        {
            _Movie = aMovie;
        }

        #region References
        MovieSetReference _MovieSetReference = null;
        Files _Files = null;
        Genres _Genres = null;
        Pictures _Pictures = null;
        #endregion References

        #region Relationships
        private KnkReferenceItf<Movie, MovieSet> MovieSetReference()
        {
            if (_MovieSetReference == null)
                _MovieSetReference = new MovieSetReference(_Movie, "IdSet");
            return _MovieSetReference;
        }

        public MovieSet MovieSet
        {
            get
            {
                return MovieSetReference().Value;
            }
        }

        public Files Files
        {
            get
            {
                if (_Files == null) _Files = new Files(_Movie);
                return _Files;
            }
        }

        public Genres Genres
        {
            get
            {
                if (_Genres == null) _Genres = new Genres(_Movie);
                return _Genres;
            }
        }

        public Pictures Pictures
        {
            get
            {
                if (_Pictures == null) _Pictures = new Pictures(_Movie);
                return _Pictures;
            }
        }

        public MediaThumb Poster
        {
            get
            {
                return Pictures.Poster;
            }
        }

        #endregion

        #region Methods
        public string ParsedId()
        {
            return _Movie.IdMovie.ToString().PadLeft(8, '0');
        }
        #endregion

    }
}
