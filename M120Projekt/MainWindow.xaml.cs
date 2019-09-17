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
        private bool articleInvalid = false;

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
        }

        private void SubmitButton_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
           
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            Data.Artikel article = new Data.Artikel();
            article.Bezeichnung = titleTextBox.Text;
            article.Anzahl = int.Parse(amountTextBox.Text);
            article.KaufenBis = (DateTime)this.buyUntilPicker.SelectedDate;
            article.Kategorie = (String)((ComboBoxItem)this.categoryComboBox.SelectedItem).Content;
            long id = article.Erstellen();

            var result = MessageBox.Show("Neuer Eintrag mit Bezeichnung: " + titleTextBox.Text + ", Anzahl: " + amountTextBox.Text +
                           ", Einkaufdatum: " + buyUntilPicker.Text + ", Kategorie: " + ((ListBoxItem) categoryComboBox.SelectedItem).Content + " soll erstellt werden.");

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
            if (this.titleTextBox.Text == "" || this.titleTextBox.Text.Length > 20)
            {
                this.titleErrorLabel.Content = this.titleTextBox.Text == "" ? "Die Bezeichnung darf nicht leer sein" : "Die Bezeichnung muss weniger als 30 Zeichen enhalten";
                this.titleErrorLabel.Visibility = Visibility.Visible;
                this.articleInvalid = true;
            }
            else
            {
                this.titleErrorLabel.Visibility = Visibility.Hidden;
                this.articleInvalid = false;
            }
        }

        private void AmountTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int x;
            if (this.amountTextBox.Text == "" || !(int.TryParse(this.amountTextBox.Text, out x) && x > 0))
            {
                this.amountErrorLabel.Content = this.amountTextBox.Text == "" ? "Die Anzahl darf nicht leer sein" : "Die Anzahl muss eine positive ganze Zahl sein";
                this.amountErrorLabel.Visibility = Visibility.Visible;
                this.articleInvalid = true;
            }
            else
            {
                this.amountErrorLabel.Visibility = Visibility.Hidden;
                this.articleInvalid = false;
            }
        }
    }
}
