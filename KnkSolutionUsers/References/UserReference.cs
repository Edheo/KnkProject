using KnkCore;
using KnkInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkSolutionUsers.References
{
    public class UserReference<TEntity,User> : KnkReference<TEntity, User> 
        where TEntity : KnkItemItf
        where User : KnkItemItf, new()
    {
        public UserReference(TEntity aDad, string aProperty) : base(aDad, aProperty, aDad.Connection.GetItem<User>)
        {
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
            return string.Empty;// this.Value.Username;
        }
    }
}
