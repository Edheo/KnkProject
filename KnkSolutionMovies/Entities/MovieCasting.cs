using KnkCore;
using KnkInterfaces.Classes;
using KnkInterfaces.PropertyAtributes;
using KnkSolutionMovies.Extenders;
using KnkSolutionMovies.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkSolutionMovies.Entities
{
    public class MovieCasting : KnkItemBase
    {
        #region Interface/Implementation
        public MovieCasting():base(new KnkTableEntity("vieMovieCasting", "MovieCasting"))
        {
        }
        #endregion Interface/Implementation

        #region Class Properties
        [AtributePrimaryKey]
        public KnkEntityIdentifier IdMovieCasting { get; set; }
        public KnkEntityIdentifier IdMovie { get; set; }
        public KnkEntityIdentifier IdCast { get; set; }
        public KnkEntityReference<CastingType> IdCastingType { get; set; }
        public int Ordinal { get; set; }
        public string Role { get; set; }
        public string ArtistName { get; set; }
        #endregion Class Properties

        public override string ToString()
        {
            return $"{ArtistName} ({Role})";
        }

        public CastingType CastingType
        {
            get
            {
                return IdCastingType?.Value;
            }
            set
            {
                IdCastingType.Value = value;
            }
        }

    }
}
