using KnkSolutionMovies.Entities;
using KnkSolutionMovies.References;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkSolutionMovies.Extenders
{
    public class MovieCastingExtender
    {
        private readonly MovieCasting _MovieCasting;
        public MovieCastingExtender(MovieCasting aMovieCasting)
        {
            _MovieCasting = aMovieCasting;
        }

        CastingTypeReference _CastingTypeReference = null;

        #region Relationships
        private CastingTypeReference CastingTypeReference()
        {
            if (_CastingTypeReference == null)
                _CastingTypeReference = new CastingTypeReference(_MovieCasting, "IdCastingType");
            return _CastingTypeReference;
        }
        #endregion Relationships

        public CastingType CastingType
        {
            get
            {
                return CastingTypeReference().Value;
            }
        }

    }
}
