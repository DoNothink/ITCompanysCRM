﻿using ITCompanysCRM.WindowFolder.ManagerFolder;
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
    /// Логика взаимодействия для ListOfClientPage.xaml
    /// </summary>
    public partial class ListOfClientPage : Page
    {
        public ListOfClientPage()
        {
            InitializeComponent();
            LoadDG();
            LoadCB();
        }

        private void TypeOfClientCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ResetBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TypeOfClientCB.SelectedIndex = -1;
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

        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            new AddClientWindow().ShowDialog();
            LoadDG();
        }

        private void LoadDG()
        {
            using (ItcompanysCrmdbContext db = new())
            {
                UsersDG.ItemsSource = db.Clients.ToList();
                db.TypeOfClients.Load();
            }
        }

        private void LoadCB()
        {
            using (ItcompanysCrmdbContext db = new())
            {
                TypeOfClientCB.ItemsSource = db.TypeOfClients.ToList();
            }
        }

    }
}
