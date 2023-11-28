using ITCompanysCRM.ClassFolder;
using Microsoft.EntityFrameworkCore;
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

namespace ITCompanysCRM.PageFolder.ManagerFolder
{
    /// <summary>
    /// Логика взаимодействия для ListOfStaffPage.xaml
    /// </summary>
    public partial class ListOfStaffPage : Page
    {
        public ListOfStaffPage()
        {
            InitializeComponent();
            LoadDG();
            LoadCB();
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (ItcompanysCrmdbContext db = new())
            {
                UsersDG.ItemsSource = db.Staff
                    .Where(x => x.SecondNameStaff.StartsWith(SearchTB.Text) || x.FirstNameStaff.StartsWith(SearchTB.Text))
                    .Where(x => x.IdUser != GlobalClass.GlobalUser.IdUser)
                    .ToList();
                db.Users.Load();
                db.Posts.Load();
            }
        }

        private void PostCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PostCB.SelectedIndex != -1)
            {
                using (ItcompanysCrmdbContext db = new())
                {
                    UsersDG.ItemsSource = db.Staff
                        .Where(x => x.IdPost == int.Parse(PostCB.SelectedValue.ToString()))
                        .Where(x => x.IdUser != GlobalClass.GlobalUser.IdUser)
                        .ToList();
                    db.Users.Load();
                    db.Posts.Load();
                }
            }
        }

        private void ResetBtn_Click(object sender, RoutedEventArgs e)
        {
            SearchTB.Text = string.Empty;
            PostCB.SelectedIndex = -1;
            LoadDG();
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

        private void MoreInfoMi_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LoadDG()
        {
            using (ItcompanysCrmdbContext db = new())
            {
                UsersDG.ItemsSource = db.Staff
                    .Where(x => x.IdUser != GlobalClass.GlobalUser.IdUser)
                    .ToList();
                db.Posts.Load();
            }
        }

        private void LoadCB()
        {
            using (ItcompanysCrmdbContext db = new())
            {
                PostCB.ItemsSource = db.Posts.ToList();
            }
        }

    }
}
