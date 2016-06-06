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
    public class CastingTypes : KnkList<CastingType>
    {
        public CastingTypes(KnkConnectionItf aConnection) 
        : base(aConnection)
        {

        }

        public override List<CastingType> Datasource()
        {
            return (from c in Items orderby c.Type select c).ToList();
        }
    }
}
