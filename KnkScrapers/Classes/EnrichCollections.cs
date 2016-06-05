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

        List<File> MissingFiles = new List<File>();

        internal readonly Files Files;
        internal readonly Folders Folders;
        internal readonly Movies Movies;
        internal readonly Genres Genres;
        internal readonly Companies Companies;
        internal readonly Countries Countries;
        internal readonly Languages Languages;

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

            Files = new Files(aCon, lCri);
            Movies = new Movies(aCon);
            Genres = new Genres(aCon);
            Companies = new Companies(aCon);
            Countries = new Countries(aCon);
            Languages = new Languages(aCon);
        }

        public List<KnkChangeDescriptorItf> StartScan()
        {
            FoldersScanner();

            MissingMovies lFiles = new MissingMovies(Files.Connection);

            var lFilesChanged = (from itm in Files.ItemsChanged() where itm.Status() != KnkInterfaces.Enumerations.UpdateStatusEnu.Delete select itm).ToList();

            MissingFiles = lFiles.Items.Union(lFilesChanged).ToList();


            ScrapFiles();
            return Result();
        }

        void ScrapFiles()
        {
            foreach (var lFil in MissingFiles)
            {
                ScrapFile(lFil);
            }
        }

        void ScrapFile(File aFile)
        {
            var lOrg = KnkScraperTmdb.FindMovies(aFile, "es-ES").FirstOrDefault();
            if (lOrg != null) EnrichMovie(lOrg, FindMovieInLibrary(lOrg), aFile);
        }

        Movie EnrichMovie(System.Net.TMDb.Movie aMovieOrg, Movie aMovieDst, File aFomFile)
        {
            var lUsr = new User();
            lUsr.PropertySet(lUsr.PrimaryKey(), aMovieDst.Connection().CurrentUserId());
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
                //	MediaCredits			Credits
                //	Images					Images
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
                FillUser(lDst, lUsr);                                           //  It belongs to the user
                FillFile(lDst, aFomFile);                                       //  File from library
                FillOverviews(lDst, lOrg.Overview);                             //	string					Overview
                FillGenres(lDst, lOrg.Genres.ToList());                         //	IEnumerable<Genre>		Genres
                FillCompanies(lDst, lOrg.Companies.ToList());                   //	IEnumerable<Company>	Companies
                FillCountries(lDst, lOrg.Countries.ToList());                   //	IEnumerable<Country>	Countries
                FillLanguages(lDst, lOrg.Languages.ToList());                   //	IEnumerable<Language>	Languages
                lDst.Update();
            }
            return lDst;
        }

        Movie FindMovieInLibrary(System.Net.TMDb.Movie aMovie)
        {
            int? lYear = aMovie?.ReleaseDate?.Year;
            var lMovieDst = (from mov in Movies.Items where mov.Title.ToLower() == aMovie.Title.ToLower() && mov.Year == lYear select mov).FirstOrDefault();
            if (lMovieDst == null)
            {
                lMovieDst = Movies.Create();
            }
            return lMovieDst;
        }

        MovieUser FillUser(Movie aMovie, User aUser)
        {
            var lUsers = aMovie.Users();
            var lFound = (from fil in lUsers.Items
                          where fil.IdUser.Equals(aUser.IdUser)
                          select fil).FirstOrDefault();
            var lReturn = lFound;
            if (lFound == null)
            {
                lReturn = lUsers.Create();
                lReturn.Movie = aMovie;
            }
            lReturn.User = aUser;
            lReturn.Update();
            return lReturn;
        }

        void FillCountries(Movie aMovie, List<System.Net.TMDb.Country> aColection)
        {
            aMovie.Companies().DeleteAll();
            foreach (var lItem in aColection)
            {
                CheckMovieCountry(lItem, aMovie);
            }
        }

        void FillCompanies(Movie aMovie, List<System.Net.TMDb.Company> aColection)
        {
            aMovie.Companies().DeleteAll();
            foreach (var lItem in aColection)
            {
                CheckMovieCompany(lItem, aMovie);
            }
        }

        void FillGenres(Movie aMovie, List<System.Net.TMDb.Genre> aColection)
        {
            aMovie.Genres().DeleteAll();
            foreach (var lItem in aColection)
            {
                CheckMovieGenre(lItem, aMovie);
            }
        }

        void FillLanguages(Movie aMovie, List<System.Net.TMDb.Language> aColection)
        {
            aMovie.Languages().DeleteAll();
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
            lReturn.Update();
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
            lChk.Update();
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
            lReturn.Update();
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
            lChk.Update();
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
            lChk.Update();
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
            lReturn.Update();
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
            lChk.Update();
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
            lReturn.Update();
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
            lReturn.Update();
            return lReturn;
        }

        void FillOverviews(Movie aMovie, string aOverviews)
        {
            var lSum = aMovie.Summary();
            lSum.DeleteAll();
            string[] lines = aOverviews?.Split(new string[] { ". " }, StringSplitOptions.None);
            if (lines != null)
            {
                int lOrdinal = 1;
                foreach (var lLine in lines)
                {
                    var lLin = lSum.Create();
                    lLin.Ordinal = lOrdinal;
                    lLin.SummaryItem = lLine + ".";
                }
            }
        }

        void ScanFiles(Folder aFolder)
        {
            var lFiles = Files.Items.OrderByDescending(e => e.IdFile.GetInnerValue());
            foreach (var lFile in System.IO.Directory.GetFiles(aFolder.Path))
            {
                string lFileName = System.IO.Path.GetFileName(lFile);
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
                            lFil.Filename = lFileName;
                            lFil.DateAdded = DateTime.Now;
                            lFil.Folder = aFolder;
                            break;
                        case ".db":
                        case ".srt":
                        case ".ini":
                            break;
                        default:
                            break;
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
                    lFol.Delete();
                else
                    ScanFiles(lFol);
            }

            foreach (var lFile in Files.Items)
            {
                if (!System.IO.File.Exists(lFile.ToString()))
                    lFile.Delete();
                else
                {
                    DateTime lDat = System.IO.File.GetLastAccessTime(lFile.ToString());
                    if (lDat < lFile.DateAdded)
                    {
                        lFile.DateAdded = lDat;
                        lFile.Update();
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
                        lFol.Update();
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
                    if (lFol != null) lFol.Delete();
                }
            }
            else
            {
                if (lFol != null) lFol.Delete();
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
            lResult = lResult.Union(Languages.ListOfChanges()).ToList();
            lResult = lResult.Union(Countries.ListOfChanges()).ToList();
            lResult = lResult.Union(Companies.ListOfChanges()).ToList();
            return lResult;
        }

        public void SaveChanges()
        {
            Folders.SaveChanges();
            Files.SaveChanges();
            Movies.SaveChanges();
            Genres.SaveChanges();
            Languages.SaveChanges();
            Countries.SaveChanges();
            Companies.SaveChanges();
        }
    }
}
