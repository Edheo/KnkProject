using KnkInterfaces.Enumerations;
using KnkScrappers.Utilities;
using KnkSolutionMovies.Entities;
using KnkSolutionMovies.Lists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkScrapers.Services
{
    public class ScanFolders
    {
        private readonly List<Folder> _RootsToScan;
        private readonly Folders _Folders;
        private readonly Files _Files;

        public struct ReturnValues
        {
            public List<Folder> Folders;
            public List<File> Files;
        }

        public ScanFolders(List<Folder> aRoots, Folders aFolders, Files aFiles)
        {
            _RootsToScan = aRoots;
            _Folders = aFolders;
            _Files = aFiles;
        }

        public ReturnValues FoldersScanner()
        {
            ReturnValues lRet = new ReturnValues();
            foreach (var lFol in _RootsToScan)
            {
                ScanFolder(lFol.Path, null);
            }

            foreach (var lFol in FoldersToScan())
            {
                if (!CheckFolder(lFol.Path))
                    lFol.Delete();
                else
                    ScanFiles(lFol);
            }

            foreach(var lFile in _Files.Items)
            {
                if (!System.IO.File.Exists(lFile.ToString()))
                    lFile.Delete();
            }

            lRet.Folders = (from itm in FoldersToScan() where itm.Status() != UpdateStatusEnu.NoChanges select itm).ToList();
            lRet.Files = (from itm in _Files.Items where itm.Status() != UpdateStatusEnu.NoChanges select itm).ToList();
            return lRet;
        }

        private void ScanFiles(Folder aFolder)
        {
            var lLss = FoldersToScan();
            var lFiles = _Files.Items.OrderByDescending(e => e.IdFile.GetInnerValue());
            foreach (var lFile in System.IO.Directory.GetFiles(aFolder.Path))
            {
                string lFileName = System.IO.Path.GetFileName(lFile);
                var lFil = _Files.Items.Where(e => e.Filename.ToLower().Equals(lFileName.ToLower()) && e.Folder.Path.ToLower().Equals(aFolder.Path.ToLower())).FirstOrDefault();
                if (lFil == null)
                {
                    string lVal = System.IO.Path.GetExtension(lFileName).ToLower();
                    switch (lVal)
                    {
                        case ".avi":
                        case ".mkv":
                        case ".mpg":
                            lFil = _Files.Create();
                            lFil.Filename = lFileName;
                            lFil.DateAdded = DateTime.Now;
                            lFil.Folder = aFolder;
                            _Files.Add(lFil);
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

        private List<Folder> FoldersToScan()
        {
            return (from fol in _Folders.Items
                    join rot in _RootsToScan
                    on fol.IdRoot.GetInnerValue() equals rot.IdPath.GetInnerValue()
                    orderby fol.IdPath descending
                    select fol).ToList();
        }

        private bool CheckFolder(string aFolder)
        {
            bool lAvailable;
            return CheckFolder(aFolder, out lAvailable);
        }

        private bool CheckFolder(string aFolder, out bool aAvailable)
        {
            return (KnkScrapersUtils.DirectoryExists(aFolder, out aAvailable));
        }

        private void ScanFolder(string aFolder, Folder aParentFolder)
        {
            bool lAvailable;
            var lFol = _Folders.Items.Find(e => e.Path.Equals(aFolder));
            if (CheckFolder(aFolder, out lAvailable))
            {
                if(lAvailable)
                {
                    if(lFol==null && aParentFolder!=null)
                    {
                        lFol = aParentFolder.Clone<Folder>();
                        lFol.Path = aFolder;
                        lFol.ContentType = null;
                        lFol.Scraper = null;
                        lFol.Hash = null;
                        lFol.ScanRecursive = null;
                        lFol.UseFolderNames = null;
                        lFol.strSettings = null;
                        lFol.NoUpdate = null;
                        lFol.Exclude = null;
                        lFol.DateAdded = DateTime.Now;
                        lFol.IdParentPath = aParentFolder.IdPath;
                        lFol.IdRoot = aParentFolder.IdRoot;
                        _Folders.Add(lFol);
                    }
                    var lDirs = System.IO.Directory.GetDirectories(aFolder);
                    {
                        foreach(var lFolder in lDirs)
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
                if(lFol!=null) lFol.Delete();
            }
        }
    }
}
