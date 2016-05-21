using System.Collections.Generic;

namespace KnkInterfaces.Interfaces
{
    public interface KnkCriteriaItf
    {
        string KnkLinkFields { get; }
        List<KnkParameterItf> FeededParameters();
        List<KnkParameterItf> GetParameters();
        string GetWhereFromParameters();
        KnkTableEntityItf EntityTable();
    }

    public interface KnkCriteriaItf<Tdad, Tlst> : KnkCriteriaItf
        where Tdad : KnkItemItf, new()
        where Tlst : KnkItemItf, new()
    {
        Tdad Parent { get; }

        List<KnkParameterItf> GetParameters(Tdad Item);

        string GetWhereFromParameters(Tdad Item);

    }
}
