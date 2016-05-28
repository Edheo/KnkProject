using KnkInterfaces.Interfaces;

namespace KnkCore
{
    public class KnkTableEntity : KnkTableEntityItf
    {
        public KnkTableEntity(string aTable)
        {
            SourceTable = aTable;
        }
        public string SourceTable { get; }
    }

    public class KnkTableEntityRelation<T> : KnkTableEntity, KnkTableEntityRelationItf<T>
        where T : KnkItemItf, new()
    {
        public KnkTableEntityRelation(string aTable) : this(aTable, new T().PrimaryKey())
        {
        }

        public KnkTableEntityRelation(string aTable, string aRelatedKey):base(aTable)
        {
            RelatedKey = aRelatedKey;
        }

        public string RelatedKey { get; }
    }
}
