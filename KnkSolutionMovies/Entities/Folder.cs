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
        #region Interface/Implementation
        public Folder():base(new KnkTableEntity("vieFolders", "Paths"))
        {
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
        public KnkEntityReference<Folder> IdParentPath { get; set; }
        public KnkEntityReference<Folder> IdRoot { get; set; }
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
                return IdParentPath?.Value;
            }
            set
            {
                IdParentPath.Value = value;
            }
        }

        public Folder RootFolder
        {
            get
            {
                return IdRoot?.Value;
            }
            set
            {
                IdRoot.Value = value;
            }
        }


    }
}
