using KnkCore;
using KnkForms.Utilities;
using KnkInterfaces.Enumerations;
using KnkInterfaces.Interfaces;
using KnkSolutionMovies.Entities;
using KnkSolutionMovies.Lists;
using KnkSolutionUsers.Lists;
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

namespace KnkMovies
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (!KnkUtility.CheckConfiguration()) Application.Current.Shutdown();
            LoadGrid();
        }

        private void LoadGrid()
        {
            dataGrid.ItemsSource = new Movies().Items;
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Movie lMovie = e.AddedItems[0] as Movie;
            if(lMovie!=null)
            {
                MovieControl.Movie = lMovie;
            }
        }
    }
}
