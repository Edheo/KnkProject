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
        KnkEntityIdentifier<File, Folder> _Folder;

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
        public KnkEntityIdentifier IdPath { get; set; }
        public string Filename { get; set; }
        public DateTime DateAdded { get; set; }
        public KnkEntityIdentifier IdRoot { get; set; }
        #endregion Class Properties

        public Folder Folder
        {
            get
            {
                if (_Folder == null) _Folder = KnkSolutionMoviesUtils.GetReference<File,Folder>(this, "IdPath");
                return _Folder?.Value;
            }
            set
            {
                if (_Folder == null) _Folder = KnkSolutionMoviesUtils.GetReference<File, Folder>(this, "IdPath");
                _Folder.Value = value;
                IdPath = value?.IdPath;
            }
        }

        public override string ToString()
        {
            return Folder?.Path + Filename;
        }
    }
}
