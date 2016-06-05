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

        internal KnkParameter(int aNumParameter, Type aType, string aName, OperatorsEnu aOperator, object aValue)
            : this(aNumParameter, aType, aName,aOperator,aValue,ParameterConnectorEnu.And)
        {
        }

        internal KnkParameter(int aNumParameter, Type aType, string aName, OperatorsEnu aOperator, object aValue, ParameterConnectorEnu aConnector)
        {
            Type = aType;
            Name = aName;
            ParameterName = aName + aNumParameter.ToString().PadLeft(3,'0');
            Operator = aOperator;
            Value = Convert.ChangeType(aValue, aType);
            Connector = aConnector;
        }

        private List<KnkParameterItf> _InnerParameters = new List<KnkParameterItf>();

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

        public void AddInnerParameter(object aValue)
        {
            AddInnerParameter(aValue, ParameterConnectorEnu.And);
        }

        public void AddInnerParameter(object aValue, ParameterConnectorEnu aConnector)
        {
            InnerParammerters.Add(new KnkParameter(InnerParammerters.Count, this.Type, this.Name, this.Operator, aValue, aConnector));
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
                lRet = lRet.Replace("@Field ", $"[{Name}] ");
                if (Value == null)
                    lRet = lRet.Replace("@Value ", $"null ");
                else
                    lRet = lRet.Replace("@Value ", $"@{ParameterName} ");

                lRet = lRet.Replace("@List[Field] ", $"@List[{ParameterName}] ");
            }
            return lRet;
        }

    }
}
