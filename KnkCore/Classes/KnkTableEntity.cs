using KnkInterfaces.Interfaces;

namespace KnkCore
{
    public class KnkTableEntity : KnkTableEntityItf
    {
        public KnkTableEntity(string aTable, string aPrimaryKey)
        {
            SourceTable = aTable;
            PrimaryKey = aPrimaryKey;
        }


        public string PrimaryKey { get; }
        public string SourceTable { get; }
    }

    public class KnkTableEntityRelation<T> : KnkTableEntity, KnkTableEntityRelationItf<T>
        where T : KnkItemItf, new()
    {
        public KnkTableEntityRelation(string aTable, string aPrimaryKey, string aRelatedKey):base(aTable,aPrimaryKey)
        {
            RelatedKey = aRelatedKey;
        }

        public string RelatedKey { get; }
    }
}
