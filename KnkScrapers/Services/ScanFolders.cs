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
        private Folders _Folders = new Folders();
        public ScanFolders()
        {

        }

        public void StartFoldersScanner()
        {
            var lLst = _Folders.Items.Where(f => f.IdParentPath == null);

            lLst = _Folders.Items.Where(f => f.IdParentPath == null && f.Scraper == "metadata.themoviedb.org");
            foreach(Folder lFol in lLst)
            {
                ScanFolder(lFol);
            }
        }

        private void ScanFolder(Folder aFolder)
        {
            string lPath = KnkScrappers.Utilities.KnkScrapersUtils.FromUrlToPath(aFolder.Path);
            foreach (string lFolderName in Directory.GetDirectories(lPath))
            {
                Folder lFolder = _Folders.Items.Find(f => KnkScrappers.Utilities.KnkScrapersUtils.FromUrlToPath(f.Path) == lFolderName);
                if (lFolder == null)
                {
                    lFolder = new Folder() { Path = lFolderName };
                    _Folders.Items.Add(lFolder);
                }
                ScanFolder(lFolder);
            }

            foreach (string lFile in Directory.GetFiles(lPath))
            {
                Console.WriteLine(lFile);
            }
        }
    }
}
