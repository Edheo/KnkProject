using System;

namespace KnkInterfaces.Interfaces
{
    public interface KnkReferenceItf<TRef> 
        where TRef : KnkItemItf, new()
    {
        TRef Value { get; }

        void ResetReference(TRef aItem);
        void ResetReference(TRef aItem, Func<int?, TRef> load);
    }
}
