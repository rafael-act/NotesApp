using NotesApp.ViewModel;
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

namespace NotesApp.View
{
    /// <summary>
    /// Interaction logic for LoginWindow1.xaml
    /// </summary>
    public partial class LoginWindow1 : Window
    {
        public LoginWindow1()
        {
            InitializeComponent();

            LoginVM vm = new LoginVM();
            containerGrid.DataContext = vm;
            vm.HasLoggedIn += Vm_HasLoggedIn;
        }

        private void Vm_HasLoggedIn(object sender, EventArgs e)
        {
            this.Close();
        }

        private void HaveAccountButton_Click(object sender, RoutedEventArgs e)
        {
            registerStackPanel.Visibility = Visibility.Collapsed;
            loginStackPanel.Visibility = Visibility.Visible;
        }

        private void NoAccountButton_Click(object sender, RoutedEventArgs e)
        {
            registerStackPanel.Visibility = Visibility.Visible;
            loginStackPanel.Visibility = Visibility.Collapsed;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
