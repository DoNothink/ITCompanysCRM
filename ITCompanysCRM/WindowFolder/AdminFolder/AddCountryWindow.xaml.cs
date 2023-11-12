using ITCompanysCRM.Model;
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

namespace ITCompanysCRM.WindowFolder.AdminFolder
{
    /// <summary>
    /// Логика взаимодействия для AddCountryWindow.xaml
    /// </summary>
    public partial class AddCountryWindow : Window
    {
        public AddCountryWindow()
        {
            InitializeComponent();
        }

        private void AddCountryBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CountryTB.Text))
            {
                MBClass.ErrorMB("Введите название добавляемой страны");
                CountryTB.Focus();
                return;
            }
            using (ItcompanysCrmdbContext db = new())
            {
                var newCountry = new Country()
                {
                    NameCountry = CountryTB.Text,
                };
                db.Countries.Add(newCountry);
                if (MBClass.QuestionMB("Вы действительно хотите добавить новую страну?"))
                {
                    db.SaveChanges();
                    MBClass.InfoMB($"Страна {newCountry.NameCountry} успешно добавлена!");
                }
            }
        }
    }
}
