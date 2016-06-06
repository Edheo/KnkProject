using KnkInterfaces.Enumerations;
using System.Collections.Generic;
using System.Data;

namespace KnkInterfaces.Interfaces
{
    public interface KnkListItf
    {
        KnkConnectionItf Connection { get; set; }
        int Count();
        void DeleteAll(string aMessage);
        bool SaveChanges();
        bool SaveChanges(UpdateStatusEnu aStatus);
        void Refresh();

        string SortProperty { get; set; }
        bool SortDirectionAsc { get; set; }
    }

    public interface KnkListItf<Tdad, Tlst> : KnkListItf
        where Tdad : KnkItemItf, new()
        where Tlst : KnkItemItf, new()
    {
        List<Tlst> FillFromList(List<Tlst> aList);

        List<Tlst> Items { get; }
        List<Tlst> ItemsChanged();
        List<Tlst> ItemsChanged(List<Tlst> aList);
        List<KnkChangeDescriptorItf> ListOfChanges();
        List<KnkChangeDescriptorItf> ListOfChanges(List<Tlst> aList);
        List<KnkChangeDescriptorItf> ItemsDescribed();
        List<KnkChangeDescriptorItf> ItemsDescribed(List<Tlst> aList);

        List<Tlst> Datasource();
        KnkCriteriaItf<Tdad, Tlst> GetCriteria();
        List<KnkEntityIdentifierItf> GetListIds();
        List<KnkEntityIdentifierItf> GetListIds(List<Tlst> aItems);
        Tlst Create();
        Tlst Create(bool aAddToList);
        void Add(Tlst aItem, string aMessage);

        bool SaveChanges(List<Tlst> aList);
        bool SaveChanges(List<Tlst> aList, UpdateStatusEnu aStatus);
    }
}
