﻿using System;
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

        }

        private void Add_button_Click(object sender, RoutedEventArgs e) {
            var win = new AddData();
            win.ShowDialog();
            infosys202125DataSet = ((PrescriptionManagementSystem.infosys202125DataSet)(this.FindResource("infosys202125DataSet")));
            // テーブル Medicine にデータを読み込みます。必要に応じてこのコードを変更できます。
            infosys202125DataSetMedicineTableAdapter = new PrescriptionManagementSystem.infosys202125DataSetTableAdapters.MedicineTableAdapter();
            infosys202125DataSetMedicineTableAdapter.Fill(infosys202125DataSet.Medicine);
            medicineViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("medicineViewSource")));
            medicineViewSource.View.MoveCurrentToFirst();

        }

        private void Search_button_Click(object sender, RoutedEventArgs e) {
            
        }

        private void Logout_Click(object sender, RoutedEventArgs e) {
            
        }
    }
}