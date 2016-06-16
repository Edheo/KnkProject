using KnkCore;
using KnkInterfaces.PropertyAtributes;

namespace KnkSolutionMovies.Entities
{
    public class LinkType : KnkItem
    {
        #region Interface/Implementation
        public LinkType():base(new KnkTableEntity("vieLinkTypes", "LinkTypes"))
        {
        }
        #endregion Interface/Implementation

        #region Class Properties
        [AtributePrimaryKey]
        public KnkEntityIdentifier IdType { get; set; }
        public string Type { get; set; }
        #endregion Class Properties

        public override string ToString()
        {
            return Type;
        }
    }
}
