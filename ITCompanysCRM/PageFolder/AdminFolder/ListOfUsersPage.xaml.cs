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

namespace ITCompanysCRM.PageFolder.AdminFolder
{
    /// <summary>
    /// Логика взаимодействия для ListOfUsersPage.xaml
    /// </summary>
    public partial class ListOfUsersPage : Page
    {
        public ListOfUsersPage()
        {
            InitializeComponent();
            LoadDG();
        }

        private void LoadDG()
        {
            using (ItcompanysCrmdbContext db = new())
            {
                UsersDG.ItemsSource = db.Staff.ToList();
            }
        }

        private void ResetBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExcelBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
