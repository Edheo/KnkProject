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
    public class Countries : KnkList<Country>
    {
        public Countries(KnkConnectionItf aConnection)
        : base(aConnection)
        {

        }

        public override List<Country> Datasource()
        {
            return (from c in Items orderby c.CountryName select c).ToList();
        }
    }
}
