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
                lDad.Parent = this;
                _Criteria = new KnkCriteria<Tdad, Tlst>((Tdad)lDad);
            }
            return _Criteria;
        }

        public List<KnkEntityIdentifierItf> GetListIds()
        {
            return Connection.GetListIds(GetCriteria());
        }

        public void Add(Tlst aItem)
        {
            aItem.Update();
            this.Items.Add(aItem);
        }

        public bool SaveChanges()
        {
            var lChanges = (from itm in Items where itm.Status() != UpdateStatusEnu.NoChanges select itm);
            foreach(var lItm in lChanges)
            {

            }
            return true;
        }
    }


}
