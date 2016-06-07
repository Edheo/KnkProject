using KnkCore;
using KnkInterfaces.PropertyAtributes;
using KnkSolutionUsers.Entities;

namespace KnkSolutionMovies.Entities
{
    public class MovieUser : KnkItem
    {
        #region Interface/Implementation
        public MovieUser():base(new KnkTableEntity("vieMovieUsers", "MovieUsers"))
        {
        }
        #endregion Interface/Implementation

        #region Class Properties
        [AtributePrimaryKey]
        public KnkEntityIdentifier IdMovieUser { get; set; }
        public KnkEntityReference<Movie> IdMovie { get; set; }
        public KnkEntityReference<User> IdUser { get; set; }
        decimal? UserRating { get; set; }
        #endregion Class Properties

        public Movie Movie { get { return IdMovie?.Reference; } }
        public User User { get { return IdUser?.Reference; } }

        public override string ToString()
        {
            return UserRating.ToString();
        }
    }
}
