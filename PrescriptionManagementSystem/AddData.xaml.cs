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
    /// AddData.xaml の相互作用ロジック
    /// </summary>
    public partial class AddData : Window {
        User user;
        infosys202125DataSet infosys202125DataSet;
        infosys202125DataSetTableAdapters.MedicineTableAdapter infosys202125DataSetMedicineTableAdapter;
        CollectionViewSource medicineViewSource;

        public AddData(User name) {
            InitializeComponent();
            infosys202125DataSet = ((PrescriptionManagementSystem.infosys202125DataSet)(this.FindResource("infosys202125DataSet")));
            // テーブル Medicine にデータを読み込みます。必要に応じてこのコードを変更できます。
            infosys202125DataSetMedicineTableAdapter = new PrescriptionManagementSystem.infosys202125DataSetTableAdapters.MedicineTableAdapter();
            infosys202125DataSetMedicineTableAdapter.Fill(infosys202125DataSet.Medicine);
            medicineViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("medicineViewSource")));
            medicineViewSource.View.MoveCurrentToFirst();
            user = name;
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {

        }

        private void Cancel_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e) {
            try {
                new MedicineTableAdapter().InsertData(Name.Text, Type.Text,
                                                  Interval.Text, dosing.Text, Precaution.Text,
                                                  user.Id);
                MessageBox.Show("追加しました。", "お薬登録", MessageBoxButton.OK);
                this.Close();
            }
            catch (Exception) {
                MessageBox.Show("入力に誤りがあります。", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            

        }

    }
}
