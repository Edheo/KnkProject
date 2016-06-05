using KnkCore;
using KnkInterfaces.PropertyAtributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkSolutionMovies.Entities
{
    public class Company : KnkItem
    {
        #region Interface/Implementation
        public Company():base(new KnkTableEntity("vieCompanies", "Companies"))
        {
        }
        #endregion Interface/Implementation

        #region Class Properties
        [AtributePrimaryKey]
        public KnkEntityIdentifier IdCompany { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public string HeadQuarters { get; set; }
        public string HomePage { get; set; }
        public Company IdParentCompany { get; set; }
        public string Logo { get; set; }

        #endregion Class Properties

        public override string ToString()
        {
            return CompanyName;
        }
    }
}
