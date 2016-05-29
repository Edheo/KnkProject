﻿using System.Collections.Generic;
using System.Linq;
using System.Net.TMDb;
using System.Threading;
using System.Threading.Tasks;

namespace KnkScrapers.Utilities
{
    internal static class KnkScraperTmdb
    {
        public static List<Movie> FindMovies(KnkSolutionMovies.Entities.File aFile)
        {
            var lTask1 = Task.Factory.StartNew(() => FindMoviesTsk(aFile));
            lTask1.Wait();
            return lTask1.Result.Result;
        }

        static string TitleWithoutYear(string aTitle)
        {
            string[] lSplit = aTitle.Split(' ');
            List<string> lJoin = new List<string>();
            foreach(var lStr in lSplit)
            {
                int lYear;
                if(!(lStr.Length.Equals(4) && int.TryParse(lStr,out lYear)))
                {
                    lJoin.Add(lStr);
                }
            }
            return lJoin.Aggregate((i, j) => $"{i} {j}");
        }

        static List<int> YearsFromFilename(string aTitle)
        {
            string[] lSplit = aTitle.Split(' ');
            List<int> lJoin = new List<int>();
            foreach (var lStr in lSplit)
            {
                int lYear;
                if (lStr.Length.Equals(4) && int.TryParse(lStr, out lYear))
                {
                    lJoin.Add(lYear);
                }
            }
            return lJoin.OrderByDescending(i => i).ToList();
        }

        struct Titleyear
        {
            public string Title;
            public int? Year;
        }

        public static async Task<List<Movie>> FindMoviesTsk(KnkSolutionMovies.Entities.File aFile)
        {
            using (var client = new ServiceClient("70fe8ec336e37f969f34bf2d69ca7f22"))
            {
                var lTitle = aFile.Extender.TitleFromFilename();
                var lTitleNoYear = TitleWithoutYear(lTitle);
                var lYears = YearsFromFilename(lTitle);
                List<Titleyear> lItems = new List<Titleyear>();
                if(lTitleNoYear.Length>4 && lYears.Count>0)
                {
                    foreach(int lYear in lYears)
                        lItems.Add(new Titleyear() { Title = lTitleNoYear, Year = lYear });
                }
                else
                {
                    lItems.Add(new Titleyear() { Title = lTitle, Year = null });
                }

                List<Movie> lLst = new List<Movie>();

                foreach (var lItm in lItems)
                {
                    var movies = await client.Movies.SearchAsync(lItm.Title, "es", true, lItm.Year, false, 1, CancellationToken.None);
                    int count = movies.PageCount;
                    foreach (Movie lMovie in movies.Results)
                    {
                        var movie = await client.Movies.GetAsync(lMovie.Id, "es", true, CancellationToken.None);
                        lLst.Add(movie);
                    }
                }

                if(lLst.Count==0)
                {
                    foreach (var lItm in lItems)
                    {
                        var movies = await client.Movies.SearchAsync(lItm.Title, "es", true, null, false, 1, CancellationToken.None);
                        int count = movies.PageCount;
                        foreach (Movie lMovie in movies.Results)
                        {
                            var movie = await client.Movies.GetAsync(lMovie.Id, "es", true, CancellationToken.None);
                            lLst.Add(movie);
                        }
                    }
                }
                return lLst;
            }
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
                        var movie = await client.Movies.GetAsync(m.Id, null, true, cancellationToken);

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