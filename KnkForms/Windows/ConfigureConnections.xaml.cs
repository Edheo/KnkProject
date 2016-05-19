using System.Collections.Generic;
using System.Windows;

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
