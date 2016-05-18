using KnkSolutionMovies.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace KnkForms.Usercontrols
{
    /// <summary>
    /// Interaction logic for Movie.xaml
    /// </summary>
    public partial class MovieThumb : UserControl
    {
        private Movie _Movie;


        public MovieThumb()
        {
            InitializeComponent();
        }

        public Movie Movie
        {
            get { return _Movie; }
            set { SetMovie(value); }
        }

        private void SetMovie(Movie aMovie)
        {
            _Movie = aMovie;
            lblTitle.Content = aMovie.Title;
            if (aMovie.Extender.Poster != null)
            {
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.StreamSource = Movie.Extender.Poster.Extender.GetImageStream();
                bi.EndInit();

                this.imgPicture.Source = bi;
            }
        }
        public static Size MinSize()
        {
            return new Size(200, 350);
        }

        public static Size MaxSize()
        {
            Size lSize = MinSize();
            return new Size(lSize.Width * 2, lSize.Height * 2);
        }

        public static float Aspect()
        {
            Size lMinSize = MinSize();
            return (float)lMinSize.Height / (float)lMinSize.Width;
        }

        public static Size SizeForWidth(int aWidth)
        {
            return new Size(aWidth, aWidth * Aspect());
        }
    }
}
