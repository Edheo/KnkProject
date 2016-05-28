using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using KnkSolutionMovies.Lists;
using KnkInterfaces.Interfaces;
using KnkSolutionMovies.Entities;
using KnkCore;
using KnkCore.Utilities;
using KnkInterfaces.Enumerations;
using System.Threading;
using KnkMovieForms.Forms;

namespace KnkMovieForms.Usercontrols
{
    public partial class MovieWall : UserControl
    {
        delegate void delLoadComboList<T>(ComboBox aCombo, List<T> aList);

        public event CancelEventHandler PerformSearch;
        private KnkCriteria<Movie,Movie> _CurrentCriteria;
        private Movies _Movies;
        bool _Initialized = false;

        public MovieWall()
        {
            InitializeComponent();
            btnClear.Factor(new System.Drawing.Size(1, 1));
            btnSearch.Factor(new System.Drawing.Size(1, 1));
            _Initialized = true;
            btnSearch.AnimationStop();
            moviesWall.LoadingItems += (s, e) => { OnStart(); };
            moviesWall.LoadedItems += (s, e) => { OnFinish(); };
        }

        private void OnStart()
        {
            btnSearch.AnimationStart();
        }

        private void OnFinish()
        {
            btnSearch.Caption = $"{moviesWall.LoadedMovies()}/{_Movies.Count()}";
            btnSearch.AnimationStop();
        }

        public void LoadMovies(Movies aMovies)
        {
            _Movies = aMovies;
            Thread lThr = new Thread(new ThreadStart(LoadMoviesThreaded));
            lThr.Start();
        }

        public void LoadMoviesThreaded()
        {
            if (_Movies != null)
            {
                LoadCombo<Casting>(cmbArtist, "ArtistName", new Castings(_Movies.Connection).Datasource());
                LoadCombo<GenreClass>(cmbGenres, "Genre", new Genres(_Movies.Connection).Datasource());
                LoadCombo<MovieSet>(cmbSaga, "Name", new MovieSets(_Movies.Connection).Datasource());
                moviesWall.LoadMovies(_Movies);
            }
        }


        private void LoadCombo<T>(ComboBox aCombo, string aDisplayMember, List<T> aList)
        {
            if (aCombo.Items.Count == 0)
            {
                aCombo.DisplayMember = aDisplayMember;
                if (InvokeRequired)
                    this.Invoke(new delLoadComboList<T>(LoadComboList<T>), aCombo, aList);
                else
                    LoadComboList<T>(aCombo, aList);
            }
        }

        private void LoadComboList<T>(ComboBox aCombo, List<T> aList)
        {
            aCombo.DataSource = aList;
            aCombo.SelectedIndex = -1;
        }

        private void MovieWall_SizeChanged(object sender, EventArgs e)
        {
            if(_Initialized) LoadMovies(_Movies);
        }

        private void OnPerformSearch()
        {
            bool lCancel = false;
            if (PerformSearch != null)
            {
                CancelEventArgs lArgs = new CancelEventArgs();
                PerformSearch(this, lArgs);
                lCancel = lArgs.Cancel;
            }
            if (!lCancel)
            {
                GenerateCriteria();
                Movies lMov = new Movies(_Movies.Connection, _CurrentCriteria);
                this.LoadMovies(lMov);
            }
        }

        private void OnClearParams()
        {
            txtSearch.Text = string.Empty;
            cmbArtist.SelectedIndex = -1;
            cmbGenres.SelectedIndex = -1;
            cmbSaga.SelectedIndex = -1;
            cmbArtist.SelectedIndex = -1;
            chkViewed.CheckState = CheckState.Indeterminate;
        }

        private void GenerateCriteria()
        {
            KnkCriteria<Movie, Movie> lCri = null;
            KnkTableEntity lEntity = new KnkTableEntity("vieMovies");
            lCri = new KnkCriteria<Movie, Movie>(new Movie(), lEntity);
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                string[] lSearch = txtSearch.Text.Split(' ');
                KnkParameterItf lPar = lCri.AddParameter(typeof(string), "TextSearch", OperatorsEnu.Like, $"%{txtSearch.Text}%");
                if (lSearch.Length > 1)
                {
                    foreach (string lStr in lSearch)
                    {
                        lPar.AddInnerParameter($"%{lStr}%");
                    }
                }
            }

            if (!string.IsNullOrEmpty(cmbArtist.Text))
            {
                KnkCoreUtils.CreateInParameter(new MovieCastings(_Movies.Connection, cmbArtist.Text), lCri, "IdMovie");
            }

            if (!string.IsNullOrEmpty(cmbGenres.Text))
            {
                KnkCoreUtils.CreateInParameter(new MovieGenres(_Movies.Connection, cmbGenres.Text), lCri, "IdMovie");
            }

            if (!string.IsNullOrEmpty(cmbSaga.Text))
            {
                KnkCoreUtils.CreateInParameter(new MovieMovieSets(_Movies.Connection, cmbSaga.Text), lCri, "IdMovie");
            }

            if (!chkViewed.CheckState.Equals(CheckState.Indeterminate))
            {
                if(chkViewed.Checked)
                {
                    lCri.AddParameter(typeof(int), "Plays", OperatorsEnu.GreatThan, $"0");
                }
                else
                {
                    lCri.AddParameter(typeof(int), "Plays", OperatorsEnu.LowerThan, $"1");
                }
            }
            if (!lCri.HasParameters())
                lCri = null;
            _CurrentCriteria = lCri;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            OnPerformSearch();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                OnPerformSearch();
                return true; 
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnClear_Load(object sender, EventArgs e)
        {
            OnClearParams();
        }

        private void butScan_Load(object sender, EventArgs e)
        {
            ScanLibrariesForm lFrm = new ScanLibrariesForm("movies", _Movies.Connection);
            lFrm.Show();
        }
    }
}
