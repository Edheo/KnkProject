namespace KnkInterfaces.Interfaces
{
    public interface KnkConnectionItf
    {
        KnkListItf<T> GetList<T>() where T : KnkItemItf, new();
        KnkListItf<Tlst> GetList<Tdad, Tlst>(KnkCriteriaItf<Tdad, Tlst> aCriteria)
            where Tdad : KnkItemItf, new()
            where Tlst : KnkItemItf, new();
        KnkListItf<T> FillList<T>(KnkListItf<T> aList) where T : KnkItemItf, new();
        KnkListItf<Tlst> FillList<Tdad, Tlst>(KnkListItf<Tlst> aList, KnkCriteriaItf<Tdad, Tlst> aCriteria)
            where Tdad : KnkItemItf, new()
            where Tlst : KnkItemItf, new();
        T GetItem<T>(int? aEntityId) where T : KnkItemItf, new();

        KnkReferenceItf<TDad, TReference> GetReference<TDad, TReference>(TDad aItem, string aProperty)
            where TDad : KnkItemItf
            where TReference : KnkItemItf, new();

        KnkReferenceItf<TDad, TReference> SetReference<TDad, TReference>(KnkReferenceItf<TDad, TReference> aReference, TDad aItem, string aProperty)
            where TDad : KnkItemItf
            where TReference : KnkItemItf, new();
    }
}
