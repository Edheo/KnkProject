using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KnkSolutionMovies.Lists;
using KnkInterfaces.Interfaces;
using KnkSolutionMovies.Entities;
using KnkCore;
using KnkInterfaces.Enumerations;

namespace KnkMovieForms.Usercontrols
{
    public partial class MovieWall : UserControl
    {
        public event CancelEventHandler PerformSearch;
        private KnkCriteria<Movie,Movie> _CurrentCriteria;
        private Movies _Movies;

        public MovieWall()
        {
            InitializeComponent();
            this.btnSearch.Image = KnkMovieForms.Properties.Resources.search;
        }

        public void LoadMovies(Movies aMovies)
        {
            _Movies = aMovies;
            this.movieWallLayout1.LoadMovies(_Movies);
        }

        private void MovieWall_SizeChanged(object sender, EventArgs e)
        {
            LoadMovies(_Movies);
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

        private void GenerateCriteria()
        {
            KnkCriteria<Movie, Movie> lCri = null;
            if (txtSearch.Text.Length > 0)
            {
                KnkTableEntity lEntity = new KnkTableEntity("vieMovies", "IdMovie");
                lCri = new KnkCriteria<Movie, Movie>(new Movie(), lEntity);
                var lParameters = lCri.FeededParameters();
                string[] lSearch = txtSearch.Text.Split(' ');
                KnkParameter lPar = new KnkParameter(typeof(string), "TextSearch", OperatorsEnu.Like, $"%{txtSearch.Text}%");
                if (lSearch.Length > 1)
                {
                    int i = 1;
                    foreach (string lStr in lSearch)
                    {
                        lPar.AddInnerParameter("TextSearch" + i.ToString(), $"%{lStr}%");
                        i++;
                    }
                }
                lParameters.Add(lPar);
            }
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
    }
}
