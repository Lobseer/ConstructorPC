using ConstructorPC.service;
using ConstructorPC.beans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using System.Windows.Input;

namespace ConstructorPC.view
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Build constructor = new Build();

        public MainWindow()
        {            
            InitializeComponent();
            doSmth();       
        }

        private void doSmth()
        {
            constructor.mbList = new List<Motherboard> {
                new Motherboard { Name = "MSI M110t" },
                new Motherboard { Name = "ASUS B115re" }
            };
            constructor.cpuList = new List<Cpu> {
                new Cpu { Name = "AMD Ryzen x7" } ,
                new Cpu { Name = "Intel Core i7 6900" }
            };
            //exMb.Content = constructor.mbList;
            //exCpu.Content = constructor.cpuList;
            lbxCpu.ItemsSource = constructor.cpuList;
            lbxMb.ItemsSource = constructor.mbList;          
        }

        #region standart buttons

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            //if(MessageBox.Show("Are you sure?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes) == MessageBoxResult.Yes)
            {
                frmMainWindow.Close();
            }
        }     

        private void aWinMove_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        #endregion
    }
}
