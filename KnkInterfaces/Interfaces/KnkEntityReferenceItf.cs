using System;

namespace KnkInterfaces.Interfaces
{
    public interface KnkEntityReferenceItf<TRef> : KnkEntityIdentifierItf
        where TRef : KnkItemItf, new()
    {
        TRef Reference { get; }
    }
}
