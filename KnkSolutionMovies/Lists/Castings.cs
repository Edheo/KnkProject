using System.Collections.Generic;
using System.Linq;
using KnkCore;
using KnkInterfaces.Interfaces;
using KnkSolutionMovies.Entities;
using KnkInterfaces.Enumerations;

namespace KnkSolutionMovies.Lists
{
    public class Castings : KnkList<Casting>
    {
        public Castings(KnkConnectionItf aConnection) 
        : base(aConnection)
        {
            
        }

        public override List<Casting> Datasource()
        {
            return (from c in Items orderby c.ArtistName select c).ToList();
        }
    }
}
