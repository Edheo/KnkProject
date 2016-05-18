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
using WpfAutoGrid;

namespace KnkForms.Usercontrols
{
    /// <summary>
    /// Interaction logic for MoviesWall.xaml
    /// </summary>
    /// 
    public partial class MoviesWall : UserControl
    {
        public MoviesWall()
        {
            InitializeComponent();
            Size lMaxSize = MovieThumb.MaxSize();
            int lColumns = (int)Math.Ceiling(this.autoGrid.ActualWidth / lMaxSize.Width);
            this.autoGrid.Columns = lColumns.ToString();


        }
    }
}
