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
    partial class MovieWallLayout : UserControl
    {
        public MovieWallLayout()
        {
            InitializeComponent();
        }

        public void LoadMovies(Movies aMovies)
        {
            flowMovies.Controls.Clear();
            if (aMovies != null)
            {
                int i = 0;
                int lMovieWidth = MovieControlWidth();

                foreach (var lMovie in aMovies.Items)
                {
                    flowMovies.Controls.Add(new MovieThumb(lMovie, lMovieWidth));
                    i++;
                    if (i > VisibleItems()) break;
                }
            }
        }

        public int VisibleCols()
        {
            return HowManyColumns(flowMovies.ClientSize.Width);
        }

        public int VisibleRows()
        {
            return HowManyRows(MovieControlHeight());
        }

        public int VisibleItems()
        {
            return VisibleCols() * VisibleRows();
        }

        private int MovieControlWidth()
        {
            int lMyWidth = flowMovies.ClientSize.Width - 6;
            return (int)Math.Ceiling(lMyWidth / (float)VisibleCols());
        }

        private int MovieControlHeight()
        {
            return MovieThumb.GetHeightFromWidth(MovieControlWidth());
        }

        private int HowManyColumns(int aWdith)
        {
            int lMovieWidth = MovieThumb.NormalSize().Width;
            int lMinWidth = MovieThumb.GetMinimumSize().Width;
            int lReturnValue = (int)Math.Ceiling(aWdith / (float)lMovieWidth);
            float lCheckWidth= aWdith / (float)lReturnValue;
            if (lCheckWidth < lMinWidth && lReturnValue > 1)
                return lReturnValue--;
            return lReturnValue;
        }

        private int HowManyRows(int aHeight)
        {
            return aHeight == 0 ? 0 : (int)Math.Ceiling(flowMovies.ClientSize.Height / (float)aHeight);
        }

        //private void MovieWall_SizeChanged(object sender, EventArgs e)
        //{
        //    int lMovieWidth = MovieControlWidth();
        //    var lTmbs = this.flowMovies.Controls.OfType<MovieThumb>();
        //    foreach(var lThb in lTmbs)
        //    {
        //        lThb.SetSize(lMovieWidth);
        //    }
        //}
    }
}
