using KnkCore;
using KnkInterfaces.Classes;
using KnkInterfaces.PropertyAtributes;
using KnkSolutionMovies.Extenders;
using KnkSolutionMovies.Utilities;
using System;

namespace KnkSolutionMovies.Entities
{
    public class Movie : KnkItemBase
    {
        public readonly MovieExtender Extender;

        #region Interface/Implementation
        public Movie() : base(new KnkTableEntity("vieMovies", "Movies"))
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
        public int? Year { get; set; }
        public string ImdbId { get; set; }
        public int? Seconds { get; set; }
        public string MPARating { get; set; }
        public string OriginalTitle { get; set; }
        public string Studio { get; set; }
        public string TrailerUrl { get; set; }
        public KnkEntityReference<MovieSet> IdSet { get; set; }
        public DateTime? ReleaseDate { get; set; }

        #endregion

        public override string ToString()
        {
            return $"{Title} ({Year})";
        }

        public MovieSet MovieSet
        {
            get
            {
                return IdSet.Value;
            }
            set
            {
                IdSet.Value = value;
            }
        }

    }

}
