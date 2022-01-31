using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
    /// Prescription.xaml の相互作用ロジック
    /// </summary>
    
    public partial class Prescription : Page {
        User user;
        infosys202125DataSet infosys202125DataSet;
        infosys202125DataSetTableAdapters.MedicineTableAdapter infosys202125DataSetMedicineTableAdapter;
        CollectionViewSource medicineViewSource;

        public Prescription(User name) {
            InitializeComponent();
            
            infosys202125DataSet = ((PrescriptionManagementSystem.infosys202125DataSet)(this.FindResource("infosys202125DataSet")));
            // テーブル Medicine にデータを読み込みます。必要に応じてこのコードを変更できます。
            infosys202125DataSetMedicineTableAdapter = new PrescriptionManagementSystem.infosys202125DataSetTableAdapters.MedicineTableAdapter();
            infosys202125DataSetMedicineTableAdapter.FillByUser(infosys202125DataSet.Medicine,name.Id,name.Password);
            medicineViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("medicineViewSource")));
            medicineViewSource.View.MoveCurrentToFirst();
            user = name;
        }

        private void Change_button_Click(object sender, RoutedEventArgs e) {
            infosys202125DataSetMedicineTableAdapter.Update(infosys202125DataSet);
        }

        private void Search_Click(object sender, RoutedEventArgs e) {
            new MedicineTableAdapter().Searching(infosys202125DataSet.Medicine, SearchName.Text,Searcid.Text);
            
        }

        private void pmda_Click(object sender, RoutedEventArgs e) {
            Process.Start("https://www.pmda.go.jp/PmdaSearch/iyakuSearch/");

        }

        private void Add_button_Click(object sender, RoutedEventArgs e) {
            var win = new AddData();
            win.ShowDialog();
            infosys202125DataSet = ((PrescriptionManagementSystem.infosys202125DataSet)(this.FindResource("infosys202125DataSet")));
            // テーブル Medicine にデータを読み込みます。必要に応じてこのコードを変更できます。
            infosys202125DataSetMedicineTableAdapter = new PrescriptionManagementSystem.infosys202125DataSetTableAdapters.MedicineTableAdapter();
            infosys202125DataSetMedicineTableAdapter.FillByUser(infosys202125DataSet.Medicine,user.Id,user.Password);
            medicineViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("medicineViewSource")));
            medicineViewSource.View.MoveCurrentToFirst();
        }

        private void Done_Click(object sender, RoutedEventArgs e) {
            if (this.NavigationService.CanGoBack) {
                this.NavigationService.GoBack();
            } else {
                MessageBox.Show("No entries in back navigation history.");
            }
        }

        
    }
}
