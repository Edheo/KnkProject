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
    public class MoviesOldfashioned : KnkList<MovieOldfashion>
    {
        public MoviesOldfashioned(KnkConnectionItf aConnection)
        : base(aConnection)
        {
        }
    }
}
