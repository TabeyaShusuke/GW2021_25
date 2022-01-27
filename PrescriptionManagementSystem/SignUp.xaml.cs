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
using PrescriptionManagementSystem.infosys202125DataSetTableAdapters;

namespace PrescriptionManagementSystem {
    /// <summary>
    /// SignUp.xaml の相互作用ロジック
    /// </summary>
    public partial class SignUp : Window {

        public SignUp() {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            PrescriptionManagementSystem.infosys202125DataSet infosys202125DataSet = ((PrescriptionManagementSystem.infosys202125DataSet)(this.FindResource("infosys202125DataSet")));
            // テーブル User にデータを読み込みます。必要に応じてこのコードを変更できます。
            PrescriptionManagementSystem.infosys202125DataSetTableAdapters.UserTableAdapter infosys202125DataSetUserTableAdapter = new PrescriptionManagementSystem.infosys202125DataSetTableAdapters.UserTableAdapter();
            infosys202125DataSetUserTableAdapter.Fill(infosys202125DataSet.User);
            System.Windows.Data.CollectionViewSource userViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("userViewSource")));
            userViewSource.View.MoveCurrentToFirst();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void Signup_Click(object sender, RoutedEventArgs e) {
            try {
                idTextBox.Text = string.Empty;
                passwordTextBox.Text = string.Empty;
                new UserTableAdapter().InsertUser(idTextBox.Text, passwordTextBox.Text);
                MessageBox.Show("登録完了しました。");
                this.Close();
            }
            catch (Exception) {
                MessageBox.Show("既に登録済みです。");
            }
            
        }

        
    }
}
