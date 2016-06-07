using KnkCore;
using KnkCore.Utilities;
using KnkInterfaces.Interfaces;
using KnkInterfaces.Utilities;
using KnkScrapers.Utilities;
using KnkSolutionMovies.Entities;
using KnkSolutionMovies.Lists;
using KnkSolutionUsers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkScrapers.Classes
{
    public class EnrichCollections
    {
        public readonly List<Folder> Roots;
        internal readonly List<Folder> FoldersToScan;
        internal List<File> MissingFiles;

        public readonly MissingMovies MissingMovies;

        internal readonly Files Files;
        internal readonly Folders Folders;
        internal readonly Movies Movies;
        internal readonly Genres Genres;
        internal readonly Companies Companies;
        internal readonly Countries Countries;
        internal readonly Languages Languages;
        internal readonly Castings Castings;
        internal readonly CastingTypes CastingTypes;

        public EnrichCollections(KnkConnectionItf aCon, string aLibraryType)
        {
            Folders = new Folders(aCon);

            Roots = (from f in Folders.Items
                     where f.IdParentPath?.GetInnerValue() == null && (f.ContentType ?? string.Empty).Equals(aLibraryType)
                     select f).ToList();

            FoldersToScan = (from fol in Folders.Items
                             join rot in Roots
                             on fol.IdRoot.GetInnerValue() equals rot.IdPath.GetInnerValue()
                             orderby fol.IdPath descending
                             select fol).ToList();

            KnkCriteria<File, File> lCri = new KnkCriteria<File, File>(new File());
            KnkCoreUtils.CreateInParameter<File, File>(Folders.GetListIds(Roots), lCri, "IdRoot");

            MissingMovies = new MissingMovies(aCon);
            Files = new Files(aCon, lCri);
            Movies = new Movies(aCon);
            Genres = new Genres(aCon);
            Companies = new Companies(aCon);
            Countries = new Countries(aCon);
            Languages = new Languages(aCon);
            Castings = new Castings(aCon);
            CastingTypes = new CastingTypes(aCon);
        }

        public List<KnkChangeDescriptorItf> StartScanFiles()
        {
            FoldersScanner();
            return Result();
        }

        public List<KnkChangeDescriptorItf> StartScanScraper()
        {
            MissingFiles = (from fil in Files.Items
                            join mis in MissingMovies.Items
                            on (fil.IdFile?.GetInnerValue()) ?? 0 equals mis.IdFile.GetInnerValue()
                            where !fil.IsChanged()
                            select fil).ToList();
            ScrapFiles();
            return Result();
        }

        void ScrapFiles()
        {
            int i = 0;
            foreach (var lFil in MissingFiles)
            {
                if (ScrapFile(lFil))
                {
                    i++;
                }
                if (i >= 3) break;
            }
        }

        bool ScrapFile(File aFile)
        {
            var lOrg = KnkScraperTmdb.FindMovies(aFile, "es-ES").FirstOrDefault();
            if (lOrg != null)
                EnrichMovie(lOrg, FindMovieInLibrary(lOrg), aFile);
            else
            {
                aFile.Scraped++;
                aFile.Update("No Tmdb movie found!!");
            }
            return lOrg != null;
        }

        Movie EnrichMovie(System.Net.TMDb.Movie aMovieOrg, Movie aMovieDst, File aFomFile)
        {
            var lOrg = aMovieOrg;
            var lDst = aMovieDst;
            if (lDst != null)
            {
                aFomFile.Update("Movie found in Tmdb");
                lDst.Title = lOrg.Title;                                        //	string					Title
                lDst.OriginalTitle = lOrg.OriginalTitle;                        //	string					OriginalTitle
                lDst.TagLine = lOrg.TagLine;                                    //	string					TagLine
                //	string					Poster                              Will be imported in Images
                //	string					Backdrop                            Will be imported in Images
                lDst.AdultContent = lOrg.Adult;                                 //	bool					Adult
                //	Collection				BelongsTo                           Will not be imported
                lDst.Budget = lOrg.Budget;                                      //	int						Budget
                lDst.HomePage = lOrg.HomePage;                                  //	string					HomePage
                lDst.ImdbId = lOrg.Imdb;                                        //	string					Imdb
                lDst.TmdbId = lOrg.Id;                                          //	int 					Tmdb.Id
                lDst.ReleaseDate = lOrg.ReleaseDate;                            //	DateTime?				ReleaseDate
                lDst.Year = lOrg.ReleaseDate?.Year;                             //  Will be deleted in a future
                lDst.Revenue = lOrg.Revenue;                                    //	Int64					Revenue
                lDst.Seconds = lOrg.Runtime * 60;                               //	int?					Runtime
                //	AlternativeTitles		AlternativeTitles                   Will not be imported
                FillCasting(lDst, lOrg.Credits);                                //	MediaCredits			Credits
                FillMediaLinks(lDst, lOrg.Images, lOrg.Videos);                 //	Images					Images
                                                                                //	Videos					Videos
                lDst.TrailerUrl = lOrg.Videos.Results.FirstOrDefault()?.Site;
                //	Keywords				Keywords
                //	Releases				Releases
                //	Translations			Translations
                lDst.Popularity = lOrg.Popularity;                              //	decimal					Popularity
                lDst.Rating = lOrg.VoteAverage;                                 //	decimal					VoteAverage
                lDst.Votes = lOrg.VoteCount;                                    //	int						VoteCount
                //	string					Status
                //	ExternalIds				External
                lDst.ScrapedDate = DateTime.Now;
                FillUser(lDst, aMovieDst.Connection().CurrentUser());                                           //  It belongs to the user
                FillFile(lDst, aFomFile);                                       //  File from library
                FillSummaries(lDst, lOrg.Overview);                             //	string					Overview
                FillGenres(lDst, lOrg.Genres.ToList());                         //	IEnumerable<Genre>		Genres
                FillCompanies(lDst, lOrg.Companies.ToList());                   //	IEnumerable<Company>	Companies
                FillCountries(lDst, lOrg.Countries.ToList());                   //	IEnumerable<Country>	Countries
                FillLanguages(lDst, lOrg.Languages.ToList());                   //	IEnumerable<Language>	Languages
                if(lDst.IsNew())
                    lDst.Update("Movie Scraped From Tmdb");
                else
                    lDst.Update("Movie Re-Scraped From Tmdb");
            }
            return lDst;
        }

        Movie FindMovieInLibrary(System.Net.TMDb.Movie aMovie)
        {
            int? lYear = aMovie?.ReleaseDate?.Year;
            var lMovieDst = (from mov in Movies.Items where (mov.Title.ToLower() == aMovie.Title.ToLower() || mov.OriginalTitle.ToLower() == aMovie.OriginalTitle.ToLower()) && mov.Year == lYear select mov).FirstOrDefault();
            if (lMovieDst == null)
            {
                lMovieDst = Movies.Create();
            }
            return lMovieDst;
        }

        void FillCasting(Movie aMovie, System.Net.TMDb.MediaCredits aCredits)
        {
            aMovie.Casting().DeleteAll("Scrap replaces old Casting");
            foreach(var lItm in aCredits.Cast)
            {
                CheckMovieCasting(lItm, aMovie);
            }
            foreach (var lItm in aCredits.Crew)
            {
                CheckMovieCasting(lItm, aMovie);
            }
        }

        CastingType CheckCastingType(string aType)
        {
            var lFound = CastingTypes.Items.Where(g => g.Type.ToLower().Equals(aType.ToLower())).FirstOrDefault();
            var lReturn = lFound;
            if (lFound == null)
            {
                lFound = lReturn;
            }
            return lReturn;
        }

        void CheckMovieCasting(System.Net.TMDb.MediaCast aItem, Movie aMovie)
        {
            var lType = CheckCastingType("Actor");
            if (lType != null)
            {
                var lFound = aMovie.Casting().Items.Where(g => g.CastingType.Type.Equals(lType.Type) && g.Casting.ArtistName.ToLower().Equals(aItem.Name.ToLower())).FirstOrDefault();
                var lReturn = lFound;
                if (lFound == null)
                {
                    lReturn = aMovie.Casting().Create();
                    lReturn.Movie = aMovie;
                }
                lReturn.IdCastingType = lType;
                lReturn.Ordinal = aMovie.Casting().Items.Where(g => g.CastingType.Type.Equals(lType.Type) && !g.Deleted).Count() + 1;
                lReturn.Casting = CheckCasting(aItem);
                lReturn.Role = aItem.Character;
                lReturn.Update("Scraper checked Movie Casting");
            }
        }

        void CheckMovieCasting(System.Net.TMDb.MediaCrew aItem, Movie aMovie)
        {
            var lType = CheckCastingType(aItem.Job);
            if (lType != null)
            {

                var lFound = aMovie.Casting().Items.Where(g => g.CastingType.Type.Equals(lType.Type) && g.Casting.ArtistName.ToLower().Equals(aItem.Name.ToLower())).FirstOrDefault();
                var lReturn = lFound;
                if (lFound == null)
                {
                    lReturn = aMovie.Casting().Create();
                    lReturn.Movie = aMovie;
                }
                lReturn.IdCastingType = lType;
                lReturn.Ordinal = aMovie.Casting().Items.Where(g => g.CastingType.Type.Equals(lType.Type) && !g.Deleted).Count() + 1;
                lReturn.Casting = CheckCasting(aItem);
                lReturn.Role = aItem.Job;
                lReturn.Update("Scraper checked Movie Casting");
            }
        }

        Casting CheckCasting(System.Net.TMDb.MediaCredit aItem)
        {
            var lChk = Castings.Items.Where(g => g.ArtistName.ToLower().Equals(aItem.Name.ToLower())).FirstOrDefault();
            if (lChk == null)
            {
                lChk = Castings.Create();
            }
            lChk.ArtistName = aItem.Name;
            if (aItem.Person != null)
            {
                lChk.BirthDay = aItem.Person.BirthDay;
                lChk.DeathDay = aItem.Person.DeathDay;
                lChk.BirthPlace = aItem.Person.BirthPlace;
                lChk.ImdbId = aItem.Person.External.Imdb;
                lChk.TmdbId = aItem.Person.Id.ToString();
                lChk.TvdbId = aItem.Person.External.Tvdb?.ToString();
                FillBiography(lChk, aItem.Person.Biography);
                FillNames(lChk, aItem.Person);
                foreach (var lImg in aItem.Person.Images.Results)
                    CheckMediaLink(lImg.FilePath, null, lChk, 1);
            }
            lChk.Update("Scraper checked Casting");
            return lChk;
        }

        void FillMediaLinks(Movie aMovie, System.Net.TMDb.Images aImages, System.Net.TMDb.Videos aVideos)
        {
            aMovie.Pictures().DeleteAll("Scrap replaces old images");
            foreach (var lItem in aImages.Posters.ToList())
            {
                CheckMediaLink(lItem.FilePath, aMovie, null, 1); //Posters
            }
            foreach (var lItem in aImages.Backdrops.ToList())
            {
                CheckMediaLink(lItem.FilePath, aMovie, null, 2); //Fanarts
            }
            foreach (var lItem in aVideos.Results.ToList())
            {
                CheckMediaLink(lItem.Key, aMovie, null, 3); //Videos
            }
        }

        MediaLink CheckMediaLink(string aUrl, Movie aMovie, Casting aCasting, int aidType)
        {
            string lValue = aUrl.Replace("/", "");
            string lSite = "http://image.tmdb.org/t/p/original/{0}";
            string lSiteThumbnail = "http://image.tmdb.org/t/p/w500/{0}";
            if(aidType.Equals(3))
            {
                lSite = "https://www.youtube.com/watch?v={0}";
                lSiteThumbnail = "http://img.youtube.com/vi/{0}/default.jpg";
            }

            MediaLink lFound = null;
            int lMax = 0;
            if (aMovie != null)
            {
                lFound = aMovie.Pictures().Items.Where(g => g.ToString().ToLower().Equals(lValue.ToLower())).FirstOrDefault();
                if (aMovie.Pictures().Items.Count > 0) lMax = aMovie.Pictures().Items.Max(p => p.Ordinal);
            }
            if (aCasting != null)
            {
                //lSite = "http://thetvdb.com/banners/actors/{0}";
                //lSiteThumbnail = "http://image.tmdb.org/t/p/w500/{0}";
                if (lFound == null)
                {
                    lFound = aCasting.Pictures().Items.Where(g => g.ToString().ToLower().Equals(lValue.ToLower())).FirstOrDefault();
                    if (aCasting.Pictures().Items.Count > 0) lMax = aCasting.Pictures().Items.Max(p => p.Ordinal);
                }
            }
            var lReturn = lFound;
            if (lFound == null)
            {
                if (aMovie != null)
                    lReturn = aMovie.Pictures().Create();
                else if(aCasting!=null)
                    lReturn = aCasting.Pictures().Create();

                lReturn.Movie = aMovie;
                lReturn.Casting = aCasting;
                lReturn.Ordinal = lMax + 1;
            }
            lReturn.IdType = aidType;
            lReturn.Site = lSite;
            lReturn.SiteThumbnail = lSiteThumbnail;
            lReturn.Value = lValue;
            lReturn.Update("Scraper checked Link");
            return lReturn;
        }

        MovieUser FillUser(Movie aMovie, KnkItemItf aUser)
        {
            User lUsr = aUser as User;
            var lUsers = aMovie.Users();
            var lFound = (from fil in lUsers.Items
                          where fil.IdUser.Equals(lUsr.IdUser)
                          select fil).FirstOrDefault();
            var lReturn = lFound;
            if (lFound == null)
            {
                lReturn = lUsers.Create();
                lReturn.Movie = aMovie;
            }
            lReturn.User = lUsr;
            lReturn.Update("Movie added to User");
            return lReturn;
        }

        void FillCountries(Movie aMovie, List<System.Net.TMDb.Country> aColection)
        {
            aMovie.Companies().DeleteAll("Scrap replaces old countries");
            foreach (var lItem in aColection)
            {
                CheckMovieCountry(lItem, aMovie);
            }
        }

        void FillCompanies(Movie aMovie, List<System.Net.TMDb.Company> aColection)
        {
            aMovie.Companies().DeleteAll("Scrap replaces old companies");
            foreach (var lItem in aColection)
            {
                CheckMovieCompany(lItem, aMovie);
            }
        }

        void FillGenres(Movie aMovie, List<System.Net.TMDb.Genre> aColection)
        {
            aMovie.Genres().DeleteAll("Scrap replaces old Genres");
            foreach (var lItem in aColection)
            {
                CheckMovieGenre(lItem, aMovie);
            }
        }

        void FillLanguages(Movie aMovie, List<System.Net.TMDb.Language> aColection)
        {
            aMovie.Languages().DeleteAll("Scrap replaces old Languages");
            foreach (var lItem in aColection)
            {
                CheckMovieLanguage(lItem, aMovie);
            }
        }

        MovieLanguage CheckMovieLanguage(System.Net.TMDb.Language aItem, Movie aMovie)
        {
            var lFound = aMovie.Languages().Items.Where(g => g.ToString().ToLower().Equals(aItem.Name.ToLower())).FirstOrDefault();
            var lReturn = lFound;
            if (lFound == null)
            {
                lReturn = aMovie.Languages().Create();
                lReturn.Movie = aMovie;
            }
            lReturn.Language = CheckLanguage(aItem);
            lReturn.Update("Scraper checked Language");
            return lReturn;
        }

        Language CheckLanguage(System.Net.TMDb.Language aItem)
        {
            var lChk = Languages.Items.Where(g => g.ToString().ToLower().Equals(aItem.Name.ToLower())).FirstOrDefault();
            if (lChk == null)
            {
                lChk = Languages.Create();
            }
            lChk.Code = aItem.Code;
            lChk.Name = aItem.Name;
            lChk.Update("Scraper checked Language");
            return lChk;
        }

        MovieCountry CheckMovieCountry(System.Net.TMDb.Country aItem, Movie aMovie)
        {
            var lFound = aMovie.Countries().Items.Where(g => g.ToString().ToLower().Equals(aItem.Name.ToLower())).FirstOrDefault();
            var lReturn = lFound;
            if (lFound == null)
            {
                lReturn = aMovie.Countries().Create();
                lReturn.Movie = aMovie;
            }
            lReturn.Country = CheckCountry(aItem);
            lReturn.Update("Scraper checked Country");
            return lReturn;
        }

        Country CheckCountry(System.Net.TMDb.Country aItem)
        {
            var lChk = Countries.Items.Where(g => g.ToString().ToLower().Equals(aItem.Name.ToLower())).FirstOrDefault();
            if (lChk == null)
            {
                lChk = Countries.Create();
            }
            lChk.CountryName = aItem.Name;
            lChk.Update("Scraper checked Country");
            return lChk;
        }

        Genre CheckGenre(System.Net.TMDb.Genre aItem)
        {
            var lChk = Genres.Items.Where(g => g.GenreName.ToLower().Equals(aItem.Name.ToLower())).FirstOrDefault();
            if (lChk == null)
            {
                lChk = Genres.Create();
            }
            lChk.GenreName = aItem.Name;
            lChk.Update("Scraper checked Genre");
            return lChk;
        }

        MovieGenre CheckMovieGenre(System.Net.TMDb.Genre aItem, Movie aMovie)
        {
            var lFound = aMovie.Genres().Items.Where(g => g.ToString().ToLower().Equals(aItem.Name.ToLower())).FirstOrDefault();
            var lReturn = lFound;
            if (lFound == null)
            {
                lReturn = aMovie.Genres().Create();
                lReturn.Movie = aMovie;
            }
            lReturn.Genre = CheckGenre(aItem);
            lReturn.Update("Scraper checked Genre");
            return lReturn;
        }

        Company CheckCompany(System.Net.TMDb.Company aItem)
        {
            var lChk = Companies.Items.Where(g => g.CompanyName.ToLower().Equals(aItem.Name.ToLower())).FirstOrDefault();
            if (lChk == null)
            {
                lChk = Companies.Create();
            }
            lChk.CompanyName = aItem.Name;
            lChk.Description = aItem.Description;
            lChk.HeadQuarters = aItem.HeadQuarters;
            lChk.HomePage = aItem.HomePage;
            lChk.Logo = aItem.Logo;
            if (aItem.Parent != null) lChk.IdParentCompany = CheckCompany(aItem.Parent);
            lChk.Update("Scraper checked Company");
            return lChk;
        }

        MovieCompany CheckMovieCompany(System.Net.TMDb.Company aItem, Movie aMovie)
        {
            var lFound = aMovie.Companies().Items.Where(g => g.ToString().ToLower().Equals(aItem.Name.ToLower())).FirstOrDefault();
            var lReturn = lFound;
            if (lFound == null)
            {
                lReturn = aMovie.Companies().Create();
                lReturn.Movie = aMovie;
            }
            lReturn.Company = CheckCompany(aItem);
            lReturn.Update("Scraper checked Company");
            return lReturn;
        }

        MediaFile FillFile(Movie aMovie, File aFile)
        {
            var lFiles = aMovie.Files();
            var lFound = (from fil in lFiles.Items
                        where fil.ToString().ToLower().Equals(aFile.ToString().ToLower())
                        select fil
                ).FirstOrDefault();
            var lReturn = lFound;
            if(lFound == null)
            {
                lReturn = lFiles.Create();
                lReturn.Movie = aMovie;
            }
            lReturn.File = aFile;
            lReturn.Update("File assigned to Movie");
            return lReturn;
        }

        void FillBiography(Casting aCasting, string aBiography)
        {
            var lBio = aCasting.Biography();
            lBio.DeleteAll("Scrap replaces old Biography");
            string[] lines = aBiography?.Split(new string[] { ". " }, StringSplitOptions.None);
            if (lines != null)
            {
                int lOrdinal = 1;
                foreach (var lLine in lines)
                {
                    var lLin = lBio.Create();
                    lLin.Casting = aCasting;
                    lLin.Ordinal = lOrdinal;
                    lLin.Text = lLine + ".";
                    lLin.Update("Scraper added Biography");
                }
            }
        }

        void FillNames(Casting aCasting, System.Net.TMDb.Person aPerson)
        {
            var lNam = aCasting.Names();
            lNam.DeleteAll("Scraper replaces old Names");
            foreach (var lNamPer in aPerson.KnownAs)
            {
                var Found = lNam.Items.Find(n => n.Name.ToLower().Equals(lNamPer.ToLower()));
                if (Found == null) Found = lNam.Create();
                Found.Casting = aCasting;
                Found.Name = lNamPer;
                Found.Update("Scraper added Name");
            }
        }


        void FillSummaries(Movie aMovie, string aOverviews)
        {
            var lSum = aMovie.Summary();
            lSum.DeleteAll("Scraper replaces old Summaries");
            string[] lines = aOverviews?.Split(new string[] { ". " }, StringSplitOptions.None);
            if (lines != null)
            {
                int lOrdinal = 1;
                foreach (var lLine in lines)
                {
                    var lLin = lSum.Create();
                    lLin.IdMovie = aMovie;
                    lLin.Ordinal = lOrdinal;
                    lLin.SummaryItem = lLine + ".";
                    lLin.Update("Scraper added Sumary");
                }
            }
        }

        void ScanFiles(Folder aFolder)
        {
            //var lFiles = Files.Items.OrderByDescending(e => e.IdFile.GetInnerValue());
            foreach (var lFile in System.IO.Directory.GetFiles(aFolder.Path))
            {
                string lFileName = System.IO.Path.GetFileName(lFile);
                System.IO.FileInfo lInfo = new System.IO.FileInfo(lFile);
                var lFil = Files.Items.Where(e => e.Filename.ToLower().Equals(lFileName.ToLower()) && e.Folder.Path.ToLower().Equals(aFolder.Path.ToLower())).FirstOrDefault();
                if (lFil == null)
                {
                    string lVal = System.IO.Path.GetExtension(lFileName).ToLower();
                    switch (lVal)
                    {
                        case ".avi":
                        case ".mkv":
                        case ".mpg":
                            lFil = Files.Create();
                            break;
                        case ".db":
                        case ".srt":
                        case ".ini":
                            break;
                        default:
                            break;
                    }
                    if (lFil != null)
                    {
                        lFil.Filename = lFileName;
                        lFil.Filedate = lInfo.LastWriteTime;
                        lFil.Folder = aFolder;
                        lFil.Update("New File Scanned");
                    }
                }
            }
        }

        void FoldersScanner()
        {
            List<KnkChangeDescriptorItf> lReturn = new List<KnkChangeDescriptorItf>();
            foreach (var lFol in Roots)
            {
                ScanFolder(lFol.Path, null);
            }

            foreach (var lFol in FoldersToScan)
            {
                if (!CheckFolder(lFol.Path))
                    lFol.Delete("Missing folder");
                else
                    ScanFiles(lFol);
            }

            foreach (var lFile in Files.Items)
            {
                if (!System.IO.File.Exists(lFile.ToString()))
                    lFile.Delete("Missing File");
                else
                {
                    DateTime lDat = System.IO.File.GetLastWriteTime(lFile.ToString());
                    int lSeconds = (int)Math.Abs((lDat - lFile.Filedate).TotalSeconds);
                    if (lSeconds!=0)
                    {
                        lFile.Filedate = lDat;
                        lFile.Update("Filedate Changed");
                    }
                }
            }
        }

        private void ScanFolder(string aFolder, Folder aParentFolder)
        {
            bool lAvailable;
            var lFol = Folders.Items.Find(e => e.Path.Equals(aFolder));
            if (CheckFolder(aFolder, out lAvailable))
            {
                if (lAvailable)
                {
                    if (lFol == null && aParentFolder != null)
                    {
                        lFol = Folders.Create();
                        lFol.Path = aFolder;
                        lFol.ContentType = null;
                        lFol.Scraper = null;
                        lFol.Hash = null;
                        lFol.ScanRecursive = null;
                        lFol.UseFolderNames = null;
                        lFol.strSettings = null;
                        lFol.NoUpdate = null;
                        lFol.Exclude = null;
                        lFol.ParentFolder = aParentFolder;
                        lFol.RootFolder = aParentFolder.RootFolder;
                        lFol.Update("Folder added to Library");
                    }
                    var lDirs = System.IO.Directory.GetDirectories(aFolder);
                    {
                        foreach (var lFolder in lDirs)
                        {
                            ScanFolder(lFolder + "/", lFol);
                        }
                    }
                }
                else
                {
                    if (lFol != null) lFol.Delete("Folder not available");
                }
            }
            else
            {
                if (lFol != null) lFol.Delete("Folder check Fails");
            }
        }

        bool CheckFolder(string aFolder)
        {
            bool lAvailable;
            return CheckFolder(aFolder, out lAvailable);
        }

        bool CheckFolder(string aFolder, out bool aAvailable)
        {
            return (KnkScrapersUtils.DirectoryExists(aFolder, out aAvailable));
        }

        List<KnkChangeDescriptorItf> Result()
        {
            var lResult = new List<KnkChangeDescriptorItf>();
            lResult = lResult.Union(Folders.ListOfChanges()).ToList();
            lResult = lResult.Union(Files.ListOfChanges()).ToList();
            lResult = lResult.Union(Movies.ListOfChanges()).ToList();
            lResult = lResult.Union(Genres.ListOfChanges()).ToList();
            lResult = lResult.Union(Castings.ListOfChanges()).ToList();
            lResult = lResult.Union(Languages.ListOfChanges()).ToList();
            lResult = lResult.Union(Countries.ListOfChanges()).ToList();
            lResult = lResult.Union(Companies.ListOfChanges()).ToList();
            lResult = lResult.Union(CastingTypes.ListOfChanges()).ToList();
            return lResult;
        }

        public void SaveChanges()
        {
            Folders.SaveChanges();
            Files.SaveChanges();
            Genres.SaveChanges();
            Languages.SaveChanges();
            Countries.SaveChanges();
            Companies.SaveChanges();
            CastingTypes.SaveChanges();
            Castings.SaveChanges();
            Movies.SaveChanges();
        }
    }
}
