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
using System.Windows.Threading;
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
        DispatcherTimer timer,clock;
        

        public Prescription(User name) {
            InitializeComponent();

            clock = new DispatcherTimer(DispatcherPriority.Background);
            clock.Tick += Clock_Tick;
            clock.Interval = new TimeSpan(0,0,0);
            clock.Start();

            timer = new DispatcherTimer(DispatcherPriority.Background);
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0,0,0);
            timer.Start();
            

            infosys202125DataSet = ((PrescriptionManagementSystem.infosys202125DataSet)(this.FindResource("infosys202125DataSet")));
            // テーブル Medicine にデータを読み込みます。必要に応じてこのコードを変更できます。
            infosys202125DataSetMedicineTableAdapter = new PrescriptionManagementSystem.infosys202125DataSetTableAdapters.MedicineTableAdapter();
            medicineDataGrid.ItemsSource = infosys202125DataSetMedicineTableAdapter.GetDataByUser(name.Id, name.Password).Select(x => new Medicine { Id = x.Id,Name = x.Name,Type = x.Type,Interval = x.Interval,
                                                                                                        Dosing = x.dosing, Precaution = x.Precaution, Userid = x.userid}).ToList();
            medicineViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("medicineViewSource")));
            medicineViewSource.View.MoveCurrentToFirst();
            user = name;
            Searcid.Content = user.Id;

            Reset.IsEnabled = false;
        }

        private void Clock_Tick(object sender, EventArgs e) {
            ti.Content = DateTime.Now.ToString("HH:mm:ss");
        }

        DateTime alarmtime;
        int ahour, aminute;
        int Setting = 0;
        string alarm;
        private void Timer_Tick(object sender, EventArgs e) {
            if ((DateTime.Now >= alarmtime) && (Setting == 1)) {
                MessageBox.Show("時間です。", "アラーム", MessageBoxButton.OK);
                timer.Stop();
                Alarm.IsEnabled = false;
            }
        }

        //編集
        private void Change_button_Click(object sender, RoutedEventArgs e) {
            Medicine medicine = (Medicine)medicineDataGrid.SelectedItem;
            if (medicine == null) {
                MessageBox.Show("変更出来ていません", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            } else {
                new MedicineTableAdapter().UpdateQuery2(medicine.Name, medicine.Type, medicine.Interval.ToString(),
                                                        medicine.Dosing, medicine.Precaution, medicine.Id, medicine.Id);
            }
        }

        //検索
        private void Search_Click(object sender, RoutedEventArgs e) {
            medicineDataGrid.ItemsSource = new MedicineTableAdapter().GetDataBy1(SearchName.Text, Searcid.Content.ToString()).Select(x => new Medicine {
                Id = x.Id, Name = x.Name, Type = x.Type, Interval = x.Interval,
                Dosing = x.dosing, Precaution = x.Precaution, Userid = x.userid
            }).ToList();
            SearchName.Text = string.Empty;
        }

        //追加
        private void Add_button_Click(object sender, RoutedEventArgs e) {
            var win = new AddData(user);
            win.ShowDialog();
            medicineDataGrid.ItemsSource = infosys202125DataSetMedicineTableAdapter.GetDataByUser(user.Id, user.Password).Select(x => new Medicine {
                Id = x.Id, Name = x.Name, Type = x.Type, Interval = x.Interval,
                Dosing = x.dosing, Precaution = x.Precaution, Userid = x.userid
            }).ToList();
        }

        //終了
        private void Done_Click(object sender, RoutedEventArgs e) {
            if (this.NavigationService.CanGoBack) {
                this.NavigationService.GoBack();
            } 
        }


        //アラームリセット
        private void Reset_Click(object sender, RoutedEventArgs e) {
            Hour.Value = 0;
            Min.Value = 0;
            Setting = 0;
            timer.Start();
            Reset.IsEnabled = false;
            Alarm.IsEnabled = true;
            MessageBox.Show("リセットしました。","アラーム" ,MessageBoxButton.OK);
        }

        //アラームセット
        private void Alarm_Click(object sender, RoutedEventArgs e) {
            try {
                ahour = (int)Hour.Value;
                aminute = (int)Min.Value;
                alarm = ahour + ":" + aminute;
                alarmtime = DateTime.Parse(alarm);
                Setting = 1;
                MessageBox.Show(alarmtime + "に時間を設定しました。", "アラーム", MessageBoxButton.OK);
                Reset.IsEnabled = true;
            }
            catch (Exception) {
                MessageBox.Show("24:00以降は設定出来ません。","エラー", MessageBoxButton.OK, MessageBoxImage.Warning);
                Hour.Value = 0;
                Min.Value = 0;
            }
        }
    }
}
