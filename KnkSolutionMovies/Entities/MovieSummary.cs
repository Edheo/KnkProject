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
    public class MovieSummary : KnkItemBase
    {
        #region Interface/Implementation
        public MovieSummary():base(new KnkTableEntity("vieMovieSummaries", "MovieSummaries"))
        {
        }
        #endregion Interface/Implementation

        #region Class Properties
        [AtributePrimaryKey]
        public KnkEntityIdentifier IdSummary { get; set; }
        public KnkEntityReference<Movie> IdMovie { get; set; }
        public int Ordinal { get; set; }
        public string SummaryItem { get; set; }
        #endregion Class Properties

        public override string ToString()
        {
            return $"{SummaryItem}{Environment.NewLine}";
        }
    }
}
