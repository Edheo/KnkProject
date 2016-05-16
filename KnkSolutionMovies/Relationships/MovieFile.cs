using KnkCore;
using KnkInterfaces.Interfaces;
using KnkSolutionMovies.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkSolutionMovies.Relationships
{
    class MovieFile : KnkItemBase, KnkItemItf
    {
        #region Interface/Implementation
        public MovieFile():base()
        {
            SourceEntity.SourceTable = "MovieFiles";
            SourceEntity.PrimaryKey = "IdMovieFile";
        }
        #endregion Interface/Implementation

        #region Class Properties
        public int? IdMovieFile { get; set; }
        public int IdMovie { get; set; }
        public int IdFile { get; set; }
        #endregion Class Properties

        KnkReferenceItf<MovieFile, File> _FileReference = null;

        private KnkReferenceItf<MovieFile, File> FileReference()
        {
            if (_FileReference == null)
                _FileReference = Connection.GetReference<MovieFile, File>(this, "IdFile");
            return _FileReference;
        }

        public File File
        {
            get
            {
                return FileReference().Value;
            }
        }

    }
}
