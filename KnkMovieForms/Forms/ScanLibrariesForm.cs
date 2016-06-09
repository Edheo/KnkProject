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
        BindingSource _Bing = new BindingSource();

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


        private void OnSyncFiles()
        {
            if (!_Enricher.IsBusy)
            {
                //System.Windows.Forms.Timer lTim = new System.Windows.Forms.Timer();
                //lTim.Interval = 2000;
                //lTim.Tick += (sender, args) =>
                //{
                //    grdResults.DataSource = typeof(List<>);
                //    _Bing.ResetBindings(true);
                //    grdResults.DataSource = _Bing;
                //};
                //lTim.Start();
                _Bing.DataSource = _Enricher.Results;

                var bw = new BackgroundWorker();
                bw.WorkerReportsProgress = true;
                btnScan.Image = global::KnkMovieForms.Properties.Resources.Ani200_2;
                bw.DoWork += (sender, args) =>
                {
                    _Enricher.StartScanner(bw);
                };
                bw.ProgressChanged += (sender, args) =>
                {
                    grdResults.DataSource = typeof(List<>);
                    _Bing.ResetBindings(true);
                    grdResults.DataSource = _Bing;
                };
                bw.RunWorkerCompleted += (sender, args) =>
                {
                    btnScan.Image = global::KnkMovieForms.Properties.Resources.btnScan_ResourceImage;
                };
                bw.RunWorkerAsync(); // starts the background worker
            }
            grdResults.DataSource = _Bing;
        }

        private void OnSaveChanges()
        {
            if (!_Enricher.IsBusy)
            {
                System.Windows.Forms.Timer lTim = new System.Windows.Forms.Timer();
                lTim.Interval = 2000;
                lTim.Tick += (sender, args) =>
                {
                    grdResults.DataSource = typeof(List<>);
                    _Bing.ResetBindings(true);
                    grdResults.DataSource = _Bing;
                };
                lTim.Start();

                var bw = new BackgroundWorker();
                bw.WorkerReportsProgress = true;
                btnScan.Image = global::KnkMovieForms.Properties.Resources.Ani200_2;
                bw.DoWork += (sender, args) =>
                {
                    _Enricher.SaveChanges(bw);
                };
                //bw.ProgressChanged += (sender, args) =>
                //{
                //    grdResults.DataSource = typeof(List<>);
                //    _Bing.ResetBindings(true);
                //    grdResults.DataSource = _Bing;
                //};
                bw.RunWorkerCompleted += (sender, args) =>
                {
                    btnScan.Image = global::KnkMovieForms.Properties.Resources.btnScan_ResourceImage;
                    lTim.Stop();
                };
                bw.RunWorkerAsync(); // starts the background worker
            }
            grdResults.DataSource = _Bing;
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
