﻿using KnkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkSolutionMovies.Entities
{
    public class Folder : KnkItemBase
    {
        #region Interface/Implementation
        public Folder():base()
        {
            SourceEntity.SourceTable = "Paths";
            SourceEntity.PrimaryKey = "IdPath";
        }
        #endregion Interface/Implementation

        #region Class Properties
        public int? IdPath { get; set; }
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
        public int? IdParentPath { get; set; }
        #endregion Class Properties
    }
}