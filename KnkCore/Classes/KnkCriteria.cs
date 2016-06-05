using KnkInterfaces.Utilities;
using KnkInterfaces.Interfaces;
using System.Collections.Generic;
using System.Linq;
using KnkInterfaces.Enumerations;
using System;
using KnkCore.Utilities;

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
            //AddLinkParameters(aItem);
        }

        public KnkCriteria(KnkTableEntityItf aEntityTable, Type aType, string aName, OperatorsEnu aOperator, object aValue)
        {
            _entityTable = aEntityTable as KnkTableEntity;
            AddParameter(aType, aName, aOperator, aValue);
        }

        //private void AddLinkParameters(Tdad aItem)
        //{
        //    if (typeof(Tdad) == typeof(Tlst))
        //    {
        //        return;
        //    }

        //    var lPrs = KnkInterfacesUtils.GetProperties<Tlst>();
        //    foreach (string lPar in KnkLinkFieldsList())
        //    {
        //        var lPrp = lPrs.Where(p => p.Name.ToLower().Equals(lPar.ToLower())).FirstOrDefault();
        //        if (lPrp != null)
        //        {
        //            //if(aItem.PropertyGet(lPrp.Name) != null)
        //            AddParameter(KnkCoreUtils.GetPropertyType(lPrp), lPrp.Name, OperatorsEnu.Equal, aItem.PropertyGet(lPrp.Name));
        //            //else if (typeof(Tdad) != typeof(Tlst))
        //            //    AddParameter(KnkCoreUtils.GetPropertyType(lPrp), lPrp.Name, OperatorsEnu.Equal, aItem.PropertyGet(lPrp.Name));
        //        }
        //    }
        //    var lEnt = this.EntityRelation();
        //    if (!string.IsNullOrEmpty(lEnt?.RelatedKey))
        //    {
        //        var lPrp = KnkInterfacesUtils.GetProperties<Tdad>().Where(p => p.Name.Equals(aItem.PrimaryKey())).FirstOrDefault();
        //        if (lPrp != null)
        //        {
        //            //if(aItem.PropertyGet(lEnt.RelatedKey)!=null)
        //            AddParameter(KnkCoreUtils.GetPropertyType(lPrp), lEnt.RelatedKey, OperatorsEnu.Equal, aItem.PropertyGet(lEnt.RelatedKey));
        //            //    AddParameter(KnkCoreUtils.GetPropertyType(lPrp), lEnt.RelatedKey, OperatorsEnu.Equal, aItem.PropertyGet(lEnt.RelatedKey));
        //        }
        //    }
        //}

        //private bool IsARelationship()
        //{
        //    if (string.IsNullOrEmpty(this.EntityRelation()?.RelatedKey))
        //        return false;
        //    else
        //        return true;
        //}

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

    public class KnkCriteria<Tlst> : KnkCriteria<Tlst, Tlst>
        where Tlst : KnkItemItf, new()
    {
        public KnkCriteria(Tlst aItem)
        : base(aItem, (new Tlst()).SourceEntity())
        {
        }
    }
}
