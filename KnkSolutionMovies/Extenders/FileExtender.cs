using KnkSolutionMovies.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KnkSolutionMovies.Extenders
{
    public class FileExtender
    {
        private readonly File _File;

        public FileExtender(File aFile)
        {
            _File = aFile;
        }


        public string TitleFromFilename()
        {
            string lReturn = System.IO.Path.GetFileNameWithoutExtension(_File.ToString());
            lReturn = new string((from c in lReturn
                                  where char.IsWhiteSpace(c) || char.IsLetterOrDigit(c)
                                  select c).ToArray());
            return lReturn;
        }
    }
}
