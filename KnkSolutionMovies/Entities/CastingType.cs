using KnkCore;
using KnkInterfaces.PropertyAtributes;

namespace KnkSolutionMovies.Entities
{
    public class CastingType : KnkItem
    {
        #region Interface/Implementation
        public CastingType():base(new KnkTableEntity("vieCastingType", "CastingType"))
        {
        }
        #endregion Interface/Implementation

        #region Class Properties
        [AtributePrimaryKey]
        public KnkEntityIdentifier IdCastingType { get; set; }
        public string Type { get; set; }
        #endregion Class Properties

        public override string ToString()
        {
            return Type;
        }
    }
}
