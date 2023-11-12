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
    /// Логика взаимодействия для AddStreetWindow.xaml
    /// </summary>
    public partial class AddStreetWindow : Window
    {
        public AddStreetWindow()
        {
            InitializeComponent();
        }

        private void AddStreetBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(StreetTB.Text))
            {
                MBClass.ErrorMB("Введите название добавляемой улицы");
                StreetTB.Focus();
                return;
            }
            using (ItcompanysCrmdbContext db = new())
            {
                var newStreet = new Street()
                {
                    NameStreet = StreetTB.Text,
                };
                db.Streets.Add(newStreet);
                if (MBClass.QuestionMB("Вы действительно хотите добавить новую улицу?"))
                {
                    db.SaveChanges();
                    MBClass.InfoMB($"Улица {newStreet.NameStreet} успешно добавлена!");
                }
            }
        }
    }
}
