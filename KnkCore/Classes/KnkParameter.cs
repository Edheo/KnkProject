using KnkInterfaces.Interfaces;
using System;
using KnkInterfaces.Enumerations;
using KnkCore.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace KnkCore
{
    public class KnkParameter:KnkParameterItf
    {
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
            Value = aValue;
            Connector = aConnector;
        }

        public Type Type { get; }
        public string Name { get; }
        public string ParameterName { get; }
        public object Value { get; }
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
                lRet = KnkUtility.JoinParameters(InnerParammerters);
            }
            else
            {
                string[] lCommand = KnkUtility.GetEnumDescription(Operator).Split('|');
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
            }
            return lRet;
        }

    }
}
