using KnkInterfaces.Enumerations;
using System;
using System.Collections.Generic;

namespace KnkInterfaces.Interfaces
{
    public interface KnkCriteriaItf
    {
        string KnkLinkFields { get; }
        List<KnkParameterItf> GetParameters();
        string GetWhereFromParameters();
        KnkTableEntityItf EntityTable();
        KnkParameterItf AddParameter(Type aType, string aName, OperatorsEnu aOperator, object aValue);
        bool HasParameters();
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
