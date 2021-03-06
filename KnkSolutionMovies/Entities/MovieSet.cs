﻿using KnkCore;
using KnkInterfaces.PropertyAtributes;

namespace KnkSolutionMovies.Entities
{
    public class MovieSet : KnkItem
    {
        #region Interface/Implementation
        public MovieSet():base(new KnkTableEntity("vieMovieSets", "MovieSets"))
        {
        }
        #endregion Interface/Implementation

        #region Class Properties
        [AtributePrimaryKey]
        public KnkEntityIdentifier IdSet { get; set; }
        public string Name { get; set; }
        public string Overview { get; set; }
        #endregion Class Properties

        public override string ToString()
        {
            return Name;
        }

    }
}
