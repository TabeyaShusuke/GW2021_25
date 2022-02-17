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

        infosys202125DataSet infosys202125DataSet;
        infosys202125DataSetTableAdapters.UserTableAdapter infosys202125DataSetUserTableAdapter;
        CollectionViewSource userViewSource;

        public SignUp() {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            infosys202125DataSet = ((PrescriptionManagementSystem.infosys202125DataSet)(this.FindResource("infosys202125DataSet")));
            // テーブル User にデータを読み込みます。必要に応じてこのコードを変更できます。
            infosys202125DataSetUserTableAdapter = new PrescriptionManagementSystem.infosys202125DataSetTableAdapters.UserTableAdapter();
            infosys202125DataSetUserTableAdapter.Fill(infosys202125DataSet.User);
            userViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("userViewSource")));
            userViewSource.View.MoveCurrentToFirst();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void Signup_Click(object sender, RoutedEventArgs e) {
            try {
                if (string.IsNullOrEmpty(Id.Text) || string.IsNullOrEmpty(Password.Password) ||
                string.IsNullOrEmpty(Confirmation.Password) || string.IsNullOrWhiteSpace(Confirmation.Password) ||
                string.IsNullOrWhiteSpace(Id.Text) || string.IsNullOrWhiteSpace(Password.Password)) {
                    MessageBox.Show("入力に誤りがあります。", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                    Empty();
                } else if (Password.Password.Length < 4 && Password.Password == Confirmation.Password) {
                    MessageBox.Show("パスワードは四文字以上入力してください。", "注意", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Empty();
                } else if (Password.Password == Confirmation.Password && Id.Text.Length <= 10 && Password.Password.Length <= 10) {
                    new UserTableAdapter().InsertUser(Id.Text, Password.Password);
                    infosys202125DataSetUserTableAdapter.Fill(infosys202125DataSet.User);
                    MessageBox.Show("登録完了しました。", "", MessageBoxButton.OK);
                    this.Close();
                } else {
                    MessageBox.Show("入力に誤りがあります。", "エラー", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Empty();
                }
            }
            catch (Exception) {
                MessageBox.Show("既に登録済みです。", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Empty();
            }
            
        }

        private void Empty() {
            Id.Text = string.Empty;
            Password.Password = string.Empty;
            Confirmation.Password = string.Empty;
        }
        
    }
}
