using KnkCore;
using KnkInterfaces.PropertyAtributes;
using KnkSolutionMovies.Extenders;
using System;

namespace KnkSolutionMovies.Entities
{
    public class Movie : KnkItemBase
    {
        public readonly MovieExtender Extender;
        KnkEntityRelation<Movie, Genre> _Genres;
        KnkEntityRelation<Movie, File> _Files;
        KnkEntityRelation<Movie, MovieCasting> _Casting;
        KnkEntityRelation<Movie, Country> _Countries;
        KnkEntityRelation<Movie, MediaLink> _Pictures;
        KnkEntityRelation<Movie, MovieSummary> _Summary;
        KnkEntityRelation<Movie, FilePlay> _Plays;

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
        public bool AdultContent { get; set; }
        public decimal Budget { get; set; }
        public decimal Revenue { get; set; }
        public string HomePage { get; set; }

        #endregion

        public MovieSet MovieSet { get { return IdSet?.Value; } set { IdSet = new KnkEntityReference<MovieSet>(value); } }

        public KnkEntityRelation<Movie, Genre> Genres()
        {
            if (_Genres == null) _Genres = new KnkEntityRelation<Movie, Genre>(this, "vieMovieGenres");
            return _Genres;
        }

        public KnkEntityRelation<Movie, File> Files()
        {
            if (_Files == null) _Files = new KnkEntityRelation<Movie, File>(this, "vieMovieFiles");
            return _Files;
        }

        public KnkEntityRelation<Movie, MovieCasting> Casting()
        {
            if (_Casting == null) _Casting = new KnkEntityRelation<Movie, MovieCasting>(this);
            return _Casting;
        }

        public KnkEntityRelation<Movie, Country> Countries()
        {
            if (_Countries == null) _Countries = new KnkEntityRelation<Movie, Country>(this, "vieMovieCountries");
            return _Countries;
        }

        public KnkEntityRelation<Movie, MediaLink> Pictures()
        {
            if (_Pictures == null) _Pictures = new KnkEntityRelation<Movie, MediaLink>(this);
            return _Pictures;
        }

        public KnkEntityRelation<Movie, MovieSummary> Summary()
        {
            if (_Summary == null) _Summary = new KnkEntityRelation<Movie, MovieSummary>(this);
            return _Summary;
        }

        public KnkEntityRelation<Movie, FilePlay> Plays()
        {
            if (_Plays == null) _Plays = new KnkEntityRelation<Movie, FilePlay>(this, "vieMoviePlays");
            return _Plays;
        }

        public override string ToString()
        {
            return $"{Title} ({Year})";
        }

    }

}
