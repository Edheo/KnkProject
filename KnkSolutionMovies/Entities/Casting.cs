﻿using System;
using KnkCore;
using KnkInterfaces.Classes;
using KnkInterfaces.PropertyAtributes;

namespace KnkSolutionMovies.Entities
{
    public class Casting : KnkItemBase
    {
        #region Interface/Implementation
        public Casting():base(new KnkTableEntity("Casting"))
        {
        }
        #endregion Interface/Implementation

        #region Class Properties
        [AtributePrimaryKey]
        public KnkEntityIdentifier IdCast { get; set; }
        public string ArtistName { get; set; }
        #endregion Class Properties

        public override string ToString()
        {
            return ArtistName;
        }
    }
}
