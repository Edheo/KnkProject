using System;
using System.Collections.Generic;
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
    }
}
