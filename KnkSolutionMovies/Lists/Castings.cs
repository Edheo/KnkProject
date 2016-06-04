using System.Collections.Generic;
using System.Linq;
using KnkCore;
using KnkInterfaces.Interfaces;
using KnkSolutionMovies.Entities;
using KnkInterfaces.Enumerations;

namespace KnkSolutionMovies.Lists
{
    public class Castings : KnkList<Casting, Casting>
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

    public class MovieCastings : KnkList<Movie,MovieCasting>
    { 
        public MovieCastings(KnkConnectionItf aConnection,KnkCriteria<Movie, MovieCasting> aCriteria)
        : base(aConnection, aCriteria)
        {
        }

        public MovieCastings(KnkConnectionItf aConnection, string aArtistName)
        : base(aConnection, KnkCore.Utilities.KnkCoreUtils.BuildLikeCriteria<Movie, MovieCasting>("ArtistName", aArtistName, "vieMovieCasting", "IdCasting"))
        {
        }
    }
}
