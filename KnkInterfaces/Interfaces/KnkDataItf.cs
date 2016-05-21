using System.Data;

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
