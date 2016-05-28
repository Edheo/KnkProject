using KnkScrappers.Utilities;
using KnkSolutionMovies.Entities;
using KnkSolutionMovies.Lists;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkScrapers.Services
{
    public class ScanFolders
    {
        private readonly Folders _Folders;
        private readonly List<Folder> _FoldersToScan;

        public ScanFolders(Folders aFolders, List<Folder> aFoldersToScan)
        {
            _Folders = aFolders;
            _FoldersToScan = aFoldersToScan;
        }

        public void StartFoldersScanner()
        {
            foreach (var lFol in _FoldersToScan)
            {
                ScanFolder(lFol.Path, null);
            }

        }

        private void ScanFolder(string aFolder, Folder aParentFolder)
        {

            bool lAvailable = false;
            if(KnkScrapersUtils.DirectoryExists(aFolder, out lAvailable))
            {
                if(lAvailable)
                {
                    var lFol = _Folders.Items.Find(e => e.Path.Equals(aFolder));
                    if(lFol==null && aParentFolder!=null)
                    {
                        lFol = aParentFolder.Clone<Folder>();
                        lFol.Path = aFolder;
                        lFol.ContentType = null;
                        lFol.Scraper = null;
                        lFol.Hash = null;
                        lFol.ScanRecursive = null;
                        lFol.strSettings = null;
                        lFol.NoUpdate = null;
                        lFol.Exclude = null;
                        lFol.IdParentPath = aParentFolder.IdPath;
                        _Folders.Add(lFol);
                    }
                    var lDirs = Directory.GetDirectories(aFolder);
                    {
                        foreach(var lFolder in lDirs)
                        {
                            ScanFolder(lFolder + "/", lFol);
                        }
                    }
                }
            }
            else
            {
                var lFol = _Folders.Items.Find(e => e.Path.Equals(aFolder));
            }

            //string lPath = KnkScrappers.Utilities.KnkScrapersUtils.FromUrlToPath(aFolder.Path);
            //foreach (string lFolderName in Directory.GetDirectories(lPath))
            //{
            //    Folder lFolder = _Folders.Items.Find(f => KnkScrappers.Utilities.KnkScrapersUtils.FromUrlToPath(f.Path) == lFolderName);
            //    if (lFolder == null)
            //    {
            //        lFolder = new Folder() { Path = lFolderName };
            //        _Folders.Items.Add(lFolder);
            //    }
            //    ScanFolder(lFolder);
            //}

            //foreach (string lFile in Directory.GetFiles(lPath))
            //{
            //    Console.WriteLine(lFile);
            //}
        }
    }
}
