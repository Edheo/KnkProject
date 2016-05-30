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
    public class Country : KnkItemBase
    {
        #region Interface/Implementation
        public Country():base(new KnkTableEntity("vieCountries", "Countries"))
        {
        }
        #endregion Interface/Implementation

        #region Class Properties
        [AtributePrimaryKey]
        public KnkEntityIdentifier IdCountry { get; set; }
        public string CountryName { get; set; }
        #endregion Class Properties

        public override string ToString()
        {
            return CountryName;
        }
    }
}
