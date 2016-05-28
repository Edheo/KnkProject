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
    public class CountryClass : KnkItemBase
    {
        #region Interface/Implementation
        public CountryClass():base(new KnkTableEntity("Countries"))
        {
        }
        #endregion Interface/Implementation

        #region Class Properties
        [AtributePrimaryKey]
        public KnkEntityIdentifier IdCountry { get; set; }
        public string Country { get; set; }
        #endregion Class Properties

        public override string ToString()
        {
            return Country;
        }
    }
}
