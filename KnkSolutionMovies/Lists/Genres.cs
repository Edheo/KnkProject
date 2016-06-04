using System.Collections.Generic;
using System.Linq;
using KnkCore;
using KnkInterfaces.Enumerations;
using KnkInterfaces.Interfaces;
using KnkSolutionMovies.Entities;

namespace KnkSolutionMovies.Lists
{
    public class Genres : KnkList<Genre, Genre>
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

    public class MovieGenres : KnkList<Movie, Genre>
    {
        //public MovieGenres(Movie aMovie) 
        //: base(aMovie.Connection(), new KnkCriteria<Movie, Genre>(aMovie, new KnkTableEntityRelation<Movie>("vieMovieGenres", "MovieGenres")))
        //{

        //}

        public MovieGenres(KnkConnectionItf aConnection, string aGenre)
        : base(aConnection, BuildCriteria(aGenre))
        {
        }

        private static KnkCriteria<Movie, Genre> BuildCriteria(string aGenre)
        {
            KnkCriteria<Movie, Genre> lCri = new KnkCriteria<Movie, Genre>(new Movie(), new KnkTableEntityRelation<Movie>("vieMovieGenres", "MovieGenres"));
            lCri.AddParameter(typeof(string), "GenreName", OperatorsEnu.Like, $"%{aGenre}%");
            return lCri;
        }
    }
}
