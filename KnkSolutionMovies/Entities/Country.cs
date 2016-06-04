using KnkCore;
using KnkInterfaces.PropertyAtributes;

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
