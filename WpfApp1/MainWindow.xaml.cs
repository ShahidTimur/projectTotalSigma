using BookJurnalLibrary;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CultureInfo newCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = newCulture;
            Thread.CurrentThread.CurrentUICulture = newCulture;
            try
            {
                DataBase.LoadItems();
                Customer.LoadCustomers();
            }
            catch (DirectoryNotFoundException)
            {
                DataBase.LogException(ex);
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }

        }

        private void btnManager_Click(object sender, RoutedEventArgs e)
        {
            MainMenu.Visibility = Visibility.Collapsed;
            managerGrid.Visibility = Visibility.Visible;
        }

        private void btnWorker_Click(object sender, RoutedEventArgs e)
        {
            MainMenu.Visibility = Visibility.Collapsed;
            workerGrid.Visibility = Visibility.Visible;
        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}