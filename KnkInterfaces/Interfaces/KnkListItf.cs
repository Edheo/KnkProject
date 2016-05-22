using System.Collections.Generic;
using System.Data;

namespace KnkInterfaces.Interfaces
{
    public interface KnkListItf
    {
        KnkConnectionItf Connection { get; set; }
        int Count();
    }

    public interface KnkListItf<Tdad, Tlst> : KnkListItf
        where Tdad : KnkItemItf, new()
        where Tlst : KnkItemItf, new()
    {
        List<Tlst> FillFromList(List<Tlst> aList);
        List<Tlst> Items { get; }
        List<Tlst> Datasource();
        KnkCriteriaItf<Tdad, Tlst> GetCriteria();
    }
}
