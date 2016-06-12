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
using System.Text.RegularExpressions;

namespace KnkMovieForms.Usercontrols
{
    public partial class MovieWall : UserControl
    {
        delegate void delLoadComboList<T>(ComboBox aCombo, List<T> aList);

        public event CancelEventHandler PerformSearch;
        private KnkConnection _Connection;
        private Movies _Movies;
        bool _Initialized = false;

        public MovieWall()
        {
            InitializeComponent();
            InitComboSort();
            //moviesWall.LoadingItems += (s, e) => { OnStart(); };
            moviesWall.LoadedItems += (s, e) => { OnFinish(); };
            _Initialized = true;
            OnPerformSearch();
        }

        private void OnFinish()
        {
            lblCount.Text = $"{moviesWall.LoadedMovies()}/{_Movies.Count()}";
        }

        public KnkConnectionItf Connection()
        {
            if(_Connection==null)
                _Connection = new KnkConnection();
            return _Connection;
        }

        private void  InitComboSort()
        {
            cmbSort.DataSource = new BindingSource(ItemsComboSort(), null); // Key => null
            cmbSort.DisplayMember = "Key";
            cmbSort.ValueMember = "Value";
            cmbSort.SelectedValue = "CreationDate:Desc";
        }

        private void LoadMovies(Movies aMovies)
        {
            _Movies = aMovies;
            Thread lThr = new Thread(new ThreadStart(LoadMoviesThreaded));
            lThr.Start();
        }

        public void LoadMoviesThreaded()
        {
            if (_Movies != null)
            {
                if (cmbArtist.Items.Count.Equals(0)) LoadCombo<Casting>(cmbArtist, "ArtistName", new Castings(Connection()).Datasource());
                if (cmbGenres.Items.Count.Equals(0)) LoadCombo<Genre>(cmbGenres, "GenreName", new Genres(Connection()).Datasource());
                if (cmbSaga.Items.Count.Equals(0)) LoadCombo<MovieSet>(cmbSaga, "Name", new MovieSets(Connection()).Datasource());
                moviesWall.LoadMovies(_Movies);
            }
        }

        private Dictionary<string, string> ItemsComboSort()
        {
            Dictionary<string, string> lDic = new Dictionary<string, string>();
            Movie lMov = new Movie();
            foreach (var lPrp in KnkInterfaces.Utilities.KnkInterfacesUtils.GetProperties(lMov, false)) 
            {
                string lName = lPrp.Name.ToLower();
                string lDescrip = SplitOnCapitals(lPrp.Name);
                switch(lName)
                {
                    case "idmovie":
                    case "tagline":
                    case "imdbid":
                    case "tmdbid":
                    case "seconds":
                    case "mparating":
                    //case "originaltitle":
                    case "trailerurl":
                    case "idset":
                    case "adultcontent":
                    case "homepage":
                    case "deleted":
                    case "deleteddate":
                    case "usercreationid":
                    case "usermodifiedid":
                    case "userdeletedid":
                    case "creationtext":
                    case "modifiedtext":
                    case "deletedtext":
                        break;
                    default:
                        lDic.Add($"{lDescrip}", $"{lPrp.Name}:Asc");
                        lDic.Add($"{lDescrip} Descending", $"{lPrp.Name}:Desc");
                        break;
                }
            }
            return lDic;
        }

        public string SplitOnCapitals(string text)
        {
            Regex regex = new Regex(@"\p{Lu}\p{Ll}*");
            var lLst = regex.Matches(text).Cast<Match>().Select(m => m.Value);
            return lLst.Aggregate((i,j) => $"{i} {j}");
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
            if ((LicenseManager.UsageMode == LicenseUsageMode.Designtime)) return;
            if (PerformSearch != null)
            {
                CancelEventArgs lArgs = new CancelEventArgs();
                PerformSearch(this, lArgs);
                lCancel = lArgs.Cancel;
            }
            if (!lCancel)
            {
                Movies lMov = new Movies(Connection());
                lMov.Criteria = GetCriteria(lMov);
                string[] lSort = cmbSort.SelectedValue.ToString().Split(':');
                lMov.SortProperty = lSort[0];
                if (lSort[1].Equals("Asc"))
                    lMov.SortDirectionAsc = true;
                else
                    lMov.SortDirectionAsc = false;
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

        private KnkCriteria<Movie, Movie> GetCriteria(Movies aList)
        {
            KnkCriteria<Movie, Movie> lCri = new KnkCriteria<Movie, Movie>(aList, new KnkTableEntity("vieMovies", "Movies"));
            KnkCoreUtils.CreateInParameter(new MovieUsers(Connection()), lCri, "IdMovie");
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
                KnkCoreUtils.CreateInParameter(new MovieCastings(Connection(), $"%{cmbArtist.Text}%"), lCri, "IdMovie");
            }

            if (!string.IsNullOrEmpty(cmbGenres.Text))
            {
                KnkCoreUtils.CreateInParameter(new MovieGenres(Connection(), $"%{cmbGenres.Text}%"), lCri, "IdMovie");
            }

            if (!string.IsNullOrEmpty(cmbSaga.Text))
            {
                KnkCoreUtils.CreateInParameter(new MovieMovieSets(Connection(), $"%{cmbSaga.Text}%"), lCri, "IdMovie");
            }

            if (!chkViewed.CheckState.Equals(CheckState.Indeterminate))
            {
                if(chkViewed.Checked)
                {
                    lCri.AddParameter(typeof(int), "ViewedTimes", OperatorsEnu.GreatThan, $"0");
                }
                else
                {
                    lCri.AddParameter(typeof(int), "ViewedTimes", OperatorsEnu.LowerThan, $"1");
                }
            }
            if (!lCri.HasParameters())
                lCri = null;
            return lCri;
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
            ScanLibrariesForm lFrm = new ScanLibrariesForm("movies", Connection());
            lFrm.Show();
        }
    }
}
