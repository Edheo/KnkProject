using KnkInterfaces.Utilities;
using KnkInterfaces.Interfaces;
using System.Collections.Generic;
using System.Linq;
using KnkInterfaces.Enumerations;
using System;

namespace KnkCore
{
    public class KnkCriteria<Tdad, Tlst> : KnkCriteriaItf<Tdad, Tlst> 
        where Tdad :KnkItemItf, new()
        where Tlst : KnkItemItf, new()
    {
        private Tdad _item;
        private readonly List<KnkParameterItf> _parameters = new List<KnkParameterItf>();
        private readonly KnkTableEntity _entityTable;

        public KnkCriteria(Tdad aItem):this(aItem, (new Tlst()).SourceEntity)
        {
        }

        public KnkCriteria(Tdad aItem, KnkTableEntityRelationItf<Tlst> aEntityTable):this(aItem,(KnkTableEntityItf)aEntityTable)
        {
        }

        public KnkCriteria(Tdad aItem, KnkTableEntityItf aEntityTable)
        {
            _item = aItem;
            _entityTable = aEntityTable as KnkTableEntity;
            KnkLinkFields = aItem.SourceEntity.PrimaryKey;
        }

        public string KnkLinkFields { get; }

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
            string lWhere = Utilities.KnkUtility.JoinParameters(GetParameters(aItem));
            return lWhere.Length > 0 ? " Where " + lWhere: string.Empty;
        }

        private List<string> KnkLinkFieldsList()
        {
            return KnkLinkFields.Split('.').Select(p => p.Trim()).ToList();
        }

        
        public List<KnkParameterItf> GetParameters()
        {
            return GetParameters(_item);
        }

        public List<KnkParameterItf> GetParameters(Tdad aItem)
        {
            _item = aItem;
            var lPrs = KnkUtility.GetProperties<Tlst>();
            var lParameters = new List<KnkParameterItf>();

            foreach(KnkParameter lKnkPar in _parameters)
            {
                lParameters.Add(lKnkPar);
            }

            foreach (string lPar in KnkLinkFieldsList())
            {
                var lPrp = lPrs.Where(p => p.Name.ToLower().Equals(lPar.ToLower())).FirstOrDefault();
                if(lPrp!=null)
                {
                    var lType = KnkUtility.GetPropertyType(lPrp);
                    var lName = lPrp.Name;
                    var lValue = _item.PropertyGet(lPrp.Name);
                    if (lValue != null)
                    {
                        KnkParameter lKnkPar = new KnkParameter(lType, lName, OperatorsEnu.Equal, lValue);
                        lParameters.Add(lKnkPar);
                    }
                }
            }
            if (!string.IsNullOrEmpty(this.EntityRelation()?.RelatedKey))
            {
                var lPrp = KnkUtility.GetProperties<Tdad>().Where(p => p.Name.Equals(_item.SourceEntity.PrimaryKey)).FirstOrDefault();
                if (lPrp != null)
                {
                    var lType = KnkUtility.GetPropertyType(lPrp);
                    var lName = this.EntityRelation().RelatedKey;
                    var lValue = _item.PropertyGet(lPrp.Name);
                    KnkParameter lKnkPar = new KnkParameter(lType, lName, OperatorsEnu.Equal, lValue);
                    lParameters.Add(lKnkPar);
                }
            }
            return lParameters;
        }

        public KnkTableEntityItf EntityTable()
        {
            return _entityTable;
        }

        public KnkTableEntityRelationItf<Tlst> EntityRelation()
        {
            return _entityTable as KnkTableEntityRelation<Tlst>;
        }

        public KnkParameterItf AddParameter(Type aType, string aName, OperatorsEnu aOperator, object aValue)
        {
            KnkParameter lPar = new KnkParameter(aType, aName, aOperator, aValue);
            _parameters.Add(lPar);
            return lPar;
        }

        public bool HasParameters()
        {
            return _parameters.Count > 0;
        }
    }
}
