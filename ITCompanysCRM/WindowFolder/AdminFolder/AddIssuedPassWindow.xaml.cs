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
    /// Логика взаимодействия для AddIssuedPassWindow.xaml
    /// </summary>
    public partial class AddIssuedPassWindow : Window
    {
        private static readonly Regex _numberRegex = new Regex("[^0-9.-]+");

        public AddIssuedPassWindow()
        {
            InitializeComponent();
        }

        private static bool IsTextAllowed(string text)
        {
            return !_numberRegex.IsMatch(text);
        }

        private void AddIssuedPassBtn_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(IssuedPassTB.Text))
            {
                MBClass.ErrorMB("Введите кем выдан паспорт");
                IssuedPassTB.Focus();
                return;
            }
            if(string.IsNullOrWhiteSpace(DivisionCodeTB.Text))
            {
                MBClass.ErrorMB("Введите код подразделения");
                DivisionCodeTB.Focus();
                return;
            }
            if(DivisionCodeTB.Text.Length <6)
            {
                MBClass.ErrorMB("Код подразделения должен содержать 6 цифр");
                DivisionCodeTB.Focus();
                return;
            }
            var newIssuedPass = new IssuedPassport()
            {
                IssuedPassport1 = IssuedPassTB.Text,
                DivisionCodePassport = int.Parse(DivisionCodeTB.Text),
            };
            using (ItcompanysCrmdbContext db = new())
            {
                db.IssuedPassports.Add(newIssuedPass);
                if (MBClass.QuestionMB("Вы действительно хотите добавить новое подразделение?"))
                {
                    db.SaveChanges();
                    MBClass.InfoMB($"Подразделение {newIssuedPass.IssuedPassport1} {newIssuedPass.DivisionCodePassport} успешно добавлена!");
                }
            }
        }

        private void DivisionCodeTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
    }
}
