using KnkInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkInterfaces.Interfaces
{
    public interface KnkEntityIdentifierItf : IConvertible, IComparable
    {
        int? Value { get; set; }
    }
}
