using KnkCore;
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
