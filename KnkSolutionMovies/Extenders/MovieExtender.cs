using KnkInterfaces.Utilities;
using KnkSolutionMovies.Entities;
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
                return KnkInterfacesUtils.ConcatStrings(_Movie.Genres().Items.Select(g => g.ToString()).ToList());
            }
        }

        public string Summary
        {
            get
            {
                var lLst = (from sum in _Movie.Summary().Items orderby sum.IdSummary.Value select sum).ToList();
                return KnkInterfacesUtils.ConcatStrings(lLst.Select(g => g.ToString()).ToList(), true, Environment.NewLine);
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
                return KnkInterfacesUtils.ConcatStrings(_Movie.Countries().Items.Select(g => g.ToString()).ToList());
            }
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

        public string Director()
        {
            return KnkInterfacesUtils.ConcatStrings((from c in _Movie.Casting().Items where c.IdCastingType.Reference.Type.Equals("Director") orderby c.IdCastingType.Reference.Type descending, c.Ordinal select c.IdCasting.Reference.ArtistName).ToList());
        }

        public string Writer()
        {
            return KnkInterfacesUtils.ConcatStrings((from c in _Movie.Casting().Items where c.IdCastingType.Reference.Type.Equals("Writer") orderby c.IdCastingType.Reference.Type descending, c.Ordinal select c.IdCasting.Reference.ArtistName).ToList());
        }

        public List<MovieCasting> ArtistCasting()
        {
            return (from c in _Movie.Casting().Items where c.IdCastingType.Reference.Type.Equals("Actor") orderby c.IdCastingType.Reference.Type descending, c.Ordinal select c).ToList();
        }

        public TimeSpan? Duration()
        {
            if (_Movie.Seconds != null)
                return TimeSpan.FromSeconds((double)_Movie.Seconds);
            else
                return null;
        }

        public string ImdbUrl()
        {
            return $"http://www.imdb.com/title/{_Movie.ImdbId}/";
        }

        public string TmdbUrl()
        {
            return $"https://www.themoviedb.org/movie/{_Movie.TmdbId}/";
        }
        #endregion

    }
}
