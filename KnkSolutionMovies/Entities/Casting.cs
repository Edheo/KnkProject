using System;
using KnkCore;
using KnkInterfaces.Classes;

namespace KnkSolutionMovies.Entities
{
    public class Casting : KnkItemBase
    {
        #region Interface/Implementation
        public Casting():base(new KnkTableEntity("Casting", "IdCast"))
        {
        }
        #endregion Interface/Implementation

        #region Class Properties
        public KnkEntityIdentifier IdCast { get; set; }
        public string ArtistName { get; set; }
        #endregion Class Properties

        public override string ToString()
        {
            return ArtistName;
        }
    }
}
