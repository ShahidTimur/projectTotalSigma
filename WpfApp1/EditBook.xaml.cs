using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using BookJurnalLibrary;

namespace WpfApp1
{
    public partial class EditBook : UserControl
    {
        public static string isbn { get; set; }

        public EditBook()
        {
            InitializeComponent();
            btnEnter.ButtonContent = Resex.btnEnter;
            btnReturn.ButtonContent = Resex.btnReturn;

            btnEnter.ButtonClickEvent += btnInput_Click;
            btnReturn.ButtonClickEvent += btnInput_Click;
        }

        private void btnInput_Click(object sender, RoutedEventArgs e)
        {
            if (sender == btnEnter)
            {
                try
                {
                    DataBase.IsIsbnValid(isbnBox.txtInput.Text);
                    isbn = isbnBox.txtInput.Text;
                    ProceedToNextMenu():
                }
                catch (illegalIsbnException ex)
                {
                    DataBase.LogException(ex);
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    isbnBox.txtInput.Focus();
                }
            }
            else
            {
                ReturnToManagerMenu();
            }
        }
        private void ReturnToManagerMenu()
        {
            Window mainWindow = Window.GetWindow(this);
            Grid editBookGrid = (Grid)mainWindow.FindName("editBookGrid");
            editBookGrid.Visibility = Visibility.Collapsed;

            Grid managerGrid = (Grid)mainWindow.FindName("managerGrid");
            managerGrid.Visibility = Visibility.Visible;

            isbnBox.txtInput.Text = string.Empty;
        }

        private void ProceedToNextMenu()
        {
            Window mainWindow = Window.GetWindow(this);
            Grid editBookGrid = (Grid)mainWindow.FindName("editBookGrid");
            editBookGrid.Visibility = Visibility.Collapsed;

            Grid editGrid2 = (Grid)mainWindow.FindName("editBookGrid2");
            editBookGrid2.Visibility = Visibility.Visible;

            EditBook2 editBookGrid2Control = (EditBook2)editBookGrid2.Children[0];
            editBookGrid2Control.ClearComboBox();
            editBookGrid2Control.FindBook();
            editBookGrid2Control.PopulateComboBox();
        }
    }
}