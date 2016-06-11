using KnkCore;
using KnkInterfaces.Interfaces;
using KnkSolutionMovies.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkSolutionMovies.Lists
{
    public class MovieCastings : KnkList<Movie, MovieCasting>
    {
        public MovieCastings(KnkConnectionItf aConnection, string aArtistName)
        : base(aConnection)
        {
            Criteria = KnkCore.Utilities.KnkCoreUtils.BuildLikeCriteria<Movie, MovieCasting>(this, "ArtistName", aArtistName, "vieMovieCasting", "IdCasting");
        }
    }
}
