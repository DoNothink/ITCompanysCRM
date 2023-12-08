using ITCompanysCRM.ClassFolder;
using ITCompanysCRM.Model;
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

namespace ITCompanysCRM.WindowFolder.ManagerFolder
{
    /// <summary>
    /// Interaction logic for EditClientWindow.xaml
    /// </summary>
    public partial class EditClientWindow : Window
    {
        List<TextBox> textBoxes = new List<TextBox>();
        private Client client { get;set; }

        public EditClientWindow(Client client)
        {
            InitializeComponent();
            LoadCB();
            this.client = client;
            DataContext = client;
            TypeOfClientCB.SelectedIndex = client.IdTypeOfClient++;
        }

        private void EditClientBtn_Click(object sender, RoutedEventArgs e)
        {
            if (TypeOfClientCB.SelectedIndex == -1)
            {
                MBClass.ErrorMB("Выберите тип клиента");
                TypeOfClientCB.Focus();
                return;
            }

            if (CheckTextBoxesClass.CheckTextBoxes(textBoxes) == false)
            {
                return;
            }

            using (ItcompanysCrmdbContext db = new())
            {
                Client editableClient = db.Clients.First(x=>x.IdClient == client.IdClient);
                editableClient.NameClient = NameClientTB.Text;
                editableClient.EmailClient = EmailClientTB.Text;
                editableClient.PhoneNumberClient = PhoneNumberClientTB.Text;
                editableClient.ServicesClient = ServicesClientTB.Text;
                editableClient.OthersData = OthersDataTB.Text;
                editableClient.IdTypeOfClient = int.Parse(TypeOfClientCB.SelectedValue.ToString());

                bool resultMB = MBClass.QuestionMB("Вы действительно хотите изменить данные?");
                if(resultMB)
                {
                    db.SaveChanges();
                    MBClass.InfoMB("Данные клиента были успешно изменены");
                    LogClass.LogToDataBase($"Данные клиента {client.IdClient} были изменены пользователем Id: {GlobalClass.GlobalUser.IdUser}");
                }
            }
        }

        private void PhoneNumberClientTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            string newVal = Regex.Replace(PhoneNumberClientTB.Text, @"[^0-9]", "");
            if (newVal.Length <= 1)
            {
                PhoneNumberClientTB.Text = Regex.Replace(newVal, @"(\d{1})", "+$1");
                PhoneNumberClientTB.Select(3, 0);
            }
            else if (newVal.Length <= 4)
            {
                PhoneNumberClientTB.Text = Regex.Replace(newVal, @"(\d{1})(\d{0,3})", "+$1($2)"); PhoneNumberClientTB.Select(7, 0);
            }
            else if (newVal.Length <= 7)
            {
                PhoneNumberClientTB.Text = Regex.Replace(newVal, @"(\d{1})(\d{3})(\d{0,3})", "+$1($2)$3");
                PhoneNumberClientTB.Select(11, 0);
            }
            else if (newVal.Length <= 9)
            {
                PhoneNumberClientTB.Text = Regex.Replace(newVal, @"(\d{1})(\d{3})(\d{0,3})(\d{0,2})", "+$1($2)$3-$4"); PhoneNumberClientTB.Select(16, 0);
            }
            else if (newVal.Length > 9)
            {
                PhoneNumberClientTB.Text = Regex.Replace(newVal, @"(\d{1})(\d{3})(\d{0,3})(\d{0,2})(\d{0,2})", "+$1($2)$3-$4-$5");
                PhoneNumberClientTB.Select(18, 0);
            }
        }


        private void LoadCB()
        {
            using (ItcompanysCrmdbContext db = new())
            {
                TypeOfClientCB.ItemsSource = db.TypeOfClients.ToList();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            textBoxes.Add(NameClientTB);
            textBoxes.Add(PhoneNumberClientTB);
            textBoxes.Add(ServicesClientTB);
        }
    }
}
