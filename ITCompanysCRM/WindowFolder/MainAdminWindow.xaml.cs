using ITCompanysCRM.ClassFolder;
using ITCompanysCRM.PageFolder.AdminFolder;
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

namespace ITCompanysCRM.WindowFolder
{
    /// <summary>
    /// Логика взаимодействия для MainAdminWindow.xaml
    /// </summary>
    public partial class MainAdminWindow : Window
    {
        public MainAdminWindow()
        {
            InitializeComponent();
            FioLabel.Content = GetFio();
        }

        private string GetFio()
        {
            using (ItcompanysCrmdbContext db = new())
            {
                Staff? currentStaff = db.Staff.FirstOrDefault(x => x.IdUser == GlobalClass.GlobalUser.IdUser);
                if (currentStaff == null)
                {
                    MBClass.ErrorMB("Произошла ошибка. Повторите попытку");
                    new AuthorizationWindow().Show();
                    this.Close();
                }

                string fio = $"{currentStaff.SecondNameStaff} {currentStaff.FirstNameStaff}";
                return fio;
            }
        }

        private void ListOfUsersBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ListOfUsersPage());
        }

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            new SettingsUserWindow().ShowDialog();
        }
        
        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            bool resultMB = MBClass.QuestionMB("Вы действительно хотите выйти?");
            if (resultMB)
            {
                new AuthorizationWindow().Show();
                this.Close();
            }
        }

        private void AddUserBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LogBookBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
