using KnkInterfaces.Utilities;
using KnkSolutionMovies.Entities;
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

        public string Director()
        {
            return KnkInterfacesUtils.ConcatStrings((from c in _Movie.Casting().Items where c.CastingType.Type.Equals("Director") orderby c.CastingType.Type descending, c.Ordinal select c.Casting.ArtistName).ToList());
        }

        public string Writer()
        {
            return KnkInterfacesUtils.ConcatStrings((from c in _Movie.Casting().Items where c.CastingType.Type.Equals("Writer") orderby c.CastingType.Type descending, c.Ordinal select c.Casting.ArtistName).ToList());
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
