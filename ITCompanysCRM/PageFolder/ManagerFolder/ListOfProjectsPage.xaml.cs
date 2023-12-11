using ITCompanysCRM.ClassFolder;
using ITCompanysCRM.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ITCompanysCRM.PageFolder.ManagerFolder
{
    /// <summary>
    /// Логика взаимодействия для ListOfProjectsPage.xaml
    /// </summary>
    public partial class ListOfProjectsPage : Page
    {
        public ListOfProjectsPage()
        {
            InitializeComponent();
            LoadCB();
            LoadDG();
        }

        private void LoadCB()
        {
            using (ItcompanysCrmdbContext db = new())
            {
                StatusOfProjectCB.ItemsSource = db.StatusOfProjects.ToList();
            }
        }

        private void LoadDG()
        {
            using (ItcompanysCrmdbContext db = new())
            {
                ProjectsDG.ItemsSource = db.Projects
                    .Include(c=>c.IdClientNavigation)
                    .Include(sp=>sp.IdStatusOfProjectNavigation)
                    .ToList();
            }
        }

        private void AddProject_Click(object sender, RoutedEventArgs e)
        {

        }

        //MenuItem's
        private void UpdateMi_Click(object sender, RoutedEventArgs e)
        {

        }

        //DG Tools
        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchTB.Text == string.Empty)
            {
                LoadDG();
                return;
            }
            using (ItcompanysCrmdbContext db = new())
            {
                ProjectsDG.ItemsSource = db.Projects
                    .Where(x => x.NameProjects.StartsWith(SearchTB.Text))
                    .ToList();
            }
        }

        private void StatusOfProjectCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StatusOfProjectCB.SelectedIndex != -1)
            {
                using (ItcompanysCrmdbContext db = new())
                {
                    ProjectsDG.ItemsSource = db.Projects
                        .Where(x => x.IdStatusOfProject == int.Parse(StatusOfProjectCB.SelectedValue.ToString()))
                        .ToList();
                }
            }
        }

        private void ResetBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StatusOfProjectCB.SelectedIndex = -1;
                SearchTB.Text = string.Empty;
                LoadDG();
            }
            catch (Exception)
            {
                MBClass.ErrorMB("Произошла ошибка. Повторите попытку позже");
            }
        }

        private void ExcelBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ExcelClass.ToExcelFile(ProjectsDG, "Список проектов");
            }
            catch (Exception)
            {
                MBClass.ErrorMB("Произошла ошибка. Повторите попытку");
            }
        }

        private void DeleteMi_Click(object sender, RoutedEventArgs e)
        {
            using (ItcompanysCrmdbContext db = new())
            {
                Project? selectedProject = ProjectsDG.SelectedItem as Project;
                if (selectedProject != null)
                {
                    bool resultMB = MBClass.QuestionMB($"Вы действительно хотите удалить проект {selectedProject.NameProjects}?");
                    if (resultMB)
                    {
                        db.Projects.Remove(selectedProject);
                        db.SaveChanges();
                        MBClass.InfoMB($"Проект {selectedProject.NameProjects} удален");
                        LogClass.LogToDataBase($"Проект {selectedProject.NameProjects} удален пользователем Id: {GlobalClass.GlobalUser.IdUser}");
                    }
                }
            }
            LoadDG();
        }
    }
}
