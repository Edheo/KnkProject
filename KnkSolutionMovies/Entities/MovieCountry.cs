﻿using KnkCore;
using KnkInterfaces.PropertyAtributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkSolutionMovies.Entities
{
    public class MovieCountry : KnkItem
    {
        #region Interface/Implementation
        public MovieCountry():base(new KnkTableEntity("vieMovieCountries", "MovieCountries"))
        {
        }
        #endregion Interface/Implementation

        #region Class Properties
        [AtributePrimaryKey]
        public KnkEntityIdentifier IdMovieCountry { get; set; }
        public KnkEntityReference<Movie> IdMovie { get; set; }
        public KnkEntityReference<Country> IdCountry { get; set; }
        #endregion Class Properties

        public Movie Movie { get { return IdMovie?.Reference; } }
        public Country Country { get { return IdCountry?.Reference; } }

        public override string ToString()
        {
            return IdCountry?.Reference.ToString();
        }
    }
}
