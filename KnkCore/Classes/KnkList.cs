using KnkInterfaces.Utilities;
using KnkInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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
    }


}
