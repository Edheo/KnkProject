using KnkCore;
using KnkInterfaces.Classes;
using KnkInterfaces.PropertyAtributes;
using KnkSolutionMovies.Extenders;
using KnkSolutionMovies.Utilities;
using System;

namespace KnkSolutionMovies.Entities
{
    public class Folder : KnkItemBase
    {
        KnkEntityIdentifier<Folder, Folder> _ParentFolder;
        KnkEntityIdentifier<Folder, Folder> _RootFolder;

        public readonly FolderExtender Extender;

        #region Interface/Implementation
        public Folder():base(new KnkTableEntity("vieFolders", "Paths"))
        {
            Extender = new FolderExtender(this);
        }
        #endregion Interface/Implementation

        #region Class Properties

        [AtributePrimaryKey]
        public KnkEntityIdentifier IdPath { get; set; }
        public string Protocol { get; set; }
        public string Path { get; set; }
        public string ContentType { get; set; }
        public string Scraper { get; set; }
        public string Hash { get; set; }
        public int? ScanRecursive { get; set; }
        public decimal? UseFolderNames { get; set; }
        public string strSettings { get; set; }
        public decimal? NoUpdate { get; set; }
        public decimal? Exclude { get; set; }
        public DateTime DateAdded { get; set; }
        public KnkEntityIdentifier IdParentPath { get; set; }
        public KnkEntityIdentifier IdRoot { get; set; }
        public int? Files { get; set; }
        #endregion Class Properties

        public override string ToString()
        {
            return Path;
        }

        public Folder ParentFolder
        {
            get
            {
                if (_ParentFolder == null) _ParentFolder = KnkSolutionMoviesUtils.GetReference<Folder, Folder>(this, "IdParentPath");
                return _ParentFolder?.Value;
            }
            set
            {
                if (_ParentFolder == null) _ParentFolder = KnkSolutionMoviesUtils.GetReference<Folder, Folder>(this, "IdParentPath");
                _ParentFolder.Value = value;
            }
        }

        public Folder RootFolder
        {
            get
            {
                if (_RootFolder == null) _RootFolder = KnkSolutionMoviesUtils.GetReference<Folder, Folder>(this, "IdParentPath");
                return _RootFolder?.Value;
            }
            set
            {
                if (_RootFolder == null) _RootFolder = KnkSolutionMoviesUtils.GetReference<Folder, Folder>(this, "IdParentPath");
                _RootFolder.Value = value;
            }
        }


    }
}
