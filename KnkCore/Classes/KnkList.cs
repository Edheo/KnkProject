using KnkInterfaces.Utilities;
using KnkInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using KnkInterfaces.Enumerations;

namespace KnkCore
{
    [Serializable]
    public class KnkList<Tdad, Tlst> : KnkListItf<Tdad, Tlst>
        where Tdad : KnkItemItf, new()
        where Tlst : KnkItemItf, new()
    {
        public KnkConnectionItf Connection { get; set; }
        KnkCriteriaItf<Tdad, Tlst> _Criteria;
        List<Tlst> _List = null;

        public KnkList(KnkConnectionItf aConnection)
        {
            Connection = aConnection;
        }

        public KnkList(KnkConnectionItf aConnection, List<Tlst> aList) : this(aConnection)
        {
            FillFromList(aList);
        }

        public KnkList(KnkConnectionItf aConnection, KnkCriteriaItf<Tdad, Tlst> aCriteria) : this(aConnection)
        {
            _Criteria = aCriteria;
        }

        public int Count()
        {
            return Items.Count;
        }

        public List<Tlst> FillFromList(List<Tlst> aList)
        {
            _List = aList;
            return _List;
        }

        public List<Tlst> Items
        {
            get
            {
                if(_List==null)
                {
                    FillFromList(Connection.GetList(GetCriteria()).Items);
                }
                return _List;
            }
        }

        public virtual List<Tlst> Datasource()
        {
            return Items;
        }

        public KnkCriteriaItf<Tdad, Tlst> GetCriteria()
        {
            if (_Criteria == null)
            {
                KnkItemItf lDad = new Tdad();
                lDad.SetParent(this);
                _Criteria = new KnkCriteria<Tdad, Tlst>((Tdad)lDad);
            }
            return _Criteria;
        }

        public List<KnkEntityIdentifierItf> GetListIds()
        {
            if (_List == null)
                return Connection.GetListIds(GetCriteria());
            else
                return GetListIds(Items);
        }

        public List<KnkEntityIdentifierItf> GetListIds(List<Tlst> aItems)
        {
            var lLst = aItems.Select(itm => itm.PropertyGet(itm.PrimaryKey()) as KnkEntityIdentifierItf);
            return lLst.ToList();
        }

        public void Add(Tlst aItem, string aMessage)
        {
            aItem.Update(aMessage);
            this.Items.Add(aItem);
        }

        public bool SaveChanges()
        {
            bool lRet = SaveChanges(ItemsChanged());
            if (lRet) Refresh();
            return lRet;
        }

        public bool SaveChanges(List<Tlst> aList)
        {
            return SaveChanges(aList, UpdateStatusEnu.NoChanges);
        }

        public bool SaveChanges(UpdateStatusEnu aStatus)
        {
            bool lRet = SaveChanges(Items, aStatus);
            if (lRet) Refresh();
            return lRet;
        }

        public bool SaveChanges(List<Tlst> aList, UpdateStatusEnu aStatus)
        {
            var lChanges = (from itm in aList where (itm.Status() == aStatus || aStatus == UpdateStatusEnu.NoChanges) select itm).ToList();
            Connection.SaveData<Tlst>(lChanges);
            return true;
        }

        public List<Tlst> ItemsChanged()
        {
            return ItemsChanged(Items);
        }

        public List<Tlst> ItemsChanged(List<Tlst> aList)
        {
            return (from itm in aList where itm.Status() != UpdateStatusEnu.NoChanges select itm).ToList();
        }

        public List<KnkChangeDescriptorItf> ListOfChanges()
        {
            return ListOfChanges(Items);
        }

        public List<KnkChangeDescriptorItf> ListOfChanges(List<Tlst> aList)
        {
            List<KnkChangeDescriptorItf> lRet = (from itm in aList where !string.IsNullOrEmpty(itm.UpdateMessage()) select new KnkChangeDescriptor(itm)).Cast<KnkChangeDescriptorItf>().ToList();
            return lRet;
        }

        public List<KnkChangeDescriptorItf> ItemsDescribed()
        {
            return ItemsDescribed(Items);
        }

        public List<KnkChangeDescriptorItf> ItemsDescribed(List<Tlst> aList)
        {
            List<KnkChangeDescriptorItf> lRet = (from itm in aList select new KnkChangeDescriptor(itm)).Cast<KnkChangeDescriptorItf>().ToList();
            return lRet;
        }


        public void Refresh()
        {
            _List = null;
        }

        public Tlst Create()
        {
            return Create(true);
        }

        public Tlst Create(bool aAddToList)
        {
            Tlst lItem = new Tlst();
            lItem.SetParent(this);
            if (aAddToList)
            {
                Items.Add(lItem);
            }
            return lItem;
        }

        public void DeleteAll(string aMessage)
        {
            foreach(var lItm in Items)
            {
                lItm.Delete(aMessage);
            }
        }
    }

    public class KnkList<Tlst> : KnkList<Tlst, Tlst>
        where Tlst : KnkItemItf, new()
    {
        public KnkList(KnkConnectionItf aConnection) : base(aConnection)
        {
        }

        public KnkList(KnkConnectionItf aConnection, List<Tlst> aList) : base(aConnection, aList)
        {
        }

        public KnkList(KnkConnectionItf aConnection, KnkCriteriaItf<Tlst, Tlst> aCriteria) : base(aConnection, aCriteria)
        {
        }
    }

}
