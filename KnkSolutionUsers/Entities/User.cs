using KnkCore;
using KnkInterfaces.Classes;
using KnkInterfaces.PropertyAtributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkSolutionUsers.Entities
{
    public class User : KnkItemBase
    {
        #region Interface/Implementation
        public User():base(new KnkTableEntity("Users"))
        {
        }
        #endregion Interface/Implementation

        #region Class Properties
        [AtributePrimaryKey]
        public KnkEntityIdentifier IdUser { get; set; }
        public string Username { get; set; }
        public string Userpassword { get; set; }
        #endregion Class Properties

        public override string ToString()
        {
            return Username;
        }

    }
}
