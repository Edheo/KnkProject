using KnkCore;
using KnkInterfaces.PropertyAtributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkSolutionMovies.Entities
{
    public class CastingName : KnkItem
    {
        #region Interface/Implementation
        public CastingName():base(new KnkTableEntity("vieCastingNames", "CastingNames"))
        {
        }
        #endregion Interface/Implementation

        #region Class Properties
        [AtributePrimaryKey]
        public KnkEntityIdentifier IdCastingName { get; set; }
        public KnkEntityReference<Casting> IdCasting { get; set; }
        public string Name { get; set; }
        #endregion Class Properties

        public Casting Casting { get { return IdCasting?.Value; } }

        public override string ToString()
        {
            return Name;
        }

    }
}
