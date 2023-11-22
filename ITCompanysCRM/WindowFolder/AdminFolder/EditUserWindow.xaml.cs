using ITCompanysCRM.ClassFolder;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для EditUserWindow.xaml
    /// </summary>
    public partial class EditUserWindow : Window
    {
        IEnumerable<TextBox> textBoxes;

        private static readonly Regex _numberRegex = new Regex("[^0-9.-]+");

        Staff editableStaff;

        public EditUserWindow(Staff _staff)
        {
            InitializeComponent();
            LoadCB();
            editableStaff = _staff;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            textBoxes = MainGrid.Children.OfType<TextBox>();

            LoginTB.Text = editableStaff.IdUserNavigation.LoginUser;
            PasswordTB.Text = editableStaff.IdUserNavigation.PasswordUser;


            SecondNameTB.Text = editableStaff.SecondNameStaff;
            FirstNameTB.Text = editableStaff.FirstNameStaff;
            MiddleNameTB.Text = editableStaff.MiddleNameStaff;

            DateOfBirthDP.Text = editableStaff.DateOfBirthStaff.ToShortDateString();
            PhoneNumberTB.Text = editableStaff.PhoneNumberStaff;
            EmailTB.Text = editableStaff.EmailStaff;

            SeriesPassTB.Text = editableStaff.SeriesPassport.ToString();
            NumberPassTB.Text = editableStaff.NumberPassport.ToString();
            DateOfIssuedDP.Text = editableStaff.DateOfIssuedPassport.ToShortDateString();

            OthersDataTB.Text = editableStaff.OthersData;
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

        private void EditUserBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckTextBoxes()) return;

            if (RoleCB.SelectedIndex == -1)
            {
                MBClass.ErrorMB("Выберите роль пользователя");
                RoleCB.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(DateOfBirthDP.Text))
            {
                MBClass.ErrorMB("Введите дату рождения");
                DateOfBirthDP.Focus();
                return;
            }
            if (AddressCB.SelectedIndex == -1)
            {
                MBClass.ErrorMB("Выберите адрес");
                AddressCB.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(DateOfIssuedDP.Text))
            {
                MBClass.ErrorMB("Введите дату выдачи паспорта");
                DateOfIssuedDP.Focus();
                return;
            }
            if (IssuedPassCB.SelectedIndex == -1)
            {
                MBClass.ErrorMB("Выберите кем выдан паспорт");
                IssuedPassCB.Focus();
                return;
            }

            try
            {
                using (ItcompanysCrmdbContext db = new())
                {
                    var editUser = db.Users.FirstOrDefault(x => x.IdUser == editableStaff.IdUser);
                    if(editUser != null) 
                    {
                        editUser.IdUser = editableStaff.IdUserNavigation.IdUser;
                        editUser.LoginUser = LoginTB.Text;
                        editUser.PasswordUser = PasswordTB.Text;
                        editUser.IdRole = int.Parse(RoleCB.SelectedValue.ToString());
                        User? checkUsers = db.Users.Where(x => x.LoginUser != editUser.LoginUser).FirstOrDefault(x => x.LoginUser == editUser.LoginUser);
                        if (checkUsers != null)
                        {
                            if (editUser.LoginUser == checkUsers.LoginUser)
                            {
                                MBClass.ErrorMB("Пользователь с таким логином уже существует");
                                return;
                            }
                        }
                        db.SaveChanges();
                    };

                    Address? getAddress = db.Addresses
                        .FirstOrDefault(x => x.IdAddress == int.Parse(AddressCB.SelectedValue.ToString()));
                    IssuedPassport? getIssuedPass = db.IssuedPassports
                        .FirstOrDefault(x => x.IdIssuedPassport == int.Parse(IssuedPassCB.SelectedValue.ToString()));

                    var editStaff = db.Staff.FirstOrDefault(x => x.IdStaff == editableStaff.IdStaff);
                    if (editStaff != null)
                    {
                        editStaff.IdStaff = editableStaff.IdStaff;
                        editStaff.SecondNameStaff = SecondNameTB.Text;
                        editStaff.FirstNameStaff = FirstNameTB.Text;
                        editStaff.MiddleNameStaff = MiddleNameTB.Text;
                        editStaff.IdPost = int.Parse(PostCB.SelectedValue.ToString());
                        editStaff.DateOfBirthStaff = DateOfBirthDP.SelectedDate.Value;
                        editStaff.IdAddress = getAddress.IdAddress;
                        editStaff.PhoneNumberStaff = PhoneNumberTB.Text;
                        editStaff.EmailStaff = EmailTB.Text;
                        editStaff.OthersData = OthersDataTB.Text;
                        editStaff.SeriesPassport = int.Parse(SeriesPassTB.Text);
                        editStaff.IdUser = editUser.IdUser;
                        editStaff.NumberPassport = int.Parse(NumberPassTB.Text);
                        editStaff.DateOfIssuedPassport = DateOfIssuedDP.SelectedDate.Value;
                        editStaff.IdIssuedPassport = getIssuedPass.IdIssuedPassport;
                        db.SaveChanges();

                        MBClass.InfoMB("Пользователь успешно изменен");
                        LogClass.LogToDataBase($"Пользователь Id:{editUser.IdUser} Login: {editUser.LoginUser} был изменен");
                    }
                }
            }
            catch (Exception ex)
            {

                MBClass.ErrorMB("Произошла ошибка. Повторите попытку");
                return;
            }
        }

        private static bool IsTextAllowed(string text)
        {
            return !_numberRegex.IsMatch(text);
        }



        private void AddIssuedPassBtn_Click(object sender, RoutedEventArgs e)
        {
            new AddIssuedPassWindow().ShowDialog();
            using (ItcompanysCrmdbContext db = new())
            {
                IssuedPassCB.ItemsSource = db.IssuedPassViews.ToList();
            }
        }

        private void NumberPassTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void SeriesPassTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        private void AddAddressBtn_Click(object sender, RoutedEventArgs e)
        {
            new AddAddressWindow().ShowDialog();
            using (ItcompanysCrmdbContext db = new())
            {
                AddressCB.ItemsSource = db.AddressViews.ToList();
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
    }
}
