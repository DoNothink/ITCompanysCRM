﻿using ITCompanysCRM.ClassFolder;
using MaterialDesignThemes.Wpf;
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
using System.Windows.Shapes;

namespace ITCompanysCRM.WindowFolder
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        ItcompanysCrmdbContext db = new();
        List<User> Users { get; set; }

        public AuthorizationWindow()
        {
            InitializeComponent();
            GetRememberMeState();

            db.Users.Load();
            Users = db.Users.Local.ToList();

        }

        private void GetRememberMeState()
        {
            LoginTB.Text = Properties.Settings.Default.LoginUser;
            RememberMeCB.IsChecked = Properties.Settings.Default.RememberMeState;
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
                var user = this.Users.FirstOrDefault(x=>x.LoginUser == LoginTB.Text); 
                if (user == null)
                {
                    MBClass.ErrorMB("Введен неверный логин или пароль");
                    return;
                }
                if (user.PasswordUser != PasswordPB.Password)
                {
                    MBClass.ErrorMB("Введен неверный логин или пароль");
                    return;
                }
                if (RememberMeCB.IsChecked == true)
                {
                    Properties.Settings.Default.LoginUser = LoginTB.Text;
                    Properties.Settings.Default.RememberMeState = true;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    Properties.Settings.Default.LoginUser = null;
                    Properties.Settings.Default.RememberMeState = false;
                    Properties.Settings.Default.Save();
                }
                MBClass.InfoMB("Успешный вход");
                GlobalClass.GlobalUser = user;
                switch (user.IdRole)
                {
                    case 1:
                        new MainAdminWindow().Show();
                        this.Close();
                        break;
                    case 2:
                        new MainManagerWindow().Show();
                        this.Close();
                        break;

                }
            }
        }

        private void ForgotPassBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MBClass.InfoMB("Для восстановления пароля обратитесь к администратору системы");
        }
    }
}
