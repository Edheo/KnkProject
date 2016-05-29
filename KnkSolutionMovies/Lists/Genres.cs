using System.Collections.Generic;
using System.Linq;
using KnkCore;
using KnkInterfaces.Enumerations;
using KnkInterfaces.Interfaces;
using KnkSolutionMovies.Entities;

namespace KnkSolutionMovies.Lists
{
    public class Genres : KnkList<GenreClass,GenreClass>
    {
        public Genres(KnkConnectionItf aConnection) 
        : base(aConnection)
        {

        }

        public override List<GenreClass> Datasource()
        {
            return (from c in Items orderby c.Genre select c).ToList();
        }
    }

    public class MovieGenres : KnkList<Movie, GenreClass>
    {
        public MovieGenres(Movie aMovie) 
        : base(aMovie.Connection(), new KnkCriteria<Movie, GenreClass>(aMovie, new KnkTableEntityRelation<Movie>("vieMovieGenres")))
        {

        }

        public MovieGenres(KnkConnectionItf aConnection, string aGenre)
        : base(aConnection, BuildCriteria(aGenre))
        {
        }

        private static KnkCriteria<Movie, GenreClass> BuildCriteria(string aGenre)
        {
            KnkCriteria<Movie, GenreClass> lCri = new KnkCriteria<Movie, GenreClass>(new Movie(), new KnkTableEntityRelation<Movie>("vieMovieGenres"));
            lCri.AddParameter(typeof(string), "Genre", OperatorsEnu.Like, $"%{aGenre}%");
            return lCri;
        }

    }
}
