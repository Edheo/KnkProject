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
        private readonly List<KnkParameterItf> _parameters = new List<KnkParameterItf>();
        private readonly KnkTableEntity _entityTable;
        //Type aType, string aName, OperatorsEnu aOperator, object aValue
        public KnkCriteria(Tdad aItem) 
        : this(aItem, (new Tlst()).SourceEntity())
        {
        }

        public KnkCriteria(Tdad aItem, KnkTableEntityRelationItf<Tlst> aEntityTable)
        : this(aItem, (KnkTableEntityItf)aEntityTable)
        {
        }

        public KnkCriteria(Tdad aItem, KnkTableEntityItf aEntityTable)
        {
            _entityTable = aEntityTable as KnkTableEntity;
            KnkLinkFields = aItem.PrimaryKey(); 
            AddLinkParameters(aItem);
        }

        public KnkCriteria(KnkTableEntityItf aEntityTable, Type aType, string aName, OperatorsEnu aOperator, object aValue)
        {
            _entityTable = aEntityTable as KnkTableEntity;
            AddParameter(aType, aName, aOperator, aValue);
        }

        private void AddLinkParameters(Tdad aItem)
        {
            var lPrs = KnkInterfacesUtils.GetProperties<Tlst>();
            foreach (string lPar in KnkLinkFieldsList())
            {
                var lPrp = lPrs.Where(p => p.Name.ToLower().Equals(lPar.ToLower())).FirstOrDefault();
                if (lPrp != null && aItem.PropertyGet(lPrp.Name) != null)
                {
                    AddParameter(KnkInterfacesUtils.GetPropertyType(lPrp), lPrp.Name, OperatorsEnu.Equal, aItem.PropertyGet(lPrp.Name));
                }
            }
            var lEnt = this.EntityRelation();
            if (!string.IsNullOrEmpty(lEnt?.RelatedKey))
            {
                var lPrp = KnkInterfacesUtils.GetProperties<Tdad>().Where(p => p.Name.Equals(aItem.PrimaryKey())).FirstOrDefault();
                if (lPrp != null && aItem.PropertyGet(lEnt.RelatedKey)!=null)
                {
                    AddParameter(KnkInterfacesUtils.GetPropertyType(lPrp), lEnt.RelatedKey, OperatorsEnu.Equal, aItem.PropertyGet(lEnt.RelatedKey));
                }
            }

        }

        public string KnkLinkFields { get; }

        private List<string> KnkLinkFieldsList()
        {
            return KnkLinkFields.Split('.').Select(p => p.Trim()).ToList();
        }
        
        public List<KnkParameterItf> GetParameters()
        {
            return _parameters;
        }

        public KnkTableEntityItf EntityTable()
        {
            return _entityTable;
        }

        public KnkTableEntityRelationItf<Tdad> EntityRelation()
        {
            return _entityTable as KnkTableEntityRelation<Tdad>;
        }

        public KnkParameterItf AddParameter(Type aType, string aName, OperatorsEnu aOperator, object aValue)
        {
            KnkParameter lPar = new KnkParameter(_parameters.Count, aType, aName, aOperator, aValue);
            _parameters.Add(lPar);
            return lPar;
        }

        public bool HasParameters()
        {
            return _parameters.Count > 0;
        }
    }
}
