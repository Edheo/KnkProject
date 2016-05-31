using System;

namespace KnkInterfaces.Interfaces
{
    public interface KnkEntityReferenceItf<TRef> 
        where TRef : KnkItemItf, new()
    {
        TRef Value { get; }
    }
}
