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
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkScrapers.Classes
{
    public partial class EnrichCollections
    {
        public List<KnkChangeDescriptorItf> Results;
        KnkChangeDescriptorItf Status = null;
        public readonly List<Folder> Roots;
        internal readonly List<Folder> FoldersToScan;
        internal List<File> MissingFiles;
        BackgroundWorker _Worker;
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

        public bool IsScanning { get; private set; }
        public bool IsSaving { get; private set; }
        public bool IsBusy { get { return IsSaving || IsScanning; } }

        public EnrichCollections(KnkConnectionItf aCon, string aLibraryType)
        {
            Results = new List<KnkChangeDescriptorItf>();
            Status = new KnkChangeDescriptor(DateTime.Now, "Libraries", "Loading Data", "Folders");
            Results.Add(Status);
            Folders = new Folders(aCon) { Messages = Results };

            Status.UpdateMessage("Loading Data", "Roots");
            Roots = (from f in Folders.Items
                     where f.IdParentPath?.Value == null && (f.ContentType ?? string.Empty).Equals(aLibraryType)
                     select f).ToList();

            Status.UpdateMessage("Loading Data", "Folders to Scan");
            FoldersToScan = (from fol in Folders.Items
                             join rot in Roots
                             on fol.IdRoot.Value equals rot.IdPath.Value
                             orderby fol.IdPath descending
                             select fol).ToList();

            KnkCriteria<File, File> lCri = new KnkCriteria<File, File>(new File());
            KnkCoreUtils.CreateInParameter<File, File>(Folders.GetListIds(Roots), lCri, "IdRoot");

            MissingMovies = new MissingMovies(aCon);
            Files = new Files(aCon, lCri) { Messages = Results };
            Movies = new Movies(aCon) { Messages = Results };
            Genres = new Genres(aCon);
            Companies = new Companies(aCon);
            Countries = new Countries(aCon);
            Languages = new Languages(aCon);
            Castings = new Castings(aCon) { Messages = Results };
            CastingTypes = new CastingTypes(aCon);
        }

        public void StartScanner(BackgroundWorker aWorker)
        {
            if (!IsBusy)
            {
                IsScanning = true;
                _Worker = aWorker;
                FoldersScanner();
                Scraper();
                IsScanning = false;
            }
        }

        void Scraper()
        {
            Status.UpdateMessage("Loading Missing Files", "Scraper");
            MissingFiles = (from fil in Files.Items
                            join mis in MissingMovies.Items
                            on (fil.IdFile?.Value) ?? 0 equals mis.IdFile.Value
                            orderby fil.CreationDate
                            where !fil.IsChanged()
                            select fil).ToList();
            ScrapFiles();
            Status.UpdateMessage("Process Finished", string.Empty);
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
            Status.UpdateMessage("Scraping", aFile.Filename);
            var lOrg = FindMovies(aFile, "es-ES").FirstOrDefault();
            if (lOrg != null)
            {
                EnrichMovie(lOrg, FindMovieInLibrary(lOrg), aFile);
                aFile.Scraped = 1;
                aFile.Update("Movie found in Tmdb");
            }
            else
            {
                aFile.Scraped++;
                aFile.Update("No Tmdb movie found!!");
            }
            _Worker?.ReportProgress(0);
            return lOrg != null;
        }

        Movie EnrichMovie(System.Net.TMDb.Movie aMovieOrg, Movie aMovieDst, File aFomFile)
        {
            var lOrg = aMovieOrg;
            var lDst = aMovieDst;
            if (lDst != null)
            {
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
                //	Keywords				Keywords
                //	Releases				Releases
                //	Translations			Translations
                lDst.Popularity = lOrg.Popularity;                              //	decimal					Popularity
                lDst.Rating = lOrg.VoteAverage;                                 //	decimal					VoteAverage
                lDst.Votes = lOrg.VoteCount;                                    //	int						VoteCount
                //	string					Status
                //	ExternalIds				External
                lDst.ScrapedDate = DateTime.Now;
                FillCasting(lDst, lOrg.Credits);                                //	MediaCredits			Credits
                _Worker?.ReportProgress(0);
                FillMediaLinks(lDst, lOrg.Images, lOrg.Videos);                 //	Images					Images
                FillUser(lDst, aMovieDst.Connection().CurrentUser());                                           //  It belongs to the user
                FillFile(lDst, aFomFile);                                       //  File from library
                FillSummaries(lDst, lOrg.Overview);                             //	string					Overview
                _Worker?.ReportProgress(0);
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

        CastingType CheckCastingType(string aType, string aDepartment)
        {
            var lFound = CastingTypes.Items.Where(g => g.Type.ToLower().Equals(aType.ToLower())).FirstOrDefault();
            var lReturn = lFound;
            if (lFound == null)
            {
                lReturn = CastingTypes.Create();
                lReturn.Type = aType;
            }
            if(string.IsNullOrEmpty(lReturn.Department))
            {
                lReturn.Department = aDepartment;
                lReturn.Update("Scrap updates Casting Types");
            }
            return lReturn;
        }

        void CheckMovieCasting(System.Net.TMDb.MediaCast aItem, Movie aMovie)
        {
            var lType = CheckCastingType("Actor", "Casting");
            if (lType != null)
            {
                var lFound = aMovie.Casting().Items.Where(g => g.CastingType.Type.Equals(lType.Type) && g.Casting.ArtistName.ToLower().Equals(aItem.Name.ToLower())).FirstOrDefault();
                var lReturn = lFound;
                if (lFound == null)
                {
                    lReturn = aMovie.Casting().Create();
                    lReturn.IdMovie = aMovie;
                }
                lReturn.IdCastingType = lType;
                lReturn.Ordinal = aMovie.Casting().Items.Where(g => g.CastingType.Type.Equals(lType.Type) && !g.Deleted).Count() + 1;
                lReturn.IdCasting = CheckCasting(aItem);
                lReturn.Role = aItem.Character;
                lReturn.Update("Scraper checked Movie Casting");
            }
        }

        void CheckMovieCasting(System.Net.TMDb.MediaCrew aItem, Movie aMovie)
        {
            var lType = CheckCastingType(aItem.Job, aItem.Department);
            if (lType != null)
            {

                var lFound = aMovie.Casting().Items.Where(g => g.CastingType.Type.Equals(lType.Type) && g.Casting.ArtistName.ToLower().Equals(aItem.Name.ToLower())).FirstOrDefault();
                var lReturn = lFound;
                if (lFound == null)
                {
                    lReturn = aMovie.Casting().Create();
                    lReturn.IdMovie = aMovie;
                }
                lReturn.IdCastingType = lType;
                lReturn.Ordinal = aMovie.Casting().Items.Where(g => g.CastingType.Type.Equals(lType.Type) && !g.Deleted).Count() + 1;
                lReturn.IdCasting = CheckCasting(aItem);
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

        void FillMediaFile(Movie aMovie, File aFile)
        {
            var lReturn = aMovie.Files().Items.Find(f => f.IdFile.Reference.Filename.ToLower().Equals(aFile.Filename.ToLower()));
            if (lReturn == null)
            {
                lReturn = aMovie.Files().Create();
                lReturn.IdMovie = aMovie;
                lReturn.IdFile = aFile;
                lReturn.Update("Scrapert Linked File & Movie");
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

                lReturn.IdMovie = aMovie;
                lReturn.IdCasting = aCasting;
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
                lReturn.IdMovie = aMovie;
            }
            lReturn.IdUser = lUsr;
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
                lReturn.IdMovie = aMovie;
            }
            lReturn.IdLanguage = CheckLanguage(aItem);
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
                lReturn.IdMovie = aMovie;
            }
            lReturn.IdCountry = CheckCountry(aItem);
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
                lReturn.IdMovie = aMovie;
            }
            lReturn.IdGenre = CheckGenre(aItem);
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
                lReturn.IdMovie = aMovie;
            }
            lReturn.IdCompany = CheckCompany(aItem);
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
                lReturn.IdMovie = aMovie;
            }
            lReturn.IdFile = aFile;
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
                    lLin.IdCasting = aCasting;
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
                Found.IdCasting = aCasting;
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
                Status.UpdateMessage("Scanning Files", lFileName);
                System.IO.FileInfo lInfo = new System.IO.FileInfo(lFile);
                var lFil = Files.Items.Where(e => e.Filename.ToLower().Equals(lFileName.ToLower()) && e.IdPath.Reference.Path.ToLower().Equals(aFolder.Path.ToLower())).FirstOrDefault();
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
                        lFil.IdPath = aFolder;
                        lFil.Update("New File Scanned");
                    }
                }
                _Worker?.ReportProgress(0);
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
                Status.UpdateMessage("Checking Files", lFile.Filename);
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
            Status.UpdateMessage("Scanner", "Process Finissed");
        }

        private void ScanFolder(string aFolder, Folder aParentFolder)
        {
            Status.UpdateMessage("Scanning Folders", aFolder);
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
                        lFol.IdParentPath = aParentFolder;
                        lFol.IdRoot = aParentFolder.IdRoot.Reference;
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
            _Worker?.ReportProgress(0);
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

        public void SaveChanges(BackgroundWorker aWorker)
        {
            if (!IsBusy)
            {
                IsSaving = true;
                _Worker = aWorker;
                Folders.SaveChanges();
                aWorker.ReportProgress(0);
                Files.SaveChanges();
                aWorker.ReportProgress(0);
                CastingTypes.SaveChanges();
                aWorker.ReportProgress(0);
                Genres.SaveChanges();
                aWorker.ReportProgress(0);
                Languages.SaveChanges();
                aWorker.ReportProgress(0);
                Countries.SaveChanges();
                aWorker.ReportProgress(0);
                Companies.SaveChanges();
                aWorker.ReportProgress(0);
                Castings.SaveChanges();
                aWorker.ReportProgress(0);
                Movies.SaveChanges();
                aWorker.ReportProgress(0);
                IsSaving = false;
            }
        }
    }
}
