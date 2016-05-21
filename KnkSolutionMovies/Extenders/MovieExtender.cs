using KnkInterfaces.Interfaces;
using KnkSolutionMovies.Entities;
using KnkSolutionMovies.Lists;
using KnkSolutionMovies.References;
using KnkSolutionUsers.Entities;
using KnkSolutionUsers.References;
using System;
using System.Linq;

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
        UserReference<Movie> _CreationUser = null;

        Files _Files = null;
        Genres _Genres = null;
        Pictures _Pictures = null;
        FilePlays _Plays = null;

        #endregion References

        #region Relationships
        private MovieSetReference MovieSetReference()
        {
            if (_MovieSetReference == null)
                _MovieSetReference = new MovieSetReference(_Movie, "IdSet");
            return _MovieSetReference;
        }

        private UserReference<Movie> CreationUserReference()
        {
            if (_CreationUser == null)
                _CreationUser = new UserReference<Movie>(_Movie, "UserCreationId");
            return _CreationUser;
        }

        public MovieSet MovieSet
        {
            get
            {
                return MovieSetReference().Value;
            }
        }

        public User CreationUser
        {
            get
            {
                return CreationUserReference().Value;
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

        public FilePlays Plays
        {
            get
            {
                if (_Plays == null) _Plays = new FilePlays(_Movie);
                return _Plays;
            }
        }

        public DateTime? LastPlayed()
        {
            var lLast = (from p in Plays.Items orderby p.DatePlay descending select p).FirstOrDefault();
            return lLast?.DatePlay;
        }

        public decimal AveragedRate
        {
            get
            {
                decimal lRet = _Movie.Rating;
                if(_Movie.UserRating!=null)
                {
                    lRet = (lRet + 2 * _Movie.Rating) / 3;
                }
                return lRet;

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
