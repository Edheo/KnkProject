using KnkCore;
using KnkSolutionMovies.Entities;

namespace KnkSolutionMovies.Lists
{
    //public class FilePlays : KnkList<FilePlay, FilePlay>
    //{
    //    public FilePlays()
    //    : base(new KnkConnection())
    //    {
    //    }
    //}

    public class MoviePlays : KnkList<Movie,FilePlay>
    {
        public MoviePlays(Movie aMovie)
        : base(aMovie.Connection, new KnkCriteria<Movie, FilePlay>(aMovie, new KnkTableEntity("vieMoviePlays", "IdPlay", "IdMovie")))
        {
        }
    }
}
