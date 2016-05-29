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

        public void Add(Tlst aItem)
        {
            aItem.Update();
            this.Items.Add(aItem);
        }

        public bool SaveChanges()
        {
            bool lRet = SaveChanges(Items);
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
            var lChanges = (from itm in aList where itm.Status() != UpdateStatusEnu.NoChanges && (itm.Status() == aStatus || aStatus==UpdateStatusEnu.NoChanges )select itm).ToList();
            Connection.SaveData<Tlst>(lChanges);
            return true;
        }

        public void Refresh()
        {
            _List = null;
        }

        public Tlst Create()
        {
            Tlst lItem = new Tlst();
            lItem.SetParent(this);
            return lItem;
        }

    }


}
