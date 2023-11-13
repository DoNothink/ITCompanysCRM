using ITCompanysCRM.ClassFolder;
using ITCompanysCRM.WindowFolder.AdminFolder;
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
            using (ItcompanysCrmdbContext db = new())
            {
                PostCB.ItemsSource = db.Posts.ToList();
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

        private void DeleteUserMi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Staff? _selectedStaff = UsersDG.SelectedItem as Staff;
                if (_selectedStaff != null)
                {
                    bool resultMB = MBClass.QuestionMB($"Вы действительно хотите " +
                        $"удалить пользователя {_selectedStaff.SecondNameStaff} {_selectedStaff.FirstNameStaff}?");
                    if (resultMB)
                    {
                        using (ItcompanysCrmdbContext db = new())
                        {
                            db.Staff.Remove(_selectedStaff);
                            db.SaveChanges();
                            MBClass.InfoMB("Пользователь удален");
                            LoadDG();
                            LogClass.LogToDataBase($"Пользователь Id:{_selectedStaff.IdUser} удален");
                        }
                    }
                }
            }
            catch (Exception)
            {
                MBClass.ErrorMB("Произошла ошибка. Повторите попытку");
            }
        }

        // TODO: EditUser
        private void EditUserMi_Click(object sender, RoutedEventArgs e)
        {
            Staff? _selectedStaff = UsersDG.SelectedItem as Staff;
            //if (_selectedStaff != null)

        }

        private void AddUserBtn_Click(object sender, RoutedEventArgs e)
        {
            new AddUserWindow().ShowDialog();
            LoadDG();
        }


        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (ItcompanysCrmdbContext db = new())
            {
                UsersDG.ItemsSource = db.Staff
                    .Where(x => x.SecondNameStaff.StartsWith(SearchTB.Text) || x.FirstNameStaff.StartsWith(SearchTB.Text))
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
                        .ToList();
                    db.Users.Load();
                    db.Posts.Load();
                }
            }
        }
    }
}
// TODO: ИЗМЕНИТЬ ТЕМУ ВСЕГО ПРОЕКТА НА ЗЕЛЕНЫЙ/ЧЕРНЫЙ ( X5 Tech )
