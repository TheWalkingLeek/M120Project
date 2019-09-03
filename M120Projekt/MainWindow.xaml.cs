using System.Windows;
using System.Windows.Controls;

namespace M120Projekt
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
        }

        private void SubmitButton_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
           
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Neuer Eintrag mit Bezeichnung: " + titleTextBox.Text + ", Anzahl: " + amountTextBox.Text +
                           ", Einkaufdatum: " + buyUntilPicker.Text + ", Kategorie: " + ((ListBoxItem) categoryComboBox.SelectedItem).Content + " soll erstellt werden.");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
