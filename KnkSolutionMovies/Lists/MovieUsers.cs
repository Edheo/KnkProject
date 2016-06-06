using KnkCore;
using KnkInterfaces.Interfaces;
using KnkSolutionMovies.Entities;
using KnkSolutionUsers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkSolutionMovies.Lists
{
    public class MovieUsers : KnkList<Movie, MovieUser>
    {
        public MovieUsers(KnkConnectionItf aConnection, KnkCriteria<Movie, MovieUser> aCriteria)
        : base(aConnection, aCriteria)
        {
        }

        public MovieUsers(KnkConnectionItf aConnection)
        : base(aConnection, KnkCore.Utilities.KnkCoreUtils.BuildLikeCriteria<Movie, MovieUser>("IdUser", aConnection.CurrentUser().PrimaryKeyValue().ToString(), "vieMovieUsers", "IdMovieUser"))
        {
        }

        public MovieUsers(KnkConnectionItf aConnection, int aIdUser)
        : base(aConnection, KnkCore.Utilities.KnkCoreUtils.BuildEqualCriteria<Movie, MovieUser>(new Movie(), "IdUser", aIdUser))
        {
        }
    }
}
