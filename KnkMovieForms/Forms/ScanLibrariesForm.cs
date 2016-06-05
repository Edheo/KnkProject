using KnkCore;
using KnkCore.Utilities;
using KnkInterfaces.Enumerations;
using KnkInterfaces.Interfaces;
using KnkScrapers.Classes;
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
        KnkConnectionItf _Connnection;
        EnrichCollections _Enricher;
        string _LibraryType = string.Empty;

        private ScanLibrariesForm()
        {
            InitializeComponent();
        }

        public ScanLibrariesForm(string aLibraryType, KnkConnectionItf aCon):this()
        {
            _Connnection = aCon;
            _LibraryType = aLibraryType;
            _Enricher = new EnrichCollections(_Connnection, aLibraryType);

            grdRoots.AutoGenerateColumns = true;
            grdRoots.DataSource = _Enricher.Roots.Select(o => new { DateAdded = o.CreationDate, Path = o.Path, Files = o.Files }).ToList();

            grdRoots.Width = GridRootsWidth();

            grdResults.AutoGenerateColumns = true;
            grdResults.DataSource = _Enricher.MissingMovies.Items;
        }

        private int GridRootsWidth()
        {
            int width = 0;
            foreach (DataGridViewColumn col in grdRoots.Columns)
            {
                width += col.Width;
            }
            width += grdRoots.RowHeadersWidth * 3;
            return width;
        }


        private void OnSyncLibraries()
        {
            grdResults.AutoGenerateColumns = true;
            grdResults.DataSource = _Enricher.StartScan();
        }

        private void butScan_Click(object sender, EventArgs e)
        {
            OnSyncLibraries();
        }

        private void btnUpdates_Click(object sender, EventArgs e)
        {
            _Enricher.SaveChanges();
        }
    }
}
