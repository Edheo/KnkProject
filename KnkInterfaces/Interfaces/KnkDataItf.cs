using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkInterfaces.Interfaces
{
    public interface KnkDataItf
    {
        bool Connect();
        DataTable GetData<T>()
            where T : KnkItemItf, new();

        DataTable GetData<Tdad,Tlst>(KnkCriteriaItf<Tdad, Tlst> aCriteria) 
            where Tdad : KnkItemItf, new()
            where Tlst : KnkItemItf, new();
    }
}
