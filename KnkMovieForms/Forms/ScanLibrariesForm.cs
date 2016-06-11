using KnkCore;
using KnkCore.Utilities;
using KnkInterfaces.Enumerations;
using KnkInterfaces.Interfaces;
using KnkScrapers.Classes;
using KnkSolutionMovies.Entities;
using KnkSolutionMovies.Lists;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KnkMovieForms.Forms
{
    public partial class ScanLibrariesForm : Form
    {
        KnkConnectionItf _Connnection;
        EnrichCollections _Enricher;
        string _LibraryType = string.Empty;
        //BindingSource _Bing = new BindingSource();

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
            grdResults.DataSource = _Enricher.DataSource();
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


        private void OnSyncFiles()
        {
            if (!_Enricher.IsBusy)
            {
                var bw = new BackgroundWorker();
                bw.WorkerReportsProgress = true;
                btnScan.Image = global::KnkMovieForms.Properties.Resources.Ani200_2;
                bw.DoWork += (sender, args) =>
                {
                    _Enricher.StartScanner(bw);
                };
                bw.ProgressChanged += (sender, args) => { ProcessChanged(); };
                bw.RunWorkerCompleted += (sender, args) => { ProcessFinished(); };
                bw.RunWorkerAsync(); // starts the background worker
            }
            grdResults.DataSource = _Enricher.DataSource();
        }

        void ProcessChanged()
        {
            grdResults.DataSource = typeof(List<>);
            grdResults.DataSource = _Enricher.DataSource();
        }

        void ProcessFinished()
        {
            btnScan.Image = null;
            btnUpdates.Image = null;
        }

        private void OnSaveChanges()
        {
            if (!_Enricher.IsBusy)
            {
                var bw = new BackgroundWorker();
                bw.WorkerReportsProgress = true;
                btnUpdates.Image = global::KnkMovieForms.Properties.Resources.Ani200_6;
                bw.DoWork += (sender, args) =>
                {
                    _Enricher.SaveChanges(bw);
                };
                bw.ProgressChanged += (sender, args) => { ProcessChanged(); };
                bw.RunWorkerCompleted += (sender, args) => { ProcessFinished(); };
                bw.RunWorkerAsync(); // starts the background worker
            }
            grdResults.DataSource = _Enricher.DataSource();
        }

        private void butScan_Click(object sender, EventArgs e)
        {
            OnSyncFiles();
        }

        private void btnUpdates_Click(object sender, EventArgs e)
        {
            OnSaveChanges();
        }
    }
}
