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
using WpfClasses;

namespace ITCompanysCRM.PageFolder.AdminFolder
{
    /// <summary>
    /// Логика взаимодействия для ListOfUsersPage.xaml
    /// </summary>
    public partial class ListOfUsersPage : Page
    {
        // TODO: Нужно сделать полную загрузку данных, фильтрацию, создание, удаление и редактирование пользователя

        public ListOfUsersPage()
        {
            InitializeComponent();
            LoadDG();
            LoadCB();
        }

        private void LoadDG()
        {
            using (ItcompanysCrmdbContext db = new())
            {
                UsersDG.ItemsSource = db.Staff.ToList();
            }
        }

        private void LoadCB()
        {
            using(ItcompanysCrmdbContext db = new())
            {
                PostCB.ItemsSource = db.Posts.ToList();
            }
        }

        private void ResetBtn_Click(object sender, RoutedEventArgs e)
        {
            SearchTB.Text = string.Empty;
            PostCB.SelectedIndex = -1;
        }

        private void ExcelBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ExcelClass.ToExcelFile(UsersDG, "Список пользователей");
            }
            catch (Exception)
            {
                MBClass.ErrorMB("Произошла ошибка. Повторите попытку");
            }
        }
    }
}
