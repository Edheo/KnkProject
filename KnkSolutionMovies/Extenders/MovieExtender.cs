using KnkSolutionMovies.Entities;
using KnkSolutionMovies.Lists;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KnkSolutionMovies.Extenders
{
    public class MovieExtender
    {
        MovieCastings _Casting = null;
        MovieCountries _Countries = null;
        MovieFiles _Files = null;
        MovieGenres _Genres = null;
        MoviePictures _Pictures = null;
        MoviePlays _Views = null;
        MovieSummaries _Summary = null;

        private readonly Movie _Movie;
        public MovieExtender(Movie aMovie)
        {
            _Movie = aMovie;
        }

        #region Relationships

        public MovieFiles Files
        {
            get
            {
                if (_Files == null) _Files = new MovieFiles(_Movie);
                return _Files;
            }
        }

        public string Genres
        {
            get
            {
                return GenresList.Items.OrderBy(g=>g.GenreName).Aggregate((i, j) => new Genre { GenreName = (i.GenreName + ", " + j.GenreName) }).GenreName;
            }
        }

        private MovieGenres GenresList
        {
            get
            {
                if (_Genres == null) _Genres = new MovieGenres(_Movie);
                return _Genres;
            }
        }

        public MoviePictures Pictures
        {
            get
            {
                if (_Pictures == null) _Pictures = new MoviePictures(_Movie);
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

        public MoviePlays Views
        {
            get
            {
                if (_Views == null) _Views = new MoviePlays(_Movie);
                return _Views;
            }
        }

        public MovieCastings Casting
        {
            get
            {
                if (_Casting == null) _Casting = new MovieCastings(_Movie);
                return _Casting;
            }
        }

        public MovieSummaries Summary
        {
            get
            {
                if (_Summary == null) _Summary = new MovieSummaries(_Movie);
                return _Summary;
            }
        }

        public string Countries
        {
            get
            {
                return CountriesList.Items.OrderBy(c=>c.CountryName).Aggregate((i, j) => new Country { CountryName = (i.CountryName + ", " + j.CountryName) }).CountryName;
            }
        }

        public MovieCountries CountriesList
        {
            get
            {
                if (_Countries == null) _Countries = new MovieCountries(_Movie);
                return _Countries;
            }
        }

        public System.Collections.Generic.List<FilePlay> Plays
        {
            get
            {
                return Views.Items.Where(v => v.Finishedplay == true).ToList();
            }
        }

        public DateTime? LastPlayed()
        {
            var lLast = (from p in Plays orderby p.DatePlay descending select p).FirstOrDefault();
            return lLast?.DatePlay;
        }

        public string Director()
        {
            var lDir = (from c in Casting.Items where c.CastingType.Type.Equals("Director") orderby c.CastingType.Type descending, c.Ordinal select c);
            return lDir.OrderBy(g => g.Ordinal).Aggregate((i, j) => new MovieCasting { ArtistName  = (i.ArtistName + ", " + j.ArtistName) }).ArtistName;
        }

        public string Writer()
        {
            var lWri = (from c in Casting.Items where c.CastingType.Type.Equals("Writer") orderby c.CastingType.Type descending, c.Ordinal select c);
            return lWri.OrderBy(g => g.Ordinal).Aggregate((i, j) => new MovieCasting { ArtistName = (i.ArtistName + ", " + j.ArtistName) }).ArtistName;
        }

        public List<MovieCasting> ArtistCasting()
        {
            return (from c in Casting.Items where c.CastingType.Type.Equals("Actor") orderby c.CastingType.Type descending, c.Ordinal select c).ToList();
        }

        public decimal AveragedRate
        {
            get
            {
                decimal lRet = _Movie.Rating;
                //if(_Movie.UserRating!=null)
                //{
                //    lRet = (lRet + (2 * (decimal)_Movie.UserRating)) / 3;
                //}
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
