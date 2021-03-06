﻿using KnkInterfaces.Interfaces;
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
        List<KnkChangeDescriptorItf> _Messages = new List<KnkChangeDescriptorItf>();

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
                    FillFromList(Connection.GetList(Criteria).Items);
                }
                return _List;
            }
        }

        public string SortProperty { get; set; }

        public bool SortDirectionAsc { get; set; }

        public List<KnkChangeDescriptorItf> Messages
        {
            get
            {
                return _Messages;
            }

            set
            {
                _Messages = value;
            }
        }

        public virtual List<Tlst> Datasource()
        {
            List<Tlst> lRet = new List<Tlst>();
            if (!string.IsNullOrEmpty(SortProperty))
            {
                if(SortDirectionAsc)
                {
                    lRet = (from itm in Items orderby itm.PropertyGet(SortProperty) select itm).ToList();
                }
                else
                {
                    lRet = (from itm in Items orderby itm.PropertyGet(SortProperty) descending select itm).ToList();
                }
            }
            else
                lRet = Items;
            return lRet;
        }

        public KnkCriteriaItf<Tdad, Tlst> Criteria
        {
            get
            {
                if (_Criteria == null)
                {
                    KnkItemItf lDad = new Tdad();
                    lDad.SetParent(this);
                    _Criteria = new KnkCriteria<Tdad, Tlst>(this);
                }
                return _Criteria;
            }
            set
            {
                _Criteria = value;
                _Criteria.Parent = this;
            }
        }

        public List<KnkEntityIdentifierItf> GetListIds()
        {
            if (_List == null)
                return Connection.GetListIds(Criteria);
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

        public bool SaveChanges(KnkItemItf aItem)
        {
            var lChanges = new List<Tlst>();
            lChanges.Add((Tlst)aItem);
            return SaveChanges(lChanges);
        }

        public bool SaveChanges(List<Tlst> aList)
        {
            var lChanges = (from itm in aList where itm.Status()!=UpdateStatusEnu.NoChanges select itm).ToList();
            Connection.SaveData(lChanges);
            Refresh();
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

        public KnkChangeDescriptorItf AddMessage(KnkItemItf aItem)
        {
            return AddMessage(new KnkChangeDescriptor(aItem));
        }

        public KnkChangeDescriptorItf UpdateMessage(KnkItemItf aItem, string aMessage)
        {
            KnkItemItf lItm = aItem;
            var lFound = Messages?.Find(m => m.Item == lItm);
            if (Messages != null)
            {
                if(lFound==null)
                {
                    lFound = AddMessage(aItem);
                }
                lFound.UpdateMessage(aItem.Status().ToString(), aMessage);
            }
            return lFound;
        }

        public KnkChangeDescriptorItf AddMessage(KnkChangeDescriptorItf aMessage)
        {
            if (Messages != null)
            {
                Messages.Add(aMessage);
                return aMessage;
            }
            return null;
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
