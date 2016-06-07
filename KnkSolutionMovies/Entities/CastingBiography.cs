using KnkCore;
using KnkInterfaces.PropertyAtributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkSolutionMovies.Entities
{
    public class CastingBiography : KnkItem
    {
        #region Interface/Implementation
        public CastingBiography():base(new KnkTableEntity("vieCastingBiographies", "CastingBiographies"))
        {
        }
        #endregion Interface/Implementation

        #region Class Properties
        [AtributePrimaryKey]
        public KnkEntityIdentifier IdCastingBiography { get; set; }
        public KnkEntityReference<Casting> IdCasting { get; set; }
        public int Ordinal { get; set; }
        public string Text { get; set; }
        #endregion Class Properties

        public Casting Casting { get { return IdCasting?.Value; } }

        public override string ToString()
        {
            return $"{Text}{Environment.NewLine}";
        }
    }
}
