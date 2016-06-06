﻿using KnkCore;
using KnkInterfaces.Enumerations;
using KnkInterfaces.Interfaces;
using KnkSolutionMovies.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkSolutionMovies.Lists
{
    public class MovieGenres : KnkList<Movie, Genre>
    {
        public MovieGenres(KnkConnectionItf aConnection, string aGenre)
        : base(aConnection, KnkCore.Utilities.KnkCoreUtils.BuildLikeCriteria<Movie, Genre>("GenreName", aGenre, "vieMovieGenres", "IdCasting"))
        {
        }
    }
}
