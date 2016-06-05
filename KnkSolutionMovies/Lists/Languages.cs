using KnkCore;
using KnkInterfaces.Interfaces;
using KnkSolutionMovies.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkSolutionMovies.Lists
{
    public class Languages : KnkList<Language>
    {
        public Languages(KnkConnectionItf aConnection) 
        : base(aConnection)
        {

        }

        public override List<Language> Datasource()
        {
            return (from c in Items orderby c.ToString() select c).ToList();
        }
    }
}
