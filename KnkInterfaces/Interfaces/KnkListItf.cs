using KnkInterfaces.Enumerations;
using System.Collections.Generic;
using System.Data;

namespace KnkInterfaces.Interfaces
{
    public interface KnkListItf
    {
        KnkConnectionItf Connection { get; set; }
        int Count();
        bool SaveChanges();
        bool SaveChanges(UpdateStatusEnu aStatus);
        void Refresh();
    }

    public interface KnkListItf<Tdad, Tlst> : KnkListItf
        where Tdad : KnkItemItf, new()
        where Tlst : KnkItemItf, new()
    {
        List<Tlst> FillFromList(List<Tlst> aList);
        List<Tlst> Items { get; }
        List<Tlst> Datasource();
        KnkCriteriaItf<Tdad, Tlst> GetCriteria();
        List<KnkEntityIdentifierItf> GetListIds();
        List<KnkEntityIdentifierItf> GetListIds(List<Tlst> aItems);
        Tlst Create();
        void Add(Tlst aItem);
        bool SaveChanges(List<Tlst> aList);
        bool SaveChanges(List<Tlst> aList, UpdateStatusEnu aStatus);
    }
}
