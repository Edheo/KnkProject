using KnkForms.Utilities;
using KnkScrapers.Services;
using KnkSolutionMovies.Entities;
using KnkSolutionMovies.Lists;
using System.Windows;
using System.Windows.Controls;

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
            //dataGrid.ItemsSource = new Movies().Items;

            //ScanFolders lScan = new ScanFolders();
            //lScan.StartFoldersScanner();
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
