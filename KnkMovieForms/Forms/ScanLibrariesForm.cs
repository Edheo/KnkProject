using KnkCore;
using KnkCore.Utilities;
using KnkInterfaces.Enumerations;
using KnkInterfaces.Interfaces;
using KnkScrapers.Services;
using KnkSolutionMovies.Entities;
using KnkSolutionMovies.Lists;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace KnkMovieForms.Forms
{
    public partial class ScanLibrariesForm : Form
    {
        Folders _Folders = null;
        Files _Files = null;
        string _LibraryType = string.Empty;

        private ScanLibrariesForm()
        {
            InitializeComponent();
        }

        public ScanLibrariesForm(string aLibraryType, KnkConnectionItf aCon):this()
        {
            _LibraryType = aLibraryType;
            _Folders = new Folders(aCon);
            KnkCriteria<File, File> lCri = new KnkCriteria<File, File>(new File());
            KnkCoreUtils.CreateInParameter<File, File>(_Folders.GetListIds(MovieParentFolders()), lCri, "IdRoot");
            _Files = new Files(aCon, lCri);
            grdRoots.AutoGenerateColumns = true;
            grdRoots.DataSource = MovieParentFolders().Select(o => new { DateAdded = o.DateAdded, Path = o.Path, Files = o.Files }).ToList();
        }

        private void OnScanFolders()
        {
            ScanFolders lScan = new ScanFolders(MovieParentFolders(), _Folders, _Files);
            var lLst = lScan.FoldersScanner();
            var lResult1 = lLst.Folders.Select(f => new { Type="Folder", Selected = f.Status().Equals(UpdateStatusEnu.New), Action = f.Status().ToString(), Root=f.Extender.RootFolder.ToString(), Parent=f.Extender.ParentFolder.ToString(), Item = f.Path }).ToList();
            var lResult2 = lLst.Files.Select(f => new { Type = "File", Selected = f.Status().Equals(UpdateStatusEnu.New), Action = f.Status().ToString(), Root = f.Folder.Extender.RootFolder.ToString(), Parent = f.Folder.ToString(), Item = f.Filename }).ToList();
            var lResult = lResult1.Union(lResult2).ToList();
            grdResults.AutoGenerateColumns = true;
            grdResults.DataSource = lResult;
        }

        private List<Folder> MovieParentFolders()
        {
            return (from f in _Folders.Items 
            where f.IdParentPath.GetInnerValue() == null && (f.ContentType??string.Empty).Equals(_LibraryType) 
            select f).ToList();
        }

        private void butScan_Click(object sender, EventArgs e)
        {
            OnScanFolders();
        }

        private void btnDeletes_Click(object sender, EventArgs e)
        {
            _Folders.SaveChanges(UpdateStatusEnu.Delete);
            _Files.SaveChanges(UpdateStatusEnu.Delete);
        }

        private void btnUpdates_Click(object sender, EventArgs e)
        {
            _Folders.SaveChanges(UpdateStatusEnu.New);
            _Files.SaveChanges(UpdateStatusEnu.New);
        }
    }
}
