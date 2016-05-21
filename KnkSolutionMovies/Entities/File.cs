using KnkCore;
using System;

namespace KnkSolutionMovies.Entities
{
    public class File : KnkItemBase
    {
        #region Interface/Implementation
        public File():base(new KnkTableEntity("Files", "IdFile"))
        {
        }
        #endregion Interface/Implementation

        #region Class Properties
        public int? IdFile { get; set; }
        public int IdPath { get; set; }
        public string Filename { get; set; }
        public DateTime DateAdded { get; set; }
        #endregion Class Properties
    }
}
