using KnkCore;
using KnkSolutionMovies.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkSolutionMovies.References
{
    class CastingTypeReference : KnkEntityIdentifier<MovieCasting, CastingType>
    {
        public CastingTypeReference(MovieCasting aMovieCasting, string aProperty) 
        : base(aMovieCasting, aProperty, aMovieCasting.Connection.GetItem<CastingType>)
        {
        }

        public string Text
        {
            get
            {
                return this.ToString();
            }
        }

        public override string ToString()
        {
            return this.Value.Type;
        }
    }
}
