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

namespace M120Projekt
{
    /// <summary>
    /// Interaktionslogik für OverviewWindow.xaml
    /// </summary>
    public partial class OverviewWindow : Window
    {
        public OverviewWindow()
        {
            InitializeComponent();
            this.placeholder.Content = new ArticleUC();
        }

        private void NewArticleButton_Click(object sender, RoutedEventArgs e)
        {
            this.placeholder.Content = new MainWindow(this);
            this.newArticleButton.IsEnabled = false;
        }

        internal void updateSource()
        {
            ((ArticleUC)this.placeholder.Content).articleList.ItemsSource = Data.Artikel.LesenAlle();
        }
    }
}
