using KnkInterfaces.Utilities;
using KnkInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace KnkCore
{
    [Serializable]
    //public class ChildList<T> : IList<T>, IList where T : class
    public class KnkList<T> : KnkListItf<T> where T : KnkItemItf, new()
    {
        public KnkConnectionItf Connection { get; set; }

        List<T> _List = new List<T>();

        public KnkList(KnkConnectionItf aConnection)
        {
            Connection = aConnection;
        }

        public KnkList(KnkConnectionItf aConnection, KnkCriteria<T,T> aCriteria) : this(aConnection)
        {
            
        }

        public KnkList(KnkConnectionItf aConnection, List<T> aList) : this(aConnection)
        {
            FillFromList(aList);
        }

        public void FillFromDataTable(DataTable aTable)
        {
            _List.Clear();
            _List = aTable.AsEnumerable().Select(row => KnkUtility.CopyRecord<T>(this as KnkListItf, row)).ToList();
        }

        public int Count()
        {
            return _List.Count;
        }

        public void FillFromList(List<T> aList)
        {
            _List.Clear();
            _List.AddRange(aList);
        }

        public List<T> Items
        {
            get
            {
                return _List;
            }
        }

        public virtual List<T> Datasource()
        {
            return _List;
        }
    }


}
