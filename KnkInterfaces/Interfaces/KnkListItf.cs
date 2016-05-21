using System.Collections.Generic;
using System.Data;

namespace KnkInterfaces.Interfaces
{
    public interface KnkListItf
    {
        KnkConnectionItf Connection { get; set; }
        int Count();
        void FillFromDataTable(DataTable aTable);
    }

    public interface KnkListItf<T> : KnkListItf
        where T : KnkItemItf, new()
    {
        List<T> Items { get; }
        List<T> Datasource();
        void FillFromList(List<T> aList);
    }

}
