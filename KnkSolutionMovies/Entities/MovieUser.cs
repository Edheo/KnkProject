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
    public class MovieUser : KnkItemBase
    {
        #region Interface/Implementation
        public MovieUser():base(new KnkTableEntity("vieMovieUsers", "MovieUsers"))
        {
        }
        #endregion Interface/Implementation

        #region Class Properties
        [AtributePrimaryKey]
        public KnkEntityIdentifier IdMovieUser { get; set; }
        public KnkEntityIdentifier IdMovie { get; set; }
        public KnkEntityIdentifier IdUser { get; set; }
        decimal? UserRating { get; set; }
        #endregion Class Properties

        public override string ToString()
        {
            return UserRating.ToString();
        }
    }
}
