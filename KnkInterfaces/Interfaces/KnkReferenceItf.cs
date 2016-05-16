using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkInterfaces.Interfaces
{
    public interface KnkReferenceItf<T> where T : KnkItemItf, new()
    {
        int? Id { get; }
        T Value { get; }

        void ResetReference(int? aId);
        void ResetReference(int? aId, Func<int?, T> load);
    }
}
