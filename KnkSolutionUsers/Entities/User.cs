using KnkCore;
using KnkInterfaces.PropertyAtributes;

namespace KnkSolutionUsers.Entities
{
    public class User : KnkItem
    {
        #region Interface/Implementation
        public User():base(new KnkTableEntity("vieUsers", "Users"))
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
