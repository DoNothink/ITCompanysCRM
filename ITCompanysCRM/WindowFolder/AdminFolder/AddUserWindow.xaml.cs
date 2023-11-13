using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private static readonly Regex _numberRegex = new Regex("[^0-9.-]+");

        public AddUserWindow()
        {
            InitializeComponent();
            LoadCB();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            textBoxes = MainGrid.Children.OfType<TextBox>();            
        }

        private void LoadCB()
        {
            using (ItcompanysCrmdbContext db = new())
            {
                RoleCB.ItemsSource = db.Roles.ToList();
                PostCB.ItemsSource = db.Posts.ToList();
                AddressCB.ItemsSource = db.AddressViews.ToList();
                IssuedPassCB.ItemsSource = db.IssuedPassViews.ToList();
            }
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

        private static bool IsTextAllowed(string text)
        {
            return !_numberRegex.IsMatch(text);
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

            try
            {
                using (ItcompanysCrmdbContext db = new())
                {
                    var newUser = new User()
                    {
                        LoginUser = LoginTB.Text,
                        PasswordUser = PasswordTB.Text,
                        IdRole = int.Parse(RoleCB.SelectedValue.ToString()),
                    };
                    db.Add(newUser);
                    db.SaveChanges();

                    Address? getAddress = db.Addresses
                        .FirstOrDefault(x => x.IdAddress == int.Parse(AddressCB.SelectedValue.ToString()));
                    IssuedPassport? getIssuedPass = db.IssuedPassports
                        .FirstOrDefault(x => x.IdIssuedPassport == int.Parse(IssuedPassCB.SelectedValue.ToString()));

                    var newStaff = new Staff()
                    {
                        SecondNameStaff = SecondNameTB.Text,
                        FirstNameStaff = FirstNameTB.Text,
                        MiddleNameStaff = MiddleNameTB.Text,
                        IdPost = int.Parse(PostCB.SelectedValue.ToString()),
                        DateOfBirthStaff = DateOfBirthDP.SelectedDate.Value,
                        IdAddress = getAddress.IdAddress,
                        PhoneNumberStaff = PhoneNumberTB.Text,
                        EmailStaff = EmailTB.Text,
                        OthersData = OthersDataTB.Text,
                        SeriesPassport = int.Parse(SeriesPassTB.Text),
                        IdUser = newUser.IdUser,
                        NumberPassport = int.Parse(NumberPassTB.Text),
                        DateOfIssuedPassport = DateOfIssuedDP.SelectedDate.Value,
                        IdIssuedPassport = getIssuedPass.IdIssuedPassport,
                    };
                        db.Add(newStaff);
                        db.SaveChanges();
                        MBClass.InfoMB("Пользователь успешно создан");
                }
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
                MBClass.ErrorMB("Произошла ошибка. Повторите попытку");
                return;
            }
        }

        private void AddPostBtn_Click(object sender, RoutedEventArgs e)
        {
            new AddPostWindow().ShowDialog();
            using (ItcompanysCrmdbContext db = new())
            {
                PostCB.ItemsSource = db.Posts.ToList();
            }
        }

        private void AddAddressBtn_Click(object sender, RoutedEventArgs e)
        {
            new AddAddressWindow().ShowDialog();
            using (ItcompanysCrmdbContext db = new())
            {
                AddressCB.ItemsSource = db.AddressViews.ToList();
            }
        }

        private void AddIssuedPassBtn_Click(object sender, RoutedEventArgs e)
        {
            new AddIssuedPassWindow().ShowDialog();
            using (ItcompanysCrmdbContext db = new())
            {
                IssuedPassCB.ItemsSource = db.IssuedPassViews.ToList();
            }
        }

        private void SeriesPassTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void NumberPassTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void PhoneNumberTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            string newVal = Regex.Replace(PhoneNumberTB.Text, @"[^0-9]", "");
            if (newVal.Length <= 1)
            {
                PhoneNumberTB.Text = Regex.Replace(newVal, @"(\d{1})", "+$1");
                PhoneNumberTB.Select(3, 0);
            }
            else if (newVal.Length <= 4)
            {
                PhoneNumberTB.Text = Regex.Replace(newVal, @"(\d{1})(\d{0,3})", "+$1($2)"); PhoneNumberTB.Select(7, 0);
            }
            else if (newVal.Length <= 7)
            {
                PhoneNumberTB.Text = Regex.Replace(newVal, @"(\d{1})(\d{3})(\d{0,3})", "+$1($2)$3");
                PhoneNumberTB.Select(11, 0);
            }
            else if (newVal.Length <= 9)
            {
                PhoneNumberTB.Text = Regex.Replace(newVal, @"(\d{1})(\d{3})(\d{0,3})(\d{0,2})", "+$1($2)$3-$4"); PhoneNumberTB.Select(16, 0);
            }
            else if (newVal.Length > 9)
            {
                PhoneNumberTB.Text = Regex.Replace(newVal, @"(\d{1})(\d{3})(\d{0,3})(\d{0,2})(\d{0,2})", "+$1($2)$3-$4-$5");
                PhoneNumberTB.Select(18, 0);
            }
        }
    }
}