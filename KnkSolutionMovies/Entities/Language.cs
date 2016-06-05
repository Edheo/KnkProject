using KnkCore;
using KnkInterfaces.PropertyAtributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkSolutionMovies.Entities
{
    public class Language : KnkItem
    {
        #region Interface/Implementation
        public Language():base(new KnkTableEntity("vieLanguages", "Languages"))
        {
        }
        #endregion Interface/Implementation

        #region Class Properties
        [AtributePrimaryKey]
        public KnkEntityIdentifier IdLanguage { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        #endregion Class Properties

        public override string ToString()
        {
            return Name;
        }
    }
}
