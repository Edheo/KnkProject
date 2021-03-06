﻿using KnkInterfaces.Enumerations;
using System;
using System.Collections.Generic;

namespace KnkInterfaces.Interfaces
{
    public interface KnkParameterItf
    {
        Type Type { get; }
        string Name { get; }
        string ParameterName { get; }
        dynamic Value { get; }
        OperatorsEnu Operator { get; }
        ParameterConnectorEnu Connector { get; }

        List<KnkParameterItf> InnerParammerters { get; }

        void AddInnerParameter(object aValue);
        void AddInnerParameter(object aValue, ParameterConnectorEnu aConnector);

        string ToSqlWhere();
    }
}
