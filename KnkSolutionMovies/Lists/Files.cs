﻿using KnkCore;
using KnkSolutionMovies.Entities;

namespace KnkSolutionMovies.Lists
{
    public class MovieFiles : KnkList<Movie, File>
    {
        public MovieFiles(Movie aMovie)
        : base(aMovie.Connection, new KnkCriteria<Movie, File>(aMovie, new KnkTableEntityRelation<Movie>("vieMovieFiles", "IdFile")))
        {
        }

    }
}
