using KnkInterfaces.Interfaces;
using System;
using KnkInterfaces.Enumerations;
using KnkCore.Utilities;

namespace KnkCore
{
    public class KnkParameter:KnkParameterItf
    {
        public KnkParameter(Type aType, string aName, OperatorsEnu aOperator, object aValue)
            : this(aType,aName,aOperator,aValue,ParameterConnectorEnu.And)
        {
        }

        public KnkParameter(Type aType, string aName, OperatorsEnu aOperator, object aValue, ParameterConnectorEnu aConnector)
        {
            Type = aType;
            Name = aName;
            Operator = aOperator;
            Value = aValue;
            Connector = aConnector;
        }

        public Type Type { get; }
        public string Name { get; }
        public object Value { get; }
        public OperatorsEnu Operator { get; }
        public ParameterConnectorEnu Connector { get; }

        public string ToSqlWhere()
        {
            string[] lCommand = KnkUtility.GetEnumDescription(Operator).Split('|');
            string lRet = string.Empty;
            if (Value == null && lCommand.Length > 1)
                lRet = lCommand[1];
            else
            {
                lRet = lCommand[0];
            }
            lRet = lRet.Replace("@Field", $"[{Name}]");
            if(Value==null)
                lRet = lRet.Replace("@Value", $"null");
            else
                lRet = lRet.Replace("@Value", $"@{Name}");
            return lRet;
        }

    }
}
