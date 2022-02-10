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
using PrescriptionManagementSystem.infosys202125DataSetTableAdapters;

namespace PrescriptionManagementSystem {
    /// <summary>
    /// Login.xaml の相互作用ロジック
    /// </summary>
    public partial class Login : Page {
        
        public Login() {
            InitializeComponent();
            

        }

        private void Login_Click(object sender, RoutedEventArgs e) {
            try {
                var user = new UserTableAdapter().GetDataByLogin(UserId.Text, Password.Password);
                var login = new User();
                login.Id = user.First().id;
                login.Password = user.First().password;
                var anotherpage = new Prescription(login);
                NavigationService.Navigate(anotherpage);
                UserId.Text = string.Empty;
                Password.Password = string.Empty;
            }
            catch (Exception ) {
                MessageBox.Show("登録されていません。" ,"エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                UserId.Text = string.Empty;
                Password.Password = string.Empty;
            }
        }

        private void Addwindow_Click(object sender, RoutedEventArgs e) {
            UserId.Text = string.Empty;
            Password.Password = string.Empty;
            var win = new SignUp();
            win.ShowDialog();
        }

        
    }
}
