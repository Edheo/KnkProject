using KnkCore;
using KnkInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkSolutionUsers.References
{
    public class UserReference<T> : KnkReference<T> 
        where T:KnkItemItf, new()
    {
        public UserReference<T>() : base(null, aItem.Connection.GetItem<T>) 
        {
            int? lVal = (int?)aItem.PropertyGet(aProperty);
            aItem.Connection.SetReference<T>(this, lVal);
        }

        public string Text
        {
            get
            {
                return this.ToString();
            }
        }

        public override string ToString()
        {
            return this.Value.Name;
        }
    }
}
