using KnkCore;
using KnkInterfaces.Classes;
using KnkInterfaces.PropertyAtributes;
using KnkSolutionMovies.Extenders;
using KnkSolutionMovies.Utilities;
using System;

namespace KnkSolutionMovies.Entities
{
    public class MissingMovieFile : File
    {
        public MissingMovieFile():base(new KnkTableEntity("vieMovieMissing", "Files"))
        {
        }
    }

    public class File : KnkItemBase
    {
        public readonly FileExtender Extender;

        #region Interface/Implementation
        public File():this(new KnkTableEntity("vieFiles","Files"))
        {
        }

        internal File(KnkTableEntity aEntity) : base(aEntity)
        {
            Extender = new FileExtender(this);
        }

        #endregion Interface/Implementation

        #region Class Properties
        [AtributePrimaryKey]
        public KnkEntityIdentifier IdFile { get; set; }
        public KnkEntityReference<Folder> IdPath { get; set; }
        public string Filename { get; set; }
        public DateTime DateAdded { get; set; }
        public KnkEntityIdentifier IdRoot { get; set; }
        #endregion Class Properties

        public Folder Folder { get { return IdPath?.Value; } set { IdPath = new KnkEntityReference<Folder>(value); } }

        public override string ToString()
        {
            return Folder?.Path + Filename;
        }
    }
}
