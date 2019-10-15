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

namespace M120Projekt
{
    /// <summary>
    /// Interaktionslogik für UserControl1.xaml
    /// </summary>
    public partial class ArticleUC : UserControl
    {
        public ArticleUC()
        {
            InitializeComponent();
            this.articleList.ItemsSource = Data.Artikel.LesenAlle();
        }

        private void ArticleList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (articleList.SelectedItem != null)
            {
                new EditWindow(((Data.Artikel)articleList.SelectedItem).ArtikelID).Show();
            }
        }
    }
}
