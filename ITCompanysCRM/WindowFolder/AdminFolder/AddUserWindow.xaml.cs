using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ITCompanysCRM.WindowFolder.AdminFolder
{
    /// <summary>
    /// Логика взаимодействия для AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        IEnumerable<TextBox> textBoxes;

        public AddUserWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            textBoxes = MainGrid.Children.OfType<TextBox>();            
        }

        /// <summary>
        /// Метод для проверки TextBox на заполненность
        /// </summary>
        /// <returns>True при успешной проверке.
        /// False при безуспешной.</returns>
        private bool CheckTextBoxes()
        {
            foreach (var x in textBoxes)
            {
                if (x.Name == "MiddleNameTB" || x.Name == "EmailTB" || x.Name == "OthersDataTB")
                    continue;

                if (string.IsNullOrWhiteSpace(x.Text))
                {
                    MBClass.ErrorMB($"Заполните обязательное поле");
                    x.Focus();
                    return false;
                    
                }
            }
            return true;
        }

        // TODO: Добавление пользователя проверить
        private void AddUserBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckTextBoxes()) return;

            if(RoleCB.SelectedIndex == -1)
            {
                MBClass.ErrorMB("Выберите роль пользователя");
                RoleCB.Focus();
                return;
            }
            if(string.IsNullOrWhiteSpace(DateOfBirthDP.Text))
            {
                MBClass.ErrorMB("Введите дату рождения");
                DateOfBirthDP.Focus();
                return;
            }
            if(AddressCB.SelectedIndex == -1)
            {
                MBClass.ErrorMB("Выберите адрес");
                AddressCB.Focus();
                return;
            }
            if(string.IsNullOrWhiteSpace(DateOfIssuedDP.Text))
            {
                MBClass.ErrorMB("Введите дату выдачи паспорта");
                DateOfIssuedDP.Focus();
                return;
            }
            if(IssuedPassCB.SelectedIndex == -1)
            {
                MBClass.ErrorMB("Выберите кем выдан паспорт");
                IssuedPassCB.Focus();
                return;
            }

            var newUser = new User()
            {
                LoginUser = LoginTB.Text,
                PasswordUser = PasswordTB.Text,
                IdRole = int.Parse(RoleCB.SelectedValue.ToString()),
            };
            var newStaff = new Staff()
            {
                SecondNameStaff = SecondNameTB.Text,
                FirstNameStaff = FirstNameTB.Text,
                MiddleNameStaff = MiddleNameTB.Text,
                IdPost = int.Parse(PostCB.SelectedValue.ToString()),
                DateOfBirthStaff = DateOfBirthDP.SelectedDate.Value,
                IdAddress = int.Parse(AddressCB.SelectedValue.ToString()),
                PhoneNumberStaff = PhoneNumberTB.Text,
                EmailStaff = EmailTB.Text,
                OthersData = OthersDataTB.Text,
                SeriesPassport = int.Parse(SeriesPassTB.Text),
                IdUser = newUser.IdUser,
                NumberPassport = int.Parse(NumberPassTB.Text),
                DateOfIssuedPassport = DateOfIssuedDP.SelectedDate.Value,
                IdIssuedPassport = int.Parse(IssuedPassCB.SelectedValue.ToString()),
            };
            try
            {
                using (ItcompanysCrmdbContext db = new())
                {
                    db.Add(newUser);
                    db.Add(newStaff);
                    db.SaveChanges();
                    MBClass.InfoMB("Пользователь успешно создан");
                }
            }
            catch (Exception)
            {
                MBClass.ErrorMB("Произошла ошибка. Повторите попытку");
                return;
            }
        }

        // TODO: Сделать окно добавления должности
        private void AddPostBtn_Click(object sender, RoutedEventArgs e)
        {

        }
        // TODO: Сделать окно добавления адреса
        private void AddAddressBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        // TODO: Сделать окно добавления Кем Выдан Паспорт
        private void AddIssuedPassBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

// TODO: Реализовать ввод только цифр для нужных полей. Ввод номера телефона.
