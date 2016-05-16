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
using System.Windows.Shapes;

namespace KnkForms.Windows
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ConfigureConnections : Window
    {
        List<KnkInterfaces.Interfaces.KnkConfigurationItf> _Configs;

        public ConfigureConnections(List<KnkInterfaces.Interfaces.KnkConfigurationItf> aList)
        {
            InitializeComponent();
            _Configs = aList;
            dataGrid.ItemsSource = aList;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        public List<KnkInterfaces.Interfaces.KnkConfigurationItf> Configuration()
        {
            return _Configs;
        }
    }
}
