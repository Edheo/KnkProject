﻿using KnkCore;
using KnkInterfaces.PropertyAtributes;
using KnkSolutionMovies.Extenders;

namespace KnkSolutionMovies.Entities
{
    public class MediaLink: KnkItem
    {
        public readonly MediaLinkExtender Extender;

        #region Interface/Implementation
        public MediaLink():base(new KnkTableEntity("vieMediaLinks", "MediaLinks"))
        {
            Extender = new MediaLinkExtender(this);
        }
        #endregion Interface/Implementation

        #region Class Properties
        [AtributePrimaryKey]
        public KnkEntityIdentifier IdLink { get; set; }
        public KnkEntityReference<Movie> IdMovie { get; set; }
        public KnkEntityReference<Casting> IdCasting { get; set; }
        public KnkEntityIdentifier IdType { get; set; }
        public int Ordinal { get; set; }
        public string Link { get; set; }
        #endregion Class Properties

        public Movie Movie { get { return IdMovie?.Value; } set { IdMovie = new KnkEntityReference<Movie>(value); } }
        public Casting Casting { get { return IdCasting?.Value; } set { IdCasting = new KnkEntityReference<Casting>(value); } }

        public override string ToString()
        {
            return Link??string.Empty;
        }

    }
}
