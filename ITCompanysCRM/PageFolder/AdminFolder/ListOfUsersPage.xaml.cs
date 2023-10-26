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
using WpfClasses;

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
            LoadCB();
        }

        private void LoadDG()
        {
            using (ItcompanysCrmdbContext db = new())
            {
                UsersDG.ItemsSource = db.Staff.ToList();
                db.Users.Load();
                db.Posts.Load();
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

        private void DeleteUserMi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Staff? _selectedStaff = UsersDG.SelectedItem as Staff;
                if (_selectedStaff != null)
                {
                    bool resultMB = MBClass.QuestionMB($"Вы действительно хотите " +
                        $"удалить пользователя {_selectedStaff.SecondNameStaff} {_selectedStaff.FirstNameStaff}?");
                    if(resultMB)
                    {
                        using (ItcompanysCrmdbContext db = new())
                        {
                            db.Staff.Remove(_selectedStaff);
                            db.SaveChanges();
                            MBClass.InfoMB("Пользователь удален");
                            LoadDG();
                        }
                    }
                }
            }
            catch (Exception)
            {
                MBClass.ErrorMB("Произошла ошибка. Повторите попытку");
            }
        }

        private void EditUserMi_Click(object sender, RoutedEventArgs e)
        {
            Staff? _selectedStaff = UsersDG.SelectedItem as Staff;
            if( _selectedStaff != null ) 

        }

        private void AddUserBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
