using KnkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkSolutionMovies.Entities
{
    public class GenreClass : KnkItemBase
    {
        #region Interface/Implementation
        public GenreClass():base()
        {
            SourceEntity.SourceTable = "Genres";
            SourceEntity.PrimaryKey = "IdGenre";
        }
        #endregion Interface/Implementation

        #region Class Properties
        public int? IdGenre { get; set; }

        public string Genre { get; set; }
        #endregion Class Properties
    }
}
