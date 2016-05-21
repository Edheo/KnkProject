using KnkCore;
using KnkInterfaces.Interfaces;
using System;

namespace KnkSolutionMovies.Entities
{
    public class MovieSet : KnkItemBase
    {
        #region Interface/Implementation
        public MovieSet():base(new KnkTableEntity("MovieSets", "IdSet"))
        {
        }
        #endregion Interface/Implementation

        #region Class Properties
        public int? IdSet { get; set; }
        public string Name { get; set; }
        public string Overview { get; set; }
        #endregion Class Properties
    }
}
