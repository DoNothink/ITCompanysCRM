using ITCompanysCRM.ClassFolder;
using MaterialDesignThemes.Wpf;
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
    enum RememberState
    {
        Remember,
        NotRemember
    }
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
            LoginTB.Text = Properties.Settings.Default.LoginUser;

        }

        private void LogInBtn_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(LoginTB.Text))
            {
                MBClass.ErrorMB("Введите логин");
                LoginTB.Focus();
                return;
            }
            if(string.IsNullOrWhiteSpace(PasswordPB.Password))
            {
                MBClass.ErrorMB("Введите пароль");
                PasswordPB.Focus();
                return;
            }
            using (ItcompanysCrmdbContext db = new())
            {
                var user = db.Users.FirstOrDefault(x => x.LoginUser == LoginTB.Text);
                if(user==null)
                {
                    MBClass.ErrorMB("Введен неверный логин или пароль");
                    return;
                }
                if(user.PasswordUser!=PasswordPB.Password)
                {
                    MBClass.ErrorMB("Введен неверный логин или пароль");
                    return;
                }
                if(RememberMeCB.IsChecked==true)
                {
                    Properties.Settings.Default.LoginUser = LoginTB.Text;
                    Properties.Settings.Default.Save();
                }
                MBClass.InfoMB("Успешный вход");
                GlobalClass.GlobalUser = user;
            }
        }

        private void ForgotPassBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MBClass.InfoMB("Work in progress");
        }
    }
}
