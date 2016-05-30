using KnkCore;
using KnkInterfaces.Classes;
using KnkInterfaces.PropertyAtributes;
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
        public CastingType():base(new KnkTableEntity("vieCasting", "Casting"))
        {
        }
        #endregion Interface/Implementation

        #region Class Properties
        [AtributePrimaryKey]
        public KnkEntityIdentifier IdCastingType { get; set; }
        public string Type { get; set; }
        #endregion Class Properties

        public override string ToString()
        {
            return Type;
        }
    }
}
