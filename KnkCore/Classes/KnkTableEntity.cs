using KnkInterfaces.Interfaces;

namespace KnkCore
{
    public class KnkTableEntity : KnkTableEntityItf
    {
        public KnkTableEntity(string aTable):this(aTable,aTable)
        {
        }

        public KnkTableEntity(string aTable, string aTableBase)
        {
            SourceTable = aTable;
            TableBase = aTableBase;
        }

        public string SourceTable { get; }
        public string TableBase { get; }
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
