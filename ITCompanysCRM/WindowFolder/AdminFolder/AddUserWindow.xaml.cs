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


        private void AddUserBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (var x in  textBoxes)
            {
                
                if(string.IsNullOrWhiteSpace(x.Text))
                {
                    MBClass.ErrorMB($"Заполните обязательное поле");
                    x.Focus();
                    break;
                }
            }
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
            if(String.IsNullOrWhiteSpace(DateOfIssuedDP.Text))
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

        }

        private void AddPostBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddAddressBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddIssuedPassBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
