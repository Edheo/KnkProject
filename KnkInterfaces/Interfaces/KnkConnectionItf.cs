using KnkInterfaces.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkInterfaces.Interfaces
{
    public interface KnkConnectionItf
    {
        KnkListItf<T> GetList<T>() where T : KnkItemItf, new();
        KnkListItf<Tlst> GetList<Tdad,Tlst>(KnkCriteriaItf<Tdad, Tlst> aCriteria) 
            where Tdad : KnkItemItf, new()
            where Tlst : KnkItemItf, new();
        KnkListItf<T> FillList<T>(KnkListItf<T> aList) where T : KnkItemItf, new();
        KnkListItf<Tlst> FillList<Tdad, Tlst>(KnkListItf<Tlst> aList, KnkCriteriaItf<Tdad, Tlst> aCriteria)
            where Tdad : KnkItemItf, new()
            where Tlst : KnkItemItf, new();
        T GetItem<T>(int? aEntityId) where T : KnkItemItf, new();

        KnkReferenceItf<T> GetReference<T>(int? aValue) where T : KnkItemItf, new();
        KnkReferenceItf<T> SetReference<T>(KnkReferenceItf<T> aReference, int? aValue) where T : KnkItemItf, new();
    }
}
