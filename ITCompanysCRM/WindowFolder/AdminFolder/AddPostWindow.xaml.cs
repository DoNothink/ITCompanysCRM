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
    /// Логика взаимодействия для AddPostWindow.xaml
    /// </summary>
    public partial class AddPostWindow : Window
    {
        public AddPostWindow()
        {
            InitializeComponent();
        }

        private void AddPostBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PostTB.Text))
            {
                MBClass.ErrorMB("Введите название добавляемой должности");
                PostTB.Focus();
                return;
            }
            using (ItcompanysCrmdbContext db = new())
            {
                var newPost = new Post()
                {
                    NamePost = PostTB.Text,
                };
                db.Posts.Add(newPost);
                if(MBClass.QuestionMB("Вы действительно хотите добавить новую должность?"))
                {
                    db.SaveChanges();
                    MBClass.InfoMB($"Должность {newPost.NamePost} успешно добавлена!");
                }
            }
        }
    }
}
