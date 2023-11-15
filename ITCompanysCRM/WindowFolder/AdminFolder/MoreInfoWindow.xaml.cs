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

namespace ITCompanysCRM.WindowFolder.AdminFolder
{
    /// <summary>
    /// Логика взаимодействия для MoreInfoWindow.xaml
    /// </summary>
    public partial class MoreInfoWindow : Window
    {
        public MoreInfoWindow(Staff _staff)
        {
            InitializeComponent();
            DataContext = _staff;
            LoadContext(_staff);
        }
        
        private void LoadContext(Staff _staff)
        {
            try
            {
                using (ItcompanysCrmdbContext db = new())
                {
                    IssuedPassLabel.Content = db.IssuedPassViews
                        .First(x => x.IdIssuedPassport == _staff.IdIssuedPassport)
                        .NameIssuedPassport;
                    AddressLabel.Content = db.AddressViews
                        .First(x => x.IdAddress == _staff.IdAddress)
                        .NameAddress;
                }
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB("Произошла ошибка при загрузке данных. Повторите попытку позже");
                Console.WriteLine(ex);
            }
        }
    }
}
