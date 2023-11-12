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
    /// Логика взаимодействия для AddAddressWindow.xaml
    /// </summary>
    public partial class AddAddressWindow : Window
    {
        private static readonly Regex _numberRegex = new Regex("[^0-9.-]+");

        public AddAddressWindow()
        {
            InitializeComponent();
            LoadCB();
        }

        private void LoadCB()
        {
            using (ItcompanysCrmdbContext db = new ())
            {
                CountryCB.ItemsSource = db.Countries.ToList();
                CityCB.ItemsSource = db.Cities.ToList();
                StreetCB.ItemsSource = db.Streets.ToList();
            }
        }

        private static bool IsTextAllowed(string text)
        {
            return !_numberRegex.IsMatch(text);
        }

        private void AddCountryBtn_Click(object sender, RoutedEventArgs e)
        {
            new AddCountryWindow().ShowDialog();
            using (ItcompanysCrmdbContext db = new())
            {
                CountryCB.ItemsSource = db.Countries.ToList();
            }
        }

        private void AddCityBtn_Click(object sender, RoutedEventArgs e)
        {
            new AddCityWindow().ShowDialog();
            using(ItcompanysCrmdbContext db = new())
            {
                CityCB.ItemsSource = db.Cities.ToList();
            }
        }

        private void AddStreetBtn_Click(object sender, RoutedEventArgs e)
        {
            new AddStreetWindow().ShowDialog();
            using (ItcompanysCrmdbContext db = new())
            {
                StreetCB.ItemsSource = db.Streets.ToList();
            }
        }

        private void AddAddressBtn_Click(object sender, RoutedEventArgs e)
        {
            if(CountryCB.SelectedIndex==-1)
            {
                MBClass.ErrorMB("Выберите страну");
                CountryCB.Focus();
                return;
            }
            if(CityCB.SelectedIndex==-1)
            {
                MBClass.ErrorMB("Выберите город");
                CityCB.Focus();
                return;
            }
            if(StreetCB.SelectedIndex==-1)
            {
                MBClass.ErrorMB("Выберите улицу");
                StreetCB.Focus();
                return;
            }
            if(string.IsNullOrWhiteSpace(HomeAddressTB.Text))
            {
                MBClass.ErrorMB("Введите номер дома");
                HomeAddressTB.Focus();
                return;
            }
            if(string.IsNullOrWhiteSpace(FlatAddressTB.Text))
            {
                MBClass.ErrorMB("Введите номер квартиры");
                HomeAddressTB.Focus();
                return;
            }
            var newAddress = new Address()
            {
                IdCountry = int.Parse(CountryCB.SelectedValue.ToString()),
                IdCity = int.Parse(CityCB.SelectedValue.ToString()),
                IdStreet = int.Parse(StreetCB.SelectedValue.ToString()),
                HomeAddress = HomeAddressTB.Text,
                FlatAddress = int.Parse(FlatAddressTB.Text),
            };
            if(newAddress!=null)
            {
                using (ItcompanysCrmdbContext db = new())
                {
                    db.Add(newAddress);
                    if (MBClass.QuestionMB("Вы действительно хотите добавить новый адрес?"))
                    {
                        db.SaveChanges();
                        MBClass.InfoMB("Новый адрес добавлен!");
                    }
                }
            }
        }

        private void FlatAddressTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
    }
}
