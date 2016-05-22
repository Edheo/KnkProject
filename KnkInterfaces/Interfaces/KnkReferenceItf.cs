using System;

namespace KnkInterfaces.Interfaces
{
    public interface KnkReferenceItf<TDad,TReference> 
        where TDad : KnkItemItf
        where TReference : KnkItemItf, new()
    {
        TReference Value { get; }

        void ResetReference(TDad aItem, string aProperty);
        void ResetReference(TDad aItem, string aProperty, Func<int?, TReference> load);
    }
}
