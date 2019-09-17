using System;
using System.Windows;
using System.Windows.Controls;

namespace M120Projekt
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool articleInvalid = true;

        public MainWindow()
        {
            InitializeComponent();
            // Aufruf diverse APIDemo Methoden
            APIDemo.ArtikelCreate();
            APIDemo.ArtikelCreateKurz();
            APIDemo.ArtikelRead();
            APIDemo.ArtikelUpdate();
            APIDemo.ArtikelRead();
            // APIDemo.ArtikelDelete();
            this.categoryComboBox.SelectedItem = this.categoryComboBox.Items.GetItemAt(0);
            this.buyUntilPicker.SelectedDate = DateTime.Now;
            this.submitButton.IsEnabled = !this.articleInvalid;
        }

        private void SubmitButton_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
           
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.articleInvalid) return;
            Data.Artikel article = new Data.Artikel();
            article.Bezeichnung = titleTextBox.Text;
            article.Anzahl = int.Parse(amountTextBox.Text);
            article.KaufenBis = (DateTime)this.buyUntilPicker.SelectedDate;
            article.Kategorie = (String)((ComboBoxItem)this.categoryComboBox.SelectedItem).Content;
            long id = article.Erstellen();

            var result = MessageBox.Show("Neuer Eintrag wurde erstellt!");

            if (result == MessageBoxResult.OK)
            {
                EditWindow editWindow = new EditWindow(id);
                editWindow.Show();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void TitleTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.validateArticle();
        }

        private void AmountTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.validateArticle();
        }

        private void validateArticle()
        {
            this.articleInvalid = false;
            if (this.titleTextBox.Text == "" || this.titleTextBox.Text.Length > 30)
            {
                this.titleErrorLabel.Content = this.titleTextBox.Text == "" ? "Die Bezeichnung darf nicht leer sein" : "Die Bezeichnung muss weniger als 30 Zeichen enhalten";
                this.titleErrorLabel.Visibility = Visibility.Visible;
                this.articleInvalid = true;

            }
            else
            {
                this.titleErrorLabel.Visibility = Visibility.Hidden;
               // this.articleInvalid = false;
            }

            int x;
            if (this.amountTextBox.Text == "" || !(int.TryParse(this.amountTextBox.Text, out x) && x > 0))
            {
                this.amountErrorLabel.Content = this.amountTextBox.Text == "" ? "Die Anzahl darf nicht leer sein" : "Die Anzahl muss eine positive ganze Zahl sein";
                this.amountErrorLabel.Visibility = Visibility.Visible;
                this.articleInvalid = true;
                if (!(int.TryParse(this.amountTextBox.Text, out x) && x < 999999999) && this.amountTextBox.Text != "")
                {
                    this.amountErrorLabel.Content = "Die Zahl muss kleiner als 999999999";
                    this.amountErrorLabel.Visibility = Visibility.Visible;
                    this.articleInvalid = true;

                }
            }
            else
            {
                this.amountErrorLabel.Visibility = Visibility.Hidden;
                // this.articleInvalid = false;
            }
            this.submitButton.IsEnabled = !this.articleInvalid;
        }
    }
}
