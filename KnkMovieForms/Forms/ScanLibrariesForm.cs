using KnkCore;
using KnkInterfaces.Interfaces;
using KnkScrapers.Services;
using KnkSolutionMovies.Entities;
using KnkSolutionMovies.Lists;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KnkMovieForms.Forms
{
    public partial class ScanLibrariesForm : Form
    {
        Folders _Folders = null;
        string _LibraryType = string.Empty;
        private ScanLibrariesForm()
        {
            InitializeComponent();
        }

        public ScanLibrariesForm(string aLibraryType, KnkConnectionItf aCon):this()
        {
            _LibraryType = aLibraryType;
            _Folders = new Folders(aCon);
            grdRoots.AutoGenerateColumns = true;
            grdRoots.DataSource = MovieParentFolders().Select(o => new
            { DateAdded = o.DateAdded, Path = o.Path, Files = o.Files }).ToList();
        }

        private void OnScanFolders()
        {
            ScanFolders lScan = new ScanFolders(_Folders, MovieParentFolders());
            lScan.StartFoldersScanner();
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
    }
}
