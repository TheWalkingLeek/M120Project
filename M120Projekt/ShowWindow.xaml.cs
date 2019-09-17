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
    /// Interaktionslogik für EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {

        private enum modes { Show, Edit };
        private modes currentMode = modes.Show;

        private bool articleChanged = false;

        private bool articleInvalid = false;

        private Data.Artikel article;
        private long articleId = 2;
        public EditWindow(long articleId)
        {
            InitializeComponent();

            this.articleId = articleId;
            this.resetArticle();

            this.changeMode(modes.Show);
        }

        private void changeMode(modes targetMode)
        {
            this.currentMode = targetMode;
            switch (this.currentMode) {
                case modes.Show:
                    this.initShow();
                    break;
                case modes.Edit:
                    this.initEdit();
                    break;
            }
        }
        private void resetArticle()
        {
            this.article = Data.Artikel.LesenID(this.articleId);
            this.titleTextBox.Text = this.article.Bezeichnung;
            this.amountTextBox.Text = this.article.Anzahl + "";
            this.buyUntilPicker.SelectedDate = this.article.KaufenBis;
            int index = 0;
            for (int i = 0; i < this.categoryComboBox.Items.Count; i++)
            {
                ComboBoxItem item = (ComboBoxItem)this.categoryComboBox.Items.GetItemAt(i);
                if (item.Content.Equals(this.article.Kategorie))
                {
                    index = i;
                }
            }
            this.categoryComboBox.SelectedItem = this.categoryComboBox.Items.GetItemAt(index);
        }

        private void initShow()
        {
            this.articleChanged = false;
            this.cancelButton.Visibility = Visibility.Hidden;
            this.submitButton.Visibility = Visibility.Hidden;
            this.deleteButton.Visibility = Visibility.Visible;
            this.editButton.Visibility = Visibility.Visible;
            this.header.Content = "Artikel";
            this.setEnabledAttrs(false);
        }

        private void initEdit()
        {
            this.deleteButton.Visibility = Visibility.Hidden;
            this.editButton.Visibility = Visibility.Hidden;
            this.cancelButton.Visibility = Visibility.Visible;
            this.submitButton.Visibility = Visibility.Visible;
            this.header.Content = "Artikel bearbeiten";
            this.setEnabledAttrs(true);
        }

        private void setEnabledAttrs(bool value)
        {  
            this.titleTextBox.IsEnabled = value;
            this.amountTextBox.IsEnabled = value;
            this.buyUntilPicker.IsEnabled = value;
            this.categoryComboBox.IsEnabled = value;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            this.changeMode(modes.Edit);
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (!this.articleInvalid)
            {
                this.article.Bezeichnung = this.titleTextBox.Text;
                this.article.Anzahl = int.Parse(this.amountTextBox.Text);
                this.article.KaufenBis = (DateTime) this.buyUntilPicker.SelectedDate;
                this.article.Kategorie = (String) ((ComboBoxItem)this.categoryComboBox.SelectedItem).Content;
                this.article.Aktualisieren();
                this.changeMode(modes.Show);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Artikel unwiderruflich löschen", "Artikel löschen?", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                this.article.Loeschen();
                App.Current.Shutdown();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (!this.articleChanged)
            {
                this.changeMode(modes.Show);
            }
            else
            {
                var result = MessageBox.Show("Änderungen unwiderruflich verwerfen", "Änderungen verwerfen?", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    this.changeMode(modes.Show);
                    this.resetArticle();
                }
            }
        }

        private void TitleTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.articleChanged = true;
            if (this.titleTextBox.Text == "" || this.titleTextBox.Text.Length > 30) {
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
            this.articleChanged = true;
            int x;
            if (this.amountTextBox.Text == "" || !(int.TryParse(this.amountTextBox.Text, out x) && x > 0)) {
                this.amountErrorLabel.Content = this.amountTextBox.Text == "" ? "Die Anzahl darf nicht leer sein" : "Die Anzahl muss eine positive ganze Zahl sein";
                if (!(int.TryParse(this.amountTextBox.Text, out x) && x < 999999999) && this.amountTextBox.Text != "")
                {
                    this.amountErrorLabel.Content = "Die Zahl muss kleiner als 999999999";
                    this.amountErrorLabel.Visibility = Visibility.Visible;
                    this.articleInvalid = true;

                }
                this.amountErrorLabel.Visibility = Visibility.Visible;
                this.articleInvalid = true;
            } else
            {
                this.amountErrorLabel.Visibility = Visibility.Hidden;
                this.articleInvalid = false;
            }
        }

        private void BuyUntilPicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.articleChanged = true;
        }

        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.articleChanged = true;
        }
    }
}
