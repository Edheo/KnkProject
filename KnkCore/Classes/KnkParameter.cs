using KnkInterfaces.Interfaces;
using System;
using KnkInterfaces.Enumerations;
using System.Collections.Generic;
using System.Linq;
using KnkInterfaces.Utilities;

namespace KnkCore
{
    public class KnkParameter:KnkParameterItf
    {
        Type _type;
        dynamic _value;

        public KnkParameter(Type aType, string aName, OperatorsEnu aOperator, object aValue)
            : this(aType,aName,aOperator,aValue,ParameterConnectorEnu.And)
        {
        }

        private List<KnkParameterItf> _InnerParameters = new List<KnkParameterItf>();

        public KnkParameter(Type aType, string aName, OperatorsEnu aOperator, object aValue, ParameterConnectorEnu aConnector)
            : this(aType, aName, aName, aOperator, aValue, ParameterConnectorEnu.And)
        {
        }

        public KnkParameter(Type aType, string aName, string aParameterName, OperatorsEnu aOperator, object aValue, ParameterConnectorEnu aConnector)
        {
            Type = aType;
            Name = aName;
            ParameterName = aParameterName;
            Operator = aOperator;
            Value = Convert.ChangeType(aValue, aType);
            Connector = aConnector;
        }

        public Type Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }
        public string Name { get; }
        public string ParameterName { get; }
        public dynamic Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        public OperatorsEnu Operator { get; }
        public ParameterConnectorEnu Connector { get; }

        public List<KnkParameterItf> InnerParammerters
        {
            get
            {
                return _InnerParameters;
            }
        }

        public void AddInnerParameter(string aParameterName, object aValue)
        {
            AddInnerParameter(aParameterName, aValue, ParameterConnectorEnu.And);
        }

        public void AddInnerParameter(string aParameterName, object aValue, ParameterConnectorEnu aConnector)
        {
            InnerParammerters.Add(new KnkParameter(this.Type, this.Name, aParameterName, this.Operator, aValue, aConnector));
        }

        public string ToSqlWhere()
        {
            string lRet = string.Empty;
            if (InnerParammerters.Count > 0)
            {
                lRet = KnkInterfacesUtils.JoinParameters(InnerParammerters);
            }
            else
            {
                string[] lCommand = KnkInterfacesUtils.GetEnumDescription(Operator).Split('|');
                if (Value == null && lCommand.Length > 1)
                    lRet = lCommand[1];
                else
                {
                    lRet = lCommand[0];
                }
                lRet = lRet.Replace("@Field", $"[{Name}]");
                if (Value == null)
                    lRet = lRet.Replace("@Value", $"null");
                else
                    lRet = lRet.Replace("@Value", $"@{ParameterName}");

                lRet = lRet.Replace("@List[Field]", $"@List[{Name}]");
            }
            return lRet;
        }

    }
}
