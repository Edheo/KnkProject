using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkInterfaces.Interfaces
{
    public interface KnkEntityRelationItf<Tdad, Titm> : KnkListItf<Tdad, Titm>
        where Tdad : KnkItemItf, new()
        where Titm : KnkItemItf, new()
    {
    }
}
