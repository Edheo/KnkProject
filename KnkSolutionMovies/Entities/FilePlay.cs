using KnkCore;
using KnkInterfaces.Classes;
using System;

namespace KnkSolutionMovies.Entities
{
    public class FilePlay : KnkItemBase
    {
        #region Interface/Implementation
        public FilePlay():base(new KnkTableEntity("FilePlays","IdPlay"))
        {
        }

        #endregion Interface/Implementation

        #region Class Properties
        public KnkEntityIdentifier IdPlay { get; set; }
        public int IdFile { get; set; }
        public DateTime DatePlay { get; set; }
        public int Playlenght { get; set; }
        public bool Finishedplay { get; set; }
        #endregion Class Properties
    }
}
