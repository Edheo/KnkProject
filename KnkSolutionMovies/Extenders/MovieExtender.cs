using KnkSolutionMovies.Entities;
using KnkSolutionMovies.Lists;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KnkSolutionMovies.Extenders
{
    public class MovieExtender
    {
        private readonly Movie _Movie;
        private MediaLink _MediaThumb;

        public MovieExtender(Movie aMovie)
        {
            _Movie = aMovie;
        }

        #region Relationships

        public string Genres
        {
            get
            {
                return _Movie.Genres().Items.OrderBy(g=>g.GenreName).Aggregate((i, j) => new Genre { GenreName = (i.GenreName + ", " + j.GenreName) }).GenreName;
            }
        }

        public MediaLink Poster
        {
            get
            {
                if (_MediaThumb == null)
                    _MediaThumb = (from p in _Movie.Pictures().Items where p.IdType == 1 select p).FirstOrDefault();
                if (_MediaThumb == null)
                    _MediaThumb = (from p in _Movie.Pictures().Items select p).FirstOrDefault();
                return _MediaThumb;
            }
        }

        public string Countries
        {
            get
            {
                return _Movie.Countries().Items.OrderBy(c=>c.CountryName).Aggregate((i, j) => new Country { CountryName = (i.CountryName + ", " + j.CountryName) }).CountryName;
            }
        }

        public List<FilePlay> Views
        {
            get
            {
                return _Movie.Plays().Items.Where(v => v.Finishedplay == true).ToList();
            }
        }

        public DateTime? LastViewed()
        {
            var lLast = (from p in Views orderby p.DatePlay descending select p).FirstOrDefault();
            return lLast?.DatePlay;
        }

        public string Director()
        {
            var lDir = (from c in _Movie.Casting().Items where c.CastingType.Type.Equals("Director") orderby c.CastingType.Type descending, c.Ordinal select c);
            return lDir.OrderBy(g => g.Ordinal).Aggregate((i, j) => new MovieCasting { ArtistName  = (i.ArtistName + ", " + j.ArtistName) }).ArtistName;
        }

        public string Writer()
        {
            var lWri = (from c in _Movie.Casting().Items where c.CastingType.Type.Equals("Writer") orderby c.CastingType.Type descending, c.Ordinal select c);
            return lWri.OrderBy(g => g.Ordinal).Aggregate((i, j) => new MovieCasting { ArtistName = (i.ArtistName + ", " + j.ArtistName) }).ArtistName;
        }

        public List<MovieCasting> ArtistCasting()
        {
            return (from c in _Movie.Casting().Items where c.CastingType.Type.Equals("Actor") orderby c.CastingType.Type descending, c.Ordinal select c).ToList();
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
