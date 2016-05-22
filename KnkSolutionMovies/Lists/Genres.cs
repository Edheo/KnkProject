using KnkCore;
using KnkSolutionMovies.Entities;
using KnkSolutionMovies.Relationships;
using System.Linq;

namespace KnkSolutionMovies.Lists
{
    public class Genres : KnkList<GenreClass,GenreClass>
    {
        public Genres()
        : base(new KnkConnection())
        {
        }
    }

    public class MovieGenres : KnkList<Movie, GenreClass>
    {
        public MovieGenres(Movie aMovie) 
        : base(aMovie.Connection, new KnkCriteria<Movie, GenreClass>(aMovie, new KnkTableEntityRelation<Movie>("vieMovieGenres", "IdGenre")))
        {

        }
    }
}
