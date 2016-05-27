using KnkCore;
using KnkInterfaces.Classes;
using KnkSolutionMovies.Extenders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkSolutionMovies.Entities
{
    public class MovieCasting : KnkItemBase
    {
        public readonly MovieCastingExtender Extender;

        #region Interface/Implementation
        public MovieCasting():base(new KnkTableEntity("vieMovieCasting", "IdMovieCasting"))
        {
            Extender = new MovieCastingExtender(this);
        }
        #endregion Interface/Implementation

        #region Class Properties
        public KnkEntityIdentifier IdMovieCasting { get; set; }
        public KnkEntityIdentifier IdMovie { get; set; }
        public KnkEntityIdentifier IdCast { get; set; }
        public KnkEntityIdentifier IdCastingType { get; set; }
        public int Ordinal { get; set; }
        public string Role { get; set; }
        public string ArtistName { get; set; }
        #endregion Class Properties

        public override string ToString()
        {
            return $"{ArtistName} ({Role}";
        }
    }
}
