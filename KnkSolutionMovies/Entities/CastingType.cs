using KnkCore;
using KnkInterfaces.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkSolutionMovies.Entities
{
    public class CastingType : KnkItemBase
    {
        #region Interface/Implementation
        public CastingType():base(new KnkTableEntity("CastingType", "IdCastingType"))
        {
        }
        #endregion Interface/Implementation

        #region Class Properties
        public KnkEntityIdentifier IdCastingType { get; set; }
        public string Type { get; set; }
        #endregion Class Properties

        public override string ToString()
        {
            return Type;
        }
    }
}
