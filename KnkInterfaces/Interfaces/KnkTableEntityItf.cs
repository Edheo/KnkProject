namespace KnkInterfaces.Interfaces
{
    public interface KnkTableEntityItf
    {
        string SourceTable { get; }
    }

    public interface KnkTableEntityRelationItf<T>: KnkTableEntityItf
        where T : KnkItemItf, new()
    {
        string RelatedKey { get; }
    }
}
