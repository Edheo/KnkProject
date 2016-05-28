using KnkCore;
using KnkInterfaces.Classes;
using KnkInterfaces.PropertyAtributes;
using System;

namespace KnkSolutionMovies.Entities
{
    public class File : KnkItemBase
    {
        #region Interface/Implementation
        public File():base(new KnkTableEntity("Files"))
        {
        }
        #endregion Interface/Implementation

        #region Class Properties
        [AtributePrimaryKey]
        public KnkEntityIdentifier IdFile { get; set; }
        public int IdPath { get; set; }
        public string Filename { get; set; }
        public DateTime DateAdded { get; set; }
        #endregion Class Properties

        public override string ToString()
        {
            return Filename;
        }
    }
}
