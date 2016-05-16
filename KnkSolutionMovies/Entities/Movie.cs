using KnkCore;
using KnkSolutionMovies.Extenders;

namespace KnkSolutionMovies.Entities
{
    public class Movie : KnkItemBase
    {
        public readonly MovieExtender Extender;

            #region Interface/Implementation
        public Movie() : base()
        {
            SourceEntity.SourceTable = "Movies";
            SourceEntity.PrimaryKey = "IdMovie";
            Extender = new MovieExtender(this);
        }
        #endregion Interface/Implementation

        #region Class Properties
        public int? IdMovie { get; set; }
        public string Title { get; set; }
        public string TagLine { get; set; }
        public int Votes { get; set; }
        public decimal Rating { get; set; }
        public int Year { get; set; }
        public string ImdbId { get; set; }
        public int Seconds { get; set; }
        public string MPARating { get; set; }
        public string OriginalTitle { get; set; }
        public string Studio { get; set; }
        public string TrailerUrl { get; set; }
        public int? IdSet { get; set; }
        public decimal? UserRating { get; set; }

        public string MovieStet { get { return Extender.MovieSet?.Name; } }
        #endregion
    }

}
