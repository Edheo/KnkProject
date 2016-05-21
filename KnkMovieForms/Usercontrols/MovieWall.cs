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

namespace KnkMovieForms.Usercontrols
{
    public partial class MovieWall : UserControl
    {
        event CancelEventHandler PerformSearch;
        
        private Movies _Movies;

        public MovieWall()
        {
            InitializeComponent();
            this.moviePictureBox1.Image = KnkMovieForms.Properties.Resources.search;
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
            if (PerformSearch != null)
            {
                CancelEventArgs lArgs = new CancelEventArgs();
                PerformSearch(this, lArgs);
                if(!lArgs.Cancel)
                {
                    Movies lMov = new Movies(_Movies.Connection, Criteria());
                }
            }
        }

        private KnkCriteriaItf<Movie, Movie> Criteria()
        {
            KnkTableEntity lEntity = new KnkTableEntity("vieMovies", "IdMovie");
            KnkCriteriaItf<Movie, Movie> lCri = new KnkCriteria<Movie, Movie>(new Movie(), lEntity);
            var lParameters = lCri.FeededParameters();
            lParameters.Add(new KnkParameter(typeof(string), "TextSearch", KnkInterfaces.Enumerations.OperatorsEnu.Like, $"%{txtSearch}%"));
            return null;
        }

        private void moviePictureBox1_Click(object sender, CancelEventArgs e)
        {
            OnPerformSearch();
        }
    }
}
