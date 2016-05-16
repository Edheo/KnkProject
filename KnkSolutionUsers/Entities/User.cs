using KnkCore;
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
        public User():base()
        {
            SourceEntity.SourceTable = "Users";
            SourceEntity.PrimaryKey = "IdUser";
        }
        #endregion Interface/Implementation

        #region Class Properties
        public int? IdUser { get; set; }
        public string Username { get; set; }
        public string Userpassword { get; set; }
        #endregion Class Properties
    }
}
