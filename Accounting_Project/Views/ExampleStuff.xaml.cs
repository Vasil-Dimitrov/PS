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

namespace Accounting_Project.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ExampleStuff : Window
    {
        public ExampleStuff()
        {
            //InitializeComponent();
        }

        // This class is called automatically once everything on the front end is loaded
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.SelectionChanged(this.FinishComboBox, null);
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            this.WeldCheckbox.IsChecked = this.AssemblyCheckbox.IsChecked = this.PlasmaCheckbox.IsChecked = this.LaserCheckbox.IsChecked =
                this.PurchaseCheckbox.IsChecked = this.LatheCheckbox.IsChecked = this.DrillCheckbox.IsChecked = this.FoldCheckbox.IsChecked = 
                this.RollCheckbox.IsChecked = this.SawCheckbox.IsChecked = false;
        }

        private void Checkbox_Checked(object sender, RoutedEventArgs e)
        {
            this.LengthTextBlock.Text += ((CheckBox) sender).Content;
        }

        private void Checkbox_Unchecked(object sender, RoutedEventArgs e)
        {
            this.LengthTextBlock.Text = this.LengthTextBlock.Text.Replace((string) ((CheckBox)sender).Content, "");
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.NoteTextBox == null)
                return;

            var combo = (ComboBox)sender;
            var value = (ComboBoxItem) combo.SelectedValue;
            this.NoteTextBox.Text = (string) value.Content;
        }

        private void SupplierName_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.MassTextBox.Text = (string) ((TextBox)sender).Text;
        }
    }
}
