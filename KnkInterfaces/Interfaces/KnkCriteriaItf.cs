using KnkInterfaces.Enumerations;
using System;
using System.Collections.Generic;

namespace KnkInterfaces.Interfaces
{
    public interface KnkCriteriaItf
    {
        string KnkLinkFields { get; }
        List<KnkParameterItf> GetParameters();
        KnkTableEntityItf EntityTable();
        KnkParameterItf AddParameter(Type aType, string aName, OperatorsEnu aOperator, object aValue);
        bool HasParameters();
    }

    public interface KnkCriteriaItf<Tdad, Tlst> : KnkCriteriaItf
        where Tdad : KnkItemItf, new()
        where Tlst : KnkItemItf, new()
    {
        KnkTableEntityRelationItf<Tdad> EntityRelation();
        KnkListItf<Tdad,Tlst> Parent { get; set; }
    }
}
