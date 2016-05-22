using System.Collections.Generic;
using System.Linq;
using KnkCore;
using KnkInterfaces.Interfaces;
using KnkSolutionMovies.Entities;

namespace KnkSolutionMovies.Lists
{
    public class Castings : KnkList<Casting,Casting>
    {
        public Castings() : this(new KnkConnection())
        {
        }

        public Castings(KnkConnectionItf aConnection) 
        : base(aConnection)
        {
            
        }

        public override List<Casting> Datasource()
        {
            return (from art in Items orderby art.ArtistName select art).ToList(); 
        }
    }

    public class MovieCastings : KnkList<Movie,Casting>
    { 
        public MovieCastings(Movie aMovie) 
        : base(aMovie.Connection, new KnkCriteria<Movie, Casting>(aMovie, new KnkTableEntity("vieMovieCasting", "IdCasting")))
        {
        }
    }
}
