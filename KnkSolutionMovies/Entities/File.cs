using KnkCore;
using KnkInterfaces.PropertyAtributes;
using KnkSolutionMovies.Extenders;
using System;

namespace KnkSolutionMovies.Entities
{
    public class File : KnkItem
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
        public DateTime Filedate { get; set; }
        public int Scraped { get; set; }
        public string TitleSearch { get; set; }
        public string YearSearch { get; set; }
        public KnkEntityReference<Folder> IdRoot { get; set; }
        #endregion Class Properties

        public Folder Folder { get { return IdPath?.Value; } }
        public Folder Root { get { return IdRoot?.Value; } }

        public override string ToString()
        {
            return Folder?.Path + Filename;
        }
    }

    public class MissingMovieFile : File
    {
        public MissingMovieFile() : base(new KnkTableEntity("vieMovieMissing", "Files"))
        {
        }
    }
}
