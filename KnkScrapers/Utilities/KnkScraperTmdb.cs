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
                int? lYear = IsAYear(lStr);
                if (lYear == null)
                {
                    lJoin.Add(lStr);
                }
            }
            if (lJoin.Count.Equals(0))
                lJoin.Add(lSplit.Max(e => e));
            return lJoin.Aggregate((i, j) => $"{i} {j}");
        }

        static string YearsFromFilename(string aTitle, string aSearchTitle)
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
            return lJoin.Aggregate((i, j) => $"{i};{j}");
        }

        static int? IsAYear(string aText)
        {
            var lReturn = ToNullableInt32(aText);
            if (!(lReturn != null && aText.Length.Equals(4) && lReturn > 1890 && lReturn <= DateTime.Now.Year + 1))
                lReturn = null;
            return lReturn;
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
                if (string.IsNullOrEmpty(aFile.TitleSearch))
                {
                    aFile.TitleSearch = TitleWithoutYear(aFile.Extender.TitleFromFilename());
                    aFile.Update("Extracted Title");
                }
                var lTitleNoYear = aFile.TitleSearch;
                if (string.IsNullOrEmpty(aFile.YearSearch))
                {
                    aFile.YearSearch = YearsFromFilename(aFile.Extender.TitleFromFilename(), lTitleNoYear);
                    if (aFile.Status() == KnkInterfaces.Enumerations.UpdateStatusEnu.NoChanges) aFile.Update("Extracted Years");
                }
                var lYears = aFile.YearSearch;

                List<Titleyear> lItems = new List<Titleyear>();
                if(lYears.Split(';').Length>0)
                {
                    foreach(string lYear in lYears.Split(';'))
                        lItems.Add(new Titleyear() { Title = lTitleNoYear, Year = ToNullableInt32(lYear) });
                }
                else
                {
                    lItems.Add(new Titleyear() { Title = lTitleNoYear, Year = null });
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
                        break;
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
                            break;
                        }
                    }
                }
                return lLst;
            }
        }

        private static int? ToNullableInt32(this string s)
        {
            int i;
            if (Int32.TryParse(s, out i)) return i;
            return null;
        }

        public static async Task<Movie> GetMovieData(ServiceClient aClient, int aIdMovie, string aLanguage)
        {
            var movie = await aClient.Movies.GetAsync(aIdMovie, aLanguage, true, CancellationToken.None);
            movie.Images = await aClient.Movies.GetImagesAsync(movie.Id, null, CancellationToken.None);
            movie.Videos.Results = await aClient.Movies.GetVideosAsync(movie.Id, aLanguage, CancellationToken.None);
            foreach(var lCast in movie.Credits.Cast)
            {
                lCast.Person = await aClient.People.GetAsync(lCast.Id, true, CancellationToken.None);
            }
            foreach (var lCast in movie.Credits.Crew)
            {
                lCast.Person = await aClient.People.GetAsync(lCast.Id, true, CancellationToken.None);
            }
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
