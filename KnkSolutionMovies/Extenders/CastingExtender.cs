using KnkSolutionMovies.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkSolutionMovies.Extenders
{
    public class CastingExtender
    {
        private readonly Casting _Casting;
        private MediaLink _MediaThumb;

        public CastingExtender(Casting aCasting)
        {
            _Casting = aCasting;
        }

        public MediaLink Poster
        {
            get
            {
                if (_MediaThumb == null)
                    _MediaThumb = (from p in _Casting.Pictures().Items where p.IdType == 1 select p).FirstOrDefault();
                if (_MediaThumb == null)
                    _MediaThumb = (from p in _Casting.Pictures().Items select p).FirstOrDefault();
                return _MediaThumb;
            }
        }

        public string ParsedId()
        {
            return _Casting.IdCasting.ToString().PadLeft(12, '0');
        }

    }
}
