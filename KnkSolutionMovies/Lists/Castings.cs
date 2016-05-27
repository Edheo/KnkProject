using System.Collections.Generic;
using System.Linq;
using KnkCore;
using KnkInterfaces.Interfaces;
using KnkSolutionMovies.Entities;
using KnkInterfaces.Enumerations;

namespace KnkSolutionMovies.Lists
{
    public class Castings : KnkList<Casting,Casting>
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

    public class MovieCastings : KnkList<Movie,Casting>
    { 
        public MovieCastings(Movie aMovie) 
        : base(aMovie.Connection, new KnkCriteria<Movie, Casting>(aMovie, new KnkTableEntityRelation<Movie>("vieMovieCasting", "IdCasting")))
        {
        }

        public MovieCastings(KnkConnectionItf aConnection,KnkCriteria<Movie, Casting> aCriteria)
        : base(aConnection, aCriteria)
        {
        }

        public MovieCastings(KnkConnectionItf aConnection, string aArtistName)
        : base(aConnection, BuildCriteria(aArtistName))
        {
        }

        private static KnkCriteria<Movie, Casting> BuildCriteria(string aArtistName)
        {
            KnkCriteria<Movie, Casting> lCri = new KnkCriteria<Movie, Casting>(new Movie(), new KnkTableEntityRelation<Movie>("vieMovieCasting", "IdCasting"));
            lCri.AddParameter(typeof(string), "ArtistName", OperatorsEnu.Like, $"%{aArtistName}%");
            return lCri;
        }
    }
}
