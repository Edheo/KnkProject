﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KnkSolutionMovies.Utilities
{
    public static class KnkUtility
    {
        public static string GetLastPart(string aString, char aSpliter)
        {
            var lSplit = aString.Split(aSpliter);
            return lSplit[lSplit.Length -1];
        }

        public static Stream GetUrlStream(string aUrl)
        {
            Stream lStream = null;
            try
            {
                WebRequest lRequest = WebRequest.Create(aUrl);
                WebResponse lResponse = lRequest.GetResponse();
                lStream = lResponse.GetResponseStream();
            }
            catch (Exception)
            {
                throw new Exception($"Problem downloading resource {aUrl}");
            }
            return lStream;
        }

        private static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[16 * 1024];
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, read);
            }
        }

        public static void WriteStreamToFile(Stream aStream, string aFilename)
        {
            using (var lFile = new FileStream(aFilename, FileMode.Create, FileAccess.Write))
            {
                aStream.CopyTo(lFile);
            }

        }

        public static Stream GetFileStream(string aFilename)
        {
            return new FileStream(aFilename, FileMode.Open, FileAccess.Read);
        }
        //public static Image GetImageFromUrl(string aUrl)
        //{
        //    Image lImage = null;
        //    using (Stream lStream = GetUrlStream(aUrl))
        //    {
        //        if (lStream != null)
        //        {
        //            lImage = Image.FromStream(lStream);
        //            lStream.Close();
        //        }
        //    }
        //    return lImage;
        //}
    }
}
