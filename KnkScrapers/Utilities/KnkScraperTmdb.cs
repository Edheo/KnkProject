using KnkInterfaces.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.TMDb;
using System.Threading;
using System.Threading.Tasks;

namespace KnkScrapers.Utilities
{
    internal static class KnkScraperTmdb
    {
        public static string TitleWithoutYear(string aTitle)
        {
            string[] lSplit = aTitle.Split(' ');
            List<string> lJoin = new List<string>();
            foreach(var lStr in lSplit)
            {
                int? lYear = IsAYear(lStr);
                if (lYear == null)
                {
                    lJoin.Add(lStr);
                }
            }
            if (lJoin.Count.Equals(0))
                lJoin.Add(lSplit.Max(e => e));
            return KnkInterfacesUtils.ConcatStrings(lJoin, true, " ");
        }

        public static string YearsFromFilename(string aTitle, string aSearchTitle)
        {
            string[] lSplit = aTitle.Split(' ');
            List<string> lJoin = new List<string>();
            foreach (var lStr in lSplit)
            {
                int? lYear = IsAYear(lStr);
                if (lYear != null)
                {
                    lJoin.Add(lStr);
                }
            }
            return KnkInterfacesUtils.ConcatStrings(lJoin, false, ";");
        }

        static int? IsAYear(string aText)
        {
            var lReturn = ToNullableInt32(aText);
            if (!(lReturn != null && aText.Length.Equals(4) && lReturn > 1890 && lReturn <= DateTime.Now.Year + 1))
                lReturn = null;
            return lReturn;
        }

        public static int? ToNullableInt32(this string s)
        {
            int i;
            if (Int32.TryParse(s, out i)) return i;
            return null;
        }


        static async Task Sample(CancellationToken cancellationToken)
        {
            using (var client = new ServiceClient("70fe8ec336e37f969f34bf2d69ca7f22"))
            {
                for (int i = 1, count = 1000; i <= count; i++)
                {
                    var movies = await client.Movies.GetTopRatedAsync(null, i, cancellationToken);
                    count = movies.PageCount; // keep track of the actual page count

                    foreach (System.Net.TMDb.Movie m in movies.Results)
                    {
                        var movie = await client.Movies.GetAsync(m.Id, "es", true, cancellationToken);

                        var personIds = movie.Credits.Cast.Select(s => s.Id)
                            .Union(movie.Credits.Crew.Select(s => s.Id));

                        foreach (var id in personIds)
                        {
                            var person = await client.People.GetAsync(id, true, cancellationToken);

                            //foreach (var img in person.Images.Results)
                            //{
                            //    string filepath = Path.Combine("People", img.FilePath.TrimStart('/'));
                            //    await DownloadImage(img.FilePath, filepath, cancellationToken);
                            //}

                        }
                    }
                }
            }
        }
    }
}
