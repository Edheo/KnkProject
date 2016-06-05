using KnkCore;
using KnkInterfaces.Interfaces;
using KnkInterfaces.PropertyAtributes;

namespace KnkSolutionMovies.Entities
{
    public class Casting : KnkItem
    {
        KnkEntityRelation<Casting, MediaLink> _Pictures;

        #region Interface/Implementation
        public Casting():base(new KnkTableEntity("vieCasting", "Casting"))
        {
        }
        #endregion Interface/Implementation

        #region Class Properties
        [AtributePrimaryKey]
        public KnkEntityIdentifier IdCasting { get; set; }
        public string ArtistName { get; set; }
        #endregion Class Properties

        public KnkEntityRelationItf<Casting, MediaLink> Pictures()
        {
            if (_Pictures == null) _Pictures = new KnkEntityRelation<Casting, MediaLink>(this, "vieCastingLinks");
            return _Pictures;
        }


        public override string ToString()
        {
            return ArtistName;
        }
    }
}
