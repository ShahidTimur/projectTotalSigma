using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
namespace WpfApp1
{

    public partial class RemoveCustomer : UserControl
    {
        public RemoveCustomer()
        {
            InitializeComponent();
            btnEnter.ButtonContent = Resex.btnEnter;
            btnreturn.ButtonContent = Resex.btnReturn;

            btnEnter.ButtonClickContent += btnInput_Click;
            btnreturn.ButtonClickContent += btnInput_Click;
        }

        private void btnInput_Click(object sender, RoutedEventArgs e)
        {
            if (sender == btnEnter)
            {
                if (idBox.txtInput.Text != null)
                {
                    try
                    {
                        MessageBoxResult result = MessageBox.Show($"Are you sure you want to remove {idBox.txtInput.Text} from the club?", "Remove Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes)
                        {
                            Customer.RemoveCustomerFromClub(idBox.txtInput.Text);
                            Customer.DeleteFile(idBox.txtInput.Text);
                            MessageBox.Show($"The customer {idBox.txtInput.Text} has been successfully removed from the club!", "Customer Removed", MessageBoxButton.OK, MessageBoxImage.Information);
                            ReturnToWorkerMenu();
                        }
                    }
                    catch (DirectoryNotFoundException ex)
                    {
                        ErrorMessage(ex);
                        Customer.AddCustomerToClub(idBox.txtInput.Text);
                        isbnBox.txtInput.Focus();
                    }
                    catch (IllegalIdException ex)
                    {
                        ErrorMessage(ex);
                        idBox.txtInput.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Please enter the customer's id you would like to remove!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    idBox.txtInput.Focus();
                }
            }
            else
            {
                ReturnToWorkerMenu();
            }
        }
        private void ErrorMessage(Exception ex)
        {
            DataBase.LogException(ex);
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ReturnToWorkerMenu()
        {
            Window mainWindow = Window.GetWindow(this);
            Grid RemoveCustomerGrid = (Grid)mainWindow.FindName("RemoveCustomerGrid");
            RemoveCustomerGrid.Visibility = Visibility.Collapsed;

            Grid workerGrid = (Grid)mainWindow.FindName("workerGrid");
            workerGrid.Visibility = Visibility.Visible;

            idBox.txtInput.Text = string.Empty;
        }
    }
}
