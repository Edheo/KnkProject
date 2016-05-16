using KnkCore;
using KnkInterfaces.Enumerations;
using KnkInterfaces.Interfaces;
using KnkSolutionMovies.Entities;
using KnkSolutionUsers.Lists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkSolutionMovies.Lists
{
    public class Movies:KnkList<Movie>
    {
        public Movies():base(new KnkConnection())
        {
            Connection.FillList(this);
        }
    }
}
