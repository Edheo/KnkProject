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
        private KnkCriteria<Movie,Movie> _CurrentCriteria;
        private readonly KnkConnection _Connection;
        private Movies _Movies;
        bool _Initialized = false;

        private MovieWall()
        {
            InitializeComponent();
            InitComboSort();
            btnClear.Factor(new System.Drawing.Size(1, 1));
            btnSearch.Factor(new System.Drawing.Size(1, 1));
            _Initialized = true;
            btnSearch.AnimationStop();
            moviesWall.LoadingItems += (s, e) => { OnStart(); };
            moviesWall.LoadedItems += (s, e) => { OnFinish(); };
        }

        public MovieWall(KnkConnection aCon) : this()
        {
            _Connection = aCon;
            OnPerformSearch();
        }

        private void  InitComboSort()
        {
            cmbSort.DataSource = new BindingSource(ItemsComboSort(), null); // Key => null
            cmbSort.DisplayMember = "Key";
            cmbSort.ValueMember = "Value";
            cmbSort.SelectedValue = "CreationDate:Desc";
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
                if (cmbArtist.Items.Count.Equals(0)) LoadCombo<Casting>(cmbArtist, "ArtistName", new Castings(_Connection).Datasource());
                if (cmbGenres.Items.Count.Equals(0)) LoadCombo<Genre>(cmbGenres, "GenreName", new Genres(_Connection).Datasource());
                if (cmbSaga.Items.Count.Equals(0)) LoadCombo<MovieSet>(cmbSaga, "Name", new MovieSets(_Connection).Datasource());
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
            if (PerformSearch != null)
            {
                CancelEventArgs lArgs = new CancelEventArgs();
                PerformSearch(this, lArgs);
                lCancel = lArgs.Cancel;
            }
            if (!lCancel)
            {
                GenerateCriteria();
                Movies lMov = new Movies(_Connection, _CurrentCriteria);
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

        private void GenerateCriteria()
        {
            KnkCriteria<Movie, Movie> lCri = new KnkCriteria<Movie, Movie>(new Movie(), new KnkTableEntity("vieMovies", "Movies"));
            KnkCoreUtils.CreateInParameter(new MovieUsers(_Connection), lCri, "IdMovie");
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
                KnkCoreUtils.CreateInParameter(new MovieCastings(_Connection, $"%{cmbArtist.Text}%"), lCri, "IdMovie");
            }

            if (!string.IsNullOrEmpty(cmbGenres.Text))
            {
                KnkCoreUtils.CreateInParameter(new MovieGenres(_Connection, $"%{cmbGenres.Text}%"), lCri, "IdMovie");
            }

            if (!string.IsNullOrEmpty(cmbSaga.Text))
            {
                KnkCoreUtils.CreateInParameter(new MovieMovieSets(_Connection, $"%{cmbSaga.Text}%"), lCri, "IdMovie");
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
            ScanLibrariesForm lFrm = new ScanLibrariesForm("movies", _Connection);
            lFrm.Show();
        }
    }
}
