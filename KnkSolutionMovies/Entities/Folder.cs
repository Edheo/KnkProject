using KnkCore;
using KnkInterfaces.Classes;
using System;

namespace KnkSolutionMovies.Entities
{
    public class Folder : KnkItemBase
    {
        #region Interface/Implementation
        public Folder():base(new KnkTableEntity("Paths","IdPath"))
        {
        }
        #endregion Interface/Implementation

        #region Class Properties
        public KnkEntityIdentifier IdPath { get; set; }
        public string Path { get; set; }
        public string ContenType { get; set; }
        public string Scraper { get; set; }
        public string Hash { get; set; }
        public int? ScanRecursive { get; set; }
        public decimal? UseFolderNames { get; set; }
        public string strSettings { get; set; }
        public decimal? NoUpdate { get; set; }
        public decimal? Exclude { get; set; }
        public DateTime DateAdded { get; set; }
        public KnkEntityIdentifier IdParentPath { get; set; }
        #endregion Class Properties
    }
}
