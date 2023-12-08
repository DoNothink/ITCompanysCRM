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

        //DG Tools
        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void StatusOfProjectCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ResetBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExcelBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        //MenuItem's
        private void UpdateMi_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteMi_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
