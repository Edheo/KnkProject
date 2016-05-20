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
        public MovieWall()
        {
            InitializeComponent();
        }

        public void LoadMovies(Movies aMovies)
        {
            int i = 0;
            foreach(var lMovie in aMovies.Items)
            {
                this.flowLayoutPanel1.Controls.Add(new MovieThumb(lMovie));
                i++;
                if (i > 20) break;
            }
        }
    }
}
