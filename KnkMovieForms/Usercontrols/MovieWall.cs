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

            int lMovieWidth = MovieControlWidth();

            foreach (var lMovie in aMovies.Items)
            {
                flowLayoutPanel1.Controls.Add(new MovieThumb(lMovie, lMovieWidth));
                i++;
                if (i > 20) break;
            }
        }

        private int MovieControlWidth()
        {
            int lMyWidth = flowLayoutPanel1.ClientSize.Width-2;
            return (int)Math.Ceiling(lMyWidth / (float)HowManyFits(lMyWidth));
        }

        private int HowManyFits(int aWdith)
        {
            int lMovieWidth = MovieThumb.NormalSize().Width;
            int lMinWidth = MovieThumb.GetMinimumSize().Width;
            int lReturnValue = (int)Math.Ceiling(aWdith / (float)lMovieWidth);
            float lCheckWidth= aWdith / (float)lReturnValue;
            if (lCheckWidth < lMinWidth && lReturnValue > 1)
                return lReturnValue--;
            return lReturnValue;
        }

    }
}
