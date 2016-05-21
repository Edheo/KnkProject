using KnkInterfaces.Interfaces;

namespace KnkCore
{
    public class KnkTableEntity : KnkTableEntityItf
    {
        public KnkTableEntity(string aTable, string aPrimaryKey)
            : this(aTable, aPrimaryKey, string.Empty)
        {
        }

        public KnkTableEntity(string aTable, string aPrimaryKey, string aRelatedKey)
        {
            SourceTable = aTable;
            PrimaryKey = aPrimaryKey;
            RelatedKey = aRelatedKey;
        }

        public string PrimaryKey { get; }
        public string SourceTable { get; }
        public string RelatedKey { get; }
    }
}
