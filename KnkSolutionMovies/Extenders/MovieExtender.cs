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
                var lAux = _Movie.Genres().Items.Select(g => g.ToString());
                return lAux.OrderBy(g=>g).Aggregate((i, j) => $"{i}, {j}");
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
                var lAux = _Movie.Countries().Items.Select(g => g.ToString());
                return lAux.OrderBy(g => g).Aggregate((i, j) => $"{i}, {j}");
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
            string lRet = string.Empty;
            var lDir = (from c in _Movie.Casting().Items where c.CastingType.Type.Equals("Director") orderby c.CastingType.Type descending, c.Ordinal select c.Casting.ArtistName);
            if (lDir.Count() > 0)
                lRet = lDir.OrderBy(g => g).Aggregate((i, j) => $"{i}, {j}");
            return lRet;
        }

        public string Writer()
        {
            string lRet = string.Empty;
            var lWri = (from c in _Movie.Casting().Items where c.CastingType.Type.Equals("Writer") orderby c.CastingType.Type descending, c.Ordinal select c.Casting.ArtistName);
            if (lWri.Count() > 0)
                lRet = lWri.OrderBy(g => g).Aggregate((i, j) => $"{i}, {j}");
            return lRet;
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
