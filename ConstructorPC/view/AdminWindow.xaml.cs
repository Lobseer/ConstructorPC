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
using ConstructorPC.model.entity;

namespace ConstructorPC.view
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private ViewModel vm;
        public AdminWindow()
        {
            InitializeComponent();
            vm = new ViewModel();
            DataContext = vm;

            menuLanguage.Items.Clear();
            menuLanguage.SelectionChanged += ChangeLanguageClick;
            menuLanguage.ItemsSource = App.Languages;
            menuLanguage.DisplayMemberPath = "DisplayName";

            WindowState = WindowState.Maximized;
        }

        private void ChangeLanguageClick(object sender, EventArgs e)
        {
            CultureInfo lang = (sender as ComboBox).SelectedItem as CultureInfo;
            if (lang != null)
            {
                App.Language = lang;
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as TabControl).SelectedIndex == 1)
            {
                Page page = null;
                switch (vm.TempProduct?.Category?.ct_name ?? "")
                {
                    case "power_supplies":
                        page = new pgPowerSupply();
                        break;
                    case "motherboard":
                        page = new pgMotherboard();
                        break;
                    case "graphic_card":
                        break;
                    case "cpu":
                        page = new pgCpu();
                        break;
                    case "ram":
                        page = new pgRam();
                        break;
                    case "hdd":
                        break;
                    default:
                        return;
                }
                page.DataContext = vm;
                frDetailedInfo.Navigate(page);
            }
        }
    }
}
