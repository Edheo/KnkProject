using KnkInterfaces.Utilities;
using KnkInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkCore
{
    public class KnkCriteria<Tdad, Tlst> : KnkCriteriaItf<Tdad, Tlst> 
        where Tdad :KnkItemItf, new()
        where Tlst : KnkItemItf, new()
    {
        private Tdad _item;
        private string _entitysource;

        public KnkCriteria(Tdad aItem):this(aItem, aItem.SourceEntity.PrimaryKey)
        {
        }

        public KnkCriteria(Tdad aItem, string aFields) : this(aItem, aFields, (new Tlst()).SourceEntity.SourceTable)
        {
        }

        public KnkCriteria(Tdad aItem, string aFields, string aSource)
        {
            _item = aItem;
            _entitysource = aSource;
            KnkLinkFields = aFields;
        }

        public string KnkLinkFields { get; set; }

        public Tdad Parent
        {
            get
            {
                return _item;
            }
        }

        public string GetWhereFromParameters()
        {
            return GetWhereFromParameters(_item);
        }

        public string GetWhereFromParameters(Tdad aItem)
        {
            _item = aItem;
            return GetWhereFromParameters(aItem, "And");
        }

        public string GetWhereFromParameters(Tdad aItem, string aConnector)
        {
            string lWhere = GetParameters(aItem).Select(p => p.ToSqlWhere()).Aggregate((i, j) => i + " " + aConnector + " " + j);
            if (lWhere.Length > 0)
                lWhere = " Where " + lWhere;
            return lWhere;
        }

        private List<string> ParametersList()
        {
            return KnkLinkFields.Split('.').Select(p => p.Trim()).ToList();
        }

        public List<KnkParameterItf> GetParameters()
        {
            return GetParameters(_item);
        }

        public List<KnkParameterItf> GetParameters(Tdad Item)
        {
            var lPrs = KnkUtility.GetProperties<Tlst>();
            var lParameters = new List<KnkParameterItf>();

            foreach (string lPar in ParametersList())
            {
                var lPrp = lPrs.Where(p => p.Name.ToLower().Equals(lPar.ToLower())).FirstOrDefault();
                if(lPrp!=null)
                {
                    var lType = KnkUtility.GetPropertyType(lPrp);
                    var lName = lPrp.Name;
                    var lValue = Item.PropertyGet(lPrp.Name);
                    KnkParameter lKnkPar = new KnkParameter(lType, lName, lValue);
                    lParameters.Add(lKnkPar);
                }
            }
            if(lParameters.Count==0)
            {
                //At least we will link by parent key
                var lPrp = KnkUtility.GetProperties<Tdad>().Where(p => p.Name.Equals(Item.SourceEntity.PrimaryKey)).FirstOrDefault();
                if (lPrp != null)
                {
                    var lType = KnkUtility.GetPropertyType(lPrp);
                    var lName = lPrp.Name;
                    var lValue = Item.PropertyGet(lPrp.Name);
                    KnkParameter lKnkPar = new KnkParameter(lType, lName, lValue);
                    lParameters.Add(lKnkPar);
                }

            }
            return lParameters;
        }

        public string EntitySource()
        {
            return _entitysource;
        }
    }
}
