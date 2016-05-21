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

namespace KnkMovieForms.Usercontrols
{
    public partial class MovieWall : UserControl
    {
        private Movies _Movies;

        public MovieWall()
        {
            InitializeComponent();
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
    }
}
