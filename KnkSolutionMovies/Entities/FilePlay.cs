using KnkCore;
using KnkInterfaces.PropertyAtributes;
using System;

namespace KnkSolutionMovies.Entities
{
    public class FilePlay : KnkItem
    {
        #region Interface/Implementation
        public FilePlay():base(new KnkTableEntity("vieMoviePlays", "MoviePlays"))
        {
        }

        #endregion Interface/Implementation

        #region Class Properties
        [AtributePrimaryKey]
        public KnkEntityIdentifier IdPlay { get; set; }
        public KnkEntityReference<File> IdFile { get; set; }
        public DateTime DatePlay { get; set; }
        public int Playlenght { get; set; }
        public bool Finishedplay { get; set; }
        public string Filename { get; set; }
        public string Path { get; set; }

        #endregion Class Properties

        public File File { get { return IdFile?.Reference; } }

        public string Date()
        {
            return DatePlay.ToString("dd/MM/yyyy");
        }

        public string Time()
        {
            return DatePlay.ToString("hh:mm");
        }

        public override string ToString()
        {
            return $"{DatePlay:dd/MM/yyyy - hh:mm}";
        }

    }
}
