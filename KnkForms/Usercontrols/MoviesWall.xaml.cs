using KnkSolutionMovies.Entities;
using KnkSolutionMovies.Lists;
using System;
using System.Windows;
using System.Windows.Controls;

namespace KnkForms.Usercontrols
{
    /// <summary>
    /// Interaction logic for MoviesWall.xaml
    /// </summary>
    /// 
    public partial class MoviesWall : UserControl
    {
        private Movies _Movies;
        public MoviesWall()
        {
            InitializeComponent();
        }

        public void LoadMovies(Movies aMovies)
        {
            _Movies = aMovies;
            foreach(Movie lMovie in _Movies.Items)
            { 
                this.autoGrid.Children.Add(new MovieThumb() { Movie = lMovie });
            }

        }

        private int ColumsForWidth(double aWidth)
        {
            return (int)Math.Floor(aWidth / MovieThumb.MinSize().Width);
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            VerifySizeChange(e.PreviousSize, e.NewSize);
        }

        private void VerifySizeChange(Size aOldSize, Size aNewSize)
        {
            if (aOldSize.Width != aNewSize.Width)
            {
                int lColsOld = ColumsForWidth(aOldSize.Width);
                int lColsNew = ColumsForWidth(aNewSize.Width);
                if (lColsNew != lColsOld && lColsNew != 0)
                    this.SetColumns(lColsNew);
            }
        }

        private void SetColumns(int aColumns)
        {
            this.autoGrid.Columns = aColumns.ToString();
        }
    }
}
