using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkInterfaces.Interfaces
{
    public interface KnkCriteriaItf
    {
        string KnkLinkFields { get; set; }
        List<KnkParameterItf> GetParameters();
        string GetWhereFromParameters();
    }

    public interface KnkCriteriaItf<Tdad, Tlst> : KnkCriteriaItf
        where Tdad : KnkItemItf, new()
        where Tlst : KnkItemItf, new()
    {
        Tdad Parent { get; }

        List<KnkParameterItf> GetParameters(Tdad Item);

        string GetWhereFromParameters(Tdad Item);

        string GetWhereFromParameters(Tdad Item, string aConnector);
    }
}
