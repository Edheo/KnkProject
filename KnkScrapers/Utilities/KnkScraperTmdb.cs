using KnkScrapers.Classes;
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
        public static List<Movie> FindMovies(KnkSolutionMovies.Entities.File aFile, string aLanguage)
        {
            var lTask1 = Task.Factory.StartNew(() => FindMoviesTsk(aFile, aLanguage));
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
            if (lJoin.Count.Equals(0))
                lJoin.Add(lSplit.Max(e => e));
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

        public static async Task<List<Movie>> FindMoviesTsk(KnkSolutionMovies.Entities.File aFile, string aLanguage)
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
                    var movies = await client.Movies.SearchAsync(lItm.Title, aLanguage, true, lItm.Year, false, 1, CancellationToken.None);
                    int count = movies.PageCount;
                    foreach (Movie lMovie in movies.Results)
                    {
                        var lTask1 = Task.Factory.StartNew(() => GetMovieData(client, lMovie.Id, aLanguage));
                        lTask1.Wait();
                        lLst.Add(lTask1.Result.Result);
                    }
                }

                if(lLst.Count==0)
                {
                    foreach (var lItm in lItems)
                    {
                        var movies = await client.Movies.SearchAsync(lItm.Title, aLanguage, true, null, false, 1, CancellationToken.None);
                        int count = movies.PageCount;
                        foreach (Movie lMovie in movies.Results)
                        {
                            var lTask1 = Task.Factory.StartNew(() => GetMovieData(client, lMovie.Id, aLanguage));
                            lTask1.Wait();
                            lLst.Add(lTask1.Result.Result);
                        }
                    }
                }
                return lLst;
            }
        }

        public static async Task<Movie> GetMovieData(ServiceClient aClient, int aIdMovie, string aLanguage)
        {
            var movie = await aClient.Movies.GetAsync(aIdMovie, aLanguage, true, CancellationToken.None);
            //movie.Images = await aClient.Movies.GetImagesAsync(movie.Id, aLanguage, CancellationToken.None);
            //movie.Videos.Results = await aClient.Movies.GetVideosAsync(movie.Id, aLanguage, CancellationToken.None);
            
            //var personIds = movie.Credits.Cast.Select(s => s.Id)
            //    .Union(movie.Credits.Crew.Select(s => s.Id));

            //foreach (var id in personIds)
            //{
            //    var person = await aClient.People.GetAsync(id, true, CancellationToken.None);
            //}

            return movie;
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
