namespace KnkInterfaces.Interfaces
{
    public interface KnkTableEntityItf
    {
        string PrimaryKey { get; }
        string SourceTable { get; }
        string RelatedKey { get; }
    }
}
