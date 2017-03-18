using ConstructorPC.dao.api;
using ConstructorPC.model;
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

namespace ConstructorPC.ui.pages
{
    /// <summary>
    /// Interaction logic for pgDbView.xaml
    /// </summary>
    public partial class pgDbView : Page
    {
        private DaoFactory daoFactory = DaoFactory.getDAOFactory(DaoFactory.MY_SQL);

        public pgDbView()
        {
            InitializeComponent();
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            List<Motherboard> temp = daoFactory.getMotherboardDAO().ReadAll();
            dgView.ItemsSource = temp;
        }

    }
}
