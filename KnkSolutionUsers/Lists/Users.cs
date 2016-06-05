using KnkCore;
using KnkSolutionUsers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkSolutionUsers.Lists
{
    public class Users : KnkList<User>
    {
        public Users():base(new KnkConnection())
        {
        }
    }
}
