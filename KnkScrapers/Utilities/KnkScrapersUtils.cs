using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkScrappers.Utilities
{
    static class KnkScrapersUtils
    {
        public static string FromUrlToPath(string aPath)
        {
            var lReturn = aPath;
            if (lReturn.StartsWith("smb://")) lReturn = lReturn.Replace("smb://", "//");
            return lReturn;
        }

        public static bool DirectoryExists(string aPath, out bool aAvailable)
        {
            bool lRet = Directory.Exists(aPath);
            aAvailable = lRet;
            if(!lRet)
            {
                try
                {
                    var lDate = Directory.GetCreationTime(aPath);
                    lRet = true;
                }
                catch { }
                {
                    lRet = false;
                }
            }
            return lRet;
        }
    }
}
