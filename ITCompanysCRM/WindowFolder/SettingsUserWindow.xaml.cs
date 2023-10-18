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
    /// Логика взаимодействия для SettingsUserWindow.xaml
    /// </summary>
    public partial class SettingsUserWindow : Window
    {
        public SettingsUserWindow()
        {
            InitializeComponent();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            bool resultMB = MBClass.QuestionMB("Вы действительно желаете выйти без сохранения данных?");
            if (resultMB)
                this.Close();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
