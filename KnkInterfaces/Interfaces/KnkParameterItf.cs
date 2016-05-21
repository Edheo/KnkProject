using KnkInterfaces.Enumerations;
using System;

namespace KnkInterfaces.Interfaces
{
    public interface KnkParameterItf
    {
        Type Type { get; }
        string Name { get; }
        object Value { get; }
        OperatorsEnu Operator { get; }
        ParameterConnectorEnu Connector { get; }

        string ToSqlWhere();
    }
}
