using Accounting_Project.Models;
using Accounting_Project.ViewModels;
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

namespace Accounting_Project.Views
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu(User user)
        {
            InitializeComponent();
            //Main.Content = new ReviewPage();
            DataContext = new MainMenuViewModel(this, user);
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
            // OR You can Also go for below logic
            // Environment.Exit(0);
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = int.Parse(((sender as TabControl).SelectedItem as TabItem).Uid);

            switch (index)
            {
                case 0:
                    Application.Current.Shutdown();
                    break;
                case 1:
                    GridMain.Background = Brushes.Gray;
                    break;
                case 2:
                    GridMain.Background = Brushes.DimGray;
                    break;
                case 3:
                    GridMain.Background = Brushes.Gray;
                    break;
            }
        }
    }
}
