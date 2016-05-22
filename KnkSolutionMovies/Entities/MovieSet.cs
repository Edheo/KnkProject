﻿using KnkCore;
using KnkInterfaces.Classes;

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
        public KnkEntityIdentifier IdSet { get; set; }
        public string Name { get; set; }
        public string Overview { get; set; }
        #endregion Class Properties
    }
}
