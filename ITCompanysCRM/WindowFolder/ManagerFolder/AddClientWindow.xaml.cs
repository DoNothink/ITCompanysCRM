using ITCompanysCRM.ClassFolder;
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

namespace ITCompanysCRM.WindowFolder.ManagerFolder
{
    /// <summary>
    /// Логика взаимодействия для AddClientWindow.xaml
    /// </summary>
    public partial class AddClientWindow : Window
    {
        List<TextBox> textBoxes;

        public AddClientWindow()
        {
            InitializeComponent();
            LoadCB();
        }

        // TODO: Check addCLient
        private void AddClientBtn_Click(object sender, RoutedEventArgs e)
        {
            if(TypeOfClientCB.SelectedIndex == -1)
            {
                MBClass.ErrorMB("Выберите тип клиента");
                TypeOfClientCB.Focus();
                return;
            }
            CheckTextBoxesClass.CheckTextBoxes(textBoxes);

            using (ItcompanysCrmdbContext db = new())
            {
                try
                {
                    Client newClient = new()
                    {
                        IdTypeOfClient = int.Parse(TypeOfClientCB.SelectedValue.ToString()),
                        NameClient = NameClientTB.Text,
                        EmailClient = EmailClientTB.Text,
                        PhoneNumberClient = PhoneNumberClientTB.Text,
                        ServicesClient = ServicesClientTB.Text,
                        OthersData = OthersDataTB.Text,
                    };

                    db.Clients.Add(newClient);
                    db.SaveChanges();
                    MBClass.InfoMB($"Клиент '{newClient.NameClient}' успешно добавлен");

                    this.Close();
                }
                catch (Exception)
                {
                    MBClass.ErrorMB("Произошла ошибка. Повторите попытку позже");
                }
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
            textBoxes = new List<TextBox>();
            textBoxes.Add(NameClientTB);
            textBoxes.Add(PhoneNumberClientTB);
            textBoxes.Add(ServicesClientTB);

        }
    }
}
