using KnkCore;
using KnkInterfaces.Classes;
using KnkInterfaces.PropertyAtributes;
using KnkSolutionMovies.Utilities;
using System;

namespace KnkSolutionMovies.Entities
{
    public class File : KnkItemBase
    {
        KnkEntityIdentifier<File, Folder> _Folder;

        #region Interface/Implementation
        public File():base(new KnkTableEntity("vieFiles","Files"))
        {
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
