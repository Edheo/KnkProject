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
        public KnkEntityIdentifier IdMediaFile { get; set; }
        public KnkEntityReference<Movie> IdMovie { get; set; }
        public KnkEntityReference<File> IdFile { get; set; }
        #endregion Class Properties

        public Movie Movie { get { return IdMovie?.Value; } }
        public File File { get { return IdFile?.Value; } }

        public override string ToString()
        {
            return $"{File?.ToString()}";
        }
    }
}
