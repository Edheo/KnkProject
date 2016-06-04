using KnkCore;
using KnkInterfaces.PropertyAtributes;

namespace KnkSolutionMovies.Entities
{
    public class Casting : KnkItem
    {
        #region Interface/Implementation
        public Casting():base(new KnkTableEntity("vieCasting", "Casting"))
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
