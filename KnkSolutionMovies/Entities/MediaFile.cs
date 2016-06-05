using KnkCore;
using KnkInterfaces.PropertyAtributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkSolutionMovies.Entities
{
    public class MediaFile : KnkItem
    {
        #region Interface/Implementation
        public MediaFile():base(new KnkTableEntity("vieMovieFiles", "MediaFiles"))
        {
        }
        #endregion Interface/Implementation

        #region Class Properties
        [AtributePrimaryKey]
        public KnkEntityIdentifier IdMovieFile { get; set; }
        public KnkEntityReference<Movie> IdMovie { get; set; }
        public KnkEntityReference<File> IdFile { get; set; }
        #endregion Class Properties

        public Movie Movie { get { return IdMovie?.Value; } set { IdMovie = new KnkEntityReference<Movie>(value); } }
        public File File { get { return IdFile?.Value; } set { IdFile = new KnkEntityReference<File>(value); } }

        public override string ToString()
        {
            return $"{File?.ToString()}";
        }
    }
}
