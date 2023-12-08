using ITCompanysCRM.ClassFolder;
using ITCompanysCRM.PageFolder.ManagerFolder;
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
    /// Interaction logic for MainManagerWindow.xaml
    /// </summary>
    public partial class MainManagerWindow : Window
    {
        public MainManagerWindow()
        {
            InitializeComponent();
            FioLabel.Content = GetFio();
        }

        private void ListOfStaffBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ListOfStaffPage());
        }

        // TODO: Список проектов ( C R U D )
        private void ListOfProjectsBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ListOfProjectsPage());
        }

        // TODO: Список команд ( C R U D )
        private void ListOfTeamsBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListOfClientsBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ListOfClientPage());
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
    }
}
