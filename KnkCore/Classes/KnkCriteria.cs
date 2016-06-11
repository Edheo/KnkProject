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
        where Tdad : KnkItemItf, new()
        where Tlst : KnkItemItf, new()
    {
        private readonly List<KnkParameterItf> _parameters = new List<KnkParameterItf>();
        private readonly KnkTableEntity _entityTable;
        private KnkListItf<Tdad, Tlst> _parent;
        //Type aType, string aName, OperatorsEnu aOperator, object aValue
        public KnkCriteria(KnkListItf<Tdad, Tlst> aParent) 
        : this(aParent, (new Tlst()).SourceEntity())
        {
        }

        public KnkCriteria(KnkListItf<Tdad, Tlst> aParent, KnkTableEntityRelationItf<Tlst> aEntityTable)
        : this(aParent, (KnkTableEntityItf)aEntityTable)
        {
        }

        public KnkCriteria(KnkListItf<Tdad, Tlst> aParent, KnkTableEntityItf aEntityTable)
        {
            _entityTable = aEntityTable as KnkTableEntity;
            _parent = aParent;
            KnkLinkFields = new Tdad().PrimaryKey(); 
            //AddLinkParameters(aItem);
        }

        public KnkCriteria(KnkTableEntityItf aEntityTable, Type aType, string aName, OperatorsEnu aOperator, object aValue)
        {
            _entityTable = aEntityTable as KnkTableEntity;
            AddParameter(aType, aName, aOperator, aValue);
        }

        public string KnkLinkFields { get; }

        public KnkListItf<Tdad, Tlst> Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                _parent = value;
            }
        }

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
        public KnkCriteria(KnkListItf<Tlst,Tlst> aParent)
        : base(aParent, (new Tlst()).SourceEntity())
        {
        }
    }
}
