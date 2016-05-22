namespace KnkInterfaces.Interfaces
{
    public interface KnkConnectionItf
    {
        KnkListItf<Tdad,Tlst> GetList<Tdad,Tlst>()
            where Tdad : KnkItemItf, new()
            where Tlst : KnkItemItf, new();
        KnkListItf<Tdad, Tlst> GetList<Tdad, Tlst>(KnkCriteriaItf<Tdad, Tlst> aCriteria)
            where Tdad : KnkItemItf, new()
            where Tlst : KnkItemItf, new();
        KnkListItf<Tdad, Tlst> FillList<Tdad, Tlst>(KnkListItf<Tdad, Tlst> aList)
            where Tdad : KnkItemItf, new()
            where Tlst : KnkItemItf, new();
        KnkListItf<Tdad, Tlst> FillList<Tdad, Tlst>(KnkListItf<Tdad, Tlst> aList, KnkCriteriaItf<Tdad, Tlst> aCriteria)
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
