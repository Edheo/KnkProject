using KnkCore;
using KnkInterfaces.Interfaces;
using KnkInterfaces.PropertyAtributes;
using KnkSolutionMovies.Extenders;
using System;

namespace KnkSolutionMovies.Entities
{
    public class Movie : KnkItem
    {
        public readonly MovieExtender Extender;
        KnkEntityRelation<Movie, MovieGenre> _Genres;
        KnkEntityRelation<Movie, MediaFile> _Files;
        KnkEntityRelation<Movie, MovieCasting> _Casting;
        KnkEntityRelation<Movie, MovieCountry> _Countries;
        KnkEntityRelation<Movie, MediaLink> _Pictures;
        KnkEntityRelation<Movie, MovieSummary> _Summary;
        KnkEntityRelation<Movie, FilePlay> _Plays;
        KnkEntityRelation<Movie, MovieCompany> _Companies;
        KnkEntityRelation<Movie, MovieLanguage> _Languages;
        KnkEntityRelation<Movie, MovieUser> _Users;

        #region Interface/Implementation
        public Movie() : this(new KnkTableEntity("vieMovies", "Movies"))
        {
        }

        internal Movie(KnkTableEntity aEntity) : base(aEntity)
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
        public int? TmdbId { get; set; }
        public int? Seconds { get; set; }
        public string MPARating { get; set; }
        public string OriginalTitle { get; set; }
        public string Studio { get; set; }
        public KnkEntityReference<MovieSet> IdSet { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public bool AdultContent { get; set; }
        public decimal Budget { get; set; }
        public decimal Revenue { get; set; }
        public string HomePage { get; set; }
        public decimal Popularity { get; set; }
        public DateTime? ScrapedDate { get; set; }
        #endregion

        public DateTime? LastViewed { get; set; }
        public int? ViewedTimes { get; set; }
        public DateTime? LastPlayed { get; set; }
        public int? PlayedTimes { get; set; }

        public MovieSet MovieSet { get { return IdSet?.Reference; } }

        public KnkEntityRelationItf<Movie, MovieUser> Users()
        {
            if (_Users == null) _Users = new KnkEntityRelation<Movie, MovieUser>(this, "vieMovieUsers");
            return _Users;
        }

        public KnkEntityRelationItf<Movie, MovieGenre> Genres()
        {
            if (_Genres == null) _Genres = new KnkEntityRelation<Movie, MovieGenre>(this, "vieMovieGenres");
            return _Genres;
        }

        public KnkEntityRelationItf<Movie, MediaFile> Files()
        {
            if (_Files == null) _Files = new KnkEntityRelation<Movie, MediaFile>(this, "vieMovieFiles");
            return _Files;
        }

        public KnkEntityRelationItf<Movie, MovieCasting> Casting()
        {
            if (_Casting == null) _Casting = new KnkEntityRelation<Movie, MovieCasting>(this);
            return _Casting;
        }

        public KnkEntityRelationItf<Movie, MovieCountry> Countries()
        {
            if (_Countries == null) _Countries = new KnkEntityRelation<Movie, MovieCountry>(this, "vieMovieCountries");
            return _Countries;
        }

        public KnkEntityRelationItf<Movie, MediaLink> Pictures()
        {
            if (_Pictures == null) _Pictures = new KnkEntityRelation<Movie, MediaLink>(this, "vieMovieLinks");
            return _Pictures;
        }

        public KnkEntityRelationItf<Movie, MovieSummary> Summary()
        {
            if (_Summary == null) _Summary = new KnkEntityRelation<Movie, MovieSummary>(this);
            return _Summary;
        }

        public KnkEntityRelationItf<Movie, FilePlay> Plays()
        {
            if (_Plays == null) _Plays = new KnkEntityRelation<Movie, FilePlay>(this, "vieMoviePlays");
            return _Plays;
        }

        public KnkEntityRelationItf<Movie, MovieCompany> Companies()
        {
            if (_Companies == null) _Companies = new KnkEntityRelation<Movie, MovieCompany>(this, "vieMovieCompanies");
            return _Companies;
        }

        public KnkEntityRelationItf<Movie, MovieLanguage> Languages()
        {
            if (_Languages == null) _Languages = new KnkEntityRelation<Movie, MovieLanguage>(this, "vieMovieLanguages");
            return _Languages;
        }

        public override string ToString()
        {
            return $"{Title} ({Year})";
        }

    }

    public class MovieOldfashion : Movie
    {
        public MovieOldfashion() : base(new KnkTableEntity("vieMovieOldfashion", "Movies"))
        {
        }
    }
}
