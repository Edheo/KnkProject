using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkInterfaces.Interfaces
{
    public interface KnkParameterItf
    {
        Type Type { get; }
        string Name { get; }
        object Value { get; }

        string ToSqlWhere();
    }
}
