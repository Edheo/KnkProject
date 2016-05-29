using KnkCore;
using KnkSolutionMovies.Entities;

namespace KnkSolutionMovies.Lists
{
    public class MoviePlays : KnkList<Movie,FilePlay>
    {
        public MoviePlays(Movie aMovie)
        : base(aMovie.Connection(), new KnkCriteria<Movie, FilePlay>(aMovie, new KnkTableEntityRelation<Movie>("vieMoviePlays")))
        {
        }
    }
}
