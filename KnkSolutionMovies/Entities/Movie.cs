using KnkCore;
using KnkInterfaces.Classes;
using KnkInterfaces.PropertyAtributes;
using KnkSolutionMovies.Extenders;
using System;

namespace KnkSolutionMovies.Entities
{
    public class Movie : KnkItemBase
    {
        public readonly MovieExtender Extender;

        #region Interface/Implementation
        public Movie() : base(new KnkTableEntity("Movies"))
        {
            Extender = new MovieExtender(this);
        }
        #endregion Interface/Implementation

        #region Class Properties
        [AtributePrimaryKey]
        public KnkEntityIdentifier IdMovie { get; set; }
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
        public KnkEntityIdentifier IdSet { get; set; }
        public decimal? UserRating { get; set; }
        public DateTime? DateAdded { get; set; }

        public string MovieSet { get { return Extender.MovieSet?.Name; } }
        public string CreatedBy { get { return Extender.CreationUser?.Username; } }
        #endregion

        public override string ToString()
        {
            return $"{Title} ({Year})";
        }

    }

}
