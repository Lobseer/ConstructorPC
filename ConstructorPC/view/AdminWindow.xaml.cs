using ConstructorPC.view.pages;
using ConstructorPC.service;
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
using System.Globalization;

namespace ConstructorPC.view
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();

            Page page = new pgProduct();
            page.DataContext = (ProductsViewModel)FindResource("vm");
            frProductInfo.Navigate(page);

            menuLanguage.Items.Clear();
            menuLanguage.SelectionChanged += ChangeLanguageClick;
            menuLanguage.ItemsSource = App.Languages;
            menuLanguage.DisplayMemberPath = "DisplayName";
        }

        private void ChangeLanguageClick(object sender, EventArgs e)
        {
            CultureInfo lang = (sender as ComboBox).SelectedItem as CultureInfo;
            if (lang != null)
            {
                App.Language = lang;
            }
        }
    }
}
