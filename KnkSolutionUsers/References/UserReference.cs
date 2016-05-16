using KnkCore;
using KnkInterfaces.Interfaces;
using KnkSolutionUsers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkSolutionUsers.References
{
    public class UserReference<TEntity> : KnkReference<TEntity, User> 
        where TEntity : KnkItemItf
    {
        public UserReference(TEntity aDad, string aProperty) : base(aDad, aProperty, aDad.Connection.GetItem<User>)
        {
        }
    }
}
