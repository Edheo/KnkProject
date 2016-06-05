using System.Collections.Generic;
using System.Linq;
using KnkCore;
using KnkInterfaces.Enumerations;
using KnkInterfaces.Interfaces;
using KnkSolutionMovies.Entities;

namespace KnkSolutionMovies.Lists
{
    public class Genres : KnkList<Genre>
    {
        public Genres(KnkConnectionItf aConnection) 
        : base(aConnection)
        {

        }

        public override List<Genre> Datasource()
        {
            return (from c in Items orderby c.GenreName select c).ToList();
        }
    }
}
