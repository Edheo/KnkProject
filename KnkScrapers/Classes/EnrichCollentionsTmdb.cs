using KnkScrapers.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.TMDb;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KnkScrapers.Classes
{
    public partial class EnrichCollections
    {
        public List<Movie> FindMovie(KnkSolutionMovies.Entities.File aFile, string aLanguage)
        {

            if (string.IsNullOrEmpty(aFile.TitleSearch))
            {
                aFile.TitleSearch = KnkScraperTmdb.TitleWithoutYear(aFile.Extender.TitleFromFilename());
                aFile.Update("Extracted Title");
            }
            var lTitleNoYear = aFile.TitleSearch;
            if (string.IsNullOrEmpty(aFile.YearSearch))
            {
                aFile.YearSearch = KnkScraperTmdb.YearsFromFilename(aFile.Extender.TitleFromFilename(), lTitleNoYear);
                if (aFile.Status() == KnkInterfaces.Enumerations.UpdateStatusEnu.NoChanges) aFile.Update("Extracted Years");
            }
            var lYears = aFile.YearSearch;

            List<Titleyear> lItems = new List<Titleyear>();
            if (lYears.Split(';').Length > 0)
            {
                foreach (string lYear in lYears.Split(';'))
                    lItems.Add(new Titleyear() { Title = lTitleNoYear, Year = KnkScraperTmdb.ToNullableInt32(lYear) });
            }
            else
            {
                lItems.Add(new Titleyear() { Title = lTitleNoYear, Year = null });
            }
            return FindMovie(lItems, aLanguage);
        }

        public List<Movie> FindMovie(string aTitle, int? aYear, string aLanguage)
        {
            List<Titleyear> lItems = new List<Titleyear>();
            lItems.Add(new Titleyear() { Title = aTitle, Year = aYear });
            return FindMovie(lItems, aLanguage);
        }

        public List<Movie> FindMovie(List<Titleyear> aItems, string aLanguage)
        {
            var lTask1 = Task.Factory.StartNew(() => FindMovieTsk(aItems, aLanguage));
            lTask1.Wait();
            return lTask1.Result.Result;
        }

        public async Task<List<Movie>> FindMovieTsk(List<Titleyear> aItems, string aLanguage)
        {
            using (var client = new ServiceClient("70fe8ec336e37f969f34bf2d69ca7f22"))
            {
                List<Movie> lLst = new List<Movie>();
                try
                {
                    foreach (var lItm in aItems)
                    {
                        Status.UpdateMessage("Finding", $"{lItm.Title} {lItm.Year}");
                        _Worker?.ReportProgress(0);
                        var movies = await client.Movies.SearchAsync(lItm.Title, aLanguage, true, lItm.Year, false, 1, CancellationToken.None);
                        int count = movies.PageCount;
                        foreach (Movie lMovie in movies.Results)
                        {
                            Status.UpdateMessage("Found", $"{lItm.Title} {lItm.Year}");
                            _Worker?.ReportProgress(0);
                            var lTask1 = Task.Factory.StartNew(() => GetMovieData(client, lMovie.Id, aLanguage));
                            lTask1.Wait();
                            lLst.Add(lTask1.Result.Result);
                            break;
                        }
                    }

                    if (lLst.Count == 0)
                    {
                        foreach (var lItm in aItems)
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
                }
                catch(Exception lExc)
                {
                    this.Results.Add(new KnkCore.KnkChangeDescriptor("Tmdb Scraper", "FindMovies", lExc.Message));
                }

                return lLst;
            }
        }

        public async Task<Movie> GetMovieData(ServiceClient aClient, int aIdMovie, string aLanguage)
        {
            Movie movie = null;
            try
            {
                movie = await aClient.Movies.GetAsync(aIdMovie, aLanguage, true, CancellationToken.None);
                movie.Images = await aClient.Movies.GetImagesAsync(movie.Id, null, CancellationToken.None);
                movie.Videos.Results = await aClient.Movies.GetVideosAsync(movie.Id, aLanguage, CancellationToken.None);
                foreach (var lCast in movie.Credits.Cast)
                {
                    lCast.Person = await aClient.People.GetAsync(lCast.Id, true, CancellationToken.None);
                }
                foreach (var lCast in movie.Credits.Crew)
                {
                    KnkSolutionMovies.Entities.CastingType lTyp = CheckCastingType(lCast.Job, lCast.Department);
                    if(lTyp.ImportCrew)
                    {
                        lCast.Person = await aClient.People.GetAsync(lCast.Id, true, CancellationToken.None);
                    }
                }
            }
            catch
            {

            }
            return movie;
        }

        public struct Titleyear
        {
            public string Title;
            public int? Year;
        }

    }
}
