using ITCompanysCRM.ClassFolder;
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

namespace ITCompanysCRM.WindowFolder.ManagerFolder
{
    /// <summary>
    /// Interaction logic for AddProjectWindow.xaml
    /// </summary>
    public partial class AddProjectWindow : Window
    {
        List<TextBox> textBoxes;

        public AddProjectWindow()
        {
            InitializeComponent();
            LoadCB();
        }

        private void LoadCB()
        {
            using (ItcompanysCrmdbContext db = new())
            {
                StatusCB.ItemsSource = db.StatusOfProjects.ToList();
                ClientCB.ItemsSource = db.Clients.ToList();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            textBoxes = new List<TextBox>();
            textBoxes.Add(NameProjectTB);
            textBoxes.Add(PurposeProjectTB);
            textBoxes.Add(DevBudgetTB);
        }

        private void AddProjectBtn_Click(object sender, RoutedEventArgs e)
        {
            if (StatusCB.SelectedIndex == -1)
            {
                MBClass.ErrorMB("Выберите статус проекта");
                StatusCB.Focus();
                return;
            }
            if(ClientCB.SelectedIndex == -1)
            {
                MBClass.ErrorMB("Выберите клиента");
                ClientCB.Focus();
                return;
            }
            if (CheckTextBoxesClass.CheckTextBoxes(textBoxes) == false)
                return;
            if(string.IsNullOrWhiteSpace(StartDevDP.Text) )
            {
                MBClass.ErrorMB("Выберите дату начала разработки");
                StartDevDP.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(FinishDevDP.Text))
            {
                MBClass.ErrorMB("Выберите дату окончания разработки");
                FinishDevDP.Focus();
                return;
            }

            using (ItcompanysCrmdbContext db = new())
            {
                Project newProject = new Project()
                {
                    NameProjects = NameProjectTB.Text,
                    PurposeProjects = PurposeProjectTB.Text,
                    StartOfDev = StartDevDP.SelectedDate.Value,
                    EndOfDev = FinishDevDP.SelectedDate.Value,
                    DevBudget = int.Parse(DevBudgetTB.Text),
                    IdStatusOfProject = int.Parse(StatusCB.SelectedValue.ToString()),
                    IdClient = int.Parse(ClientCB.SelectedValue.ToString()),
                };

                bool resultMB = MBClass.QuestionMB("Вы действительно хотите добавить новый проект?");
                if(resultMB)
                {
                    db.Projects.Add(newProject);
                    db.SaveChanges();

                    MBClass.InfoMB("Новый проект успешно добавлен");
                    LogClass.LogToDataBase($"Проект {newProject.NameProjects} добавлен пользователем Id:{GlobalClass.GlobalUser.IdUser}");
                }
            }
        }
    }
}
