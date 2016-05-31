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
        int? GetInnerValue();
        void SetInnerValue(int? aValue);
    }

    public interface KnkEntityIdentifierItf<TRef> : KnkEntityIdentifierItf, KnkEntityReferenceItf<TRef>
        where TRef : KnkItemItf, new()
    {
    }
}
