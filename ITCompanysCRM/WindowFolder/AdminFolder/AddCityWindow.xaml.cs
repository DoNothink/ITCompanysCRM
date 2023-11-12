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
    /// Логика взаимодействия для AddCityWindow.xaml
    /// </summary>
    public partial class AddCityWindow : Window
    {
        public AddCityWindow()
        {
            InitializeComponent();
        }

        private void AddCityBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CityTB.Text))
            {
                MBClass.ErrorMB("Введите название добавляемого города");
                CityTB.Focus();
                return;
            }
            using (ItcompanysCrmdbContext db = new())
            {
                var newCity = new City()
                {
                    NameCity = CityTB.Text,
                };
                db.Cities.Add(newCity);
                if (MBClass.QuestionMB("Вы действительно хотите добавить новый город?"))
                {
                    db.SaveChanges();
                    MBClass.InfoMB($"Город {newCity.NameCity} успешно добавлен!");
                }
            }
        }
    }
}
