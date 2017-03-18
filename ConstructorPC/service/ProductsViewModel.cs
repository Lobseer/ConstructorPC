using ConstructorPC.dao.api;
using ConstructorPC.beans;
using ConstructorPC.view;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ConstructorPC.service
{
    public class ProductsViewModel : NotifyPropertyChanged
    {
        private AbstractDaoFactory daoFactory = AbstractDaoFactory.getDAOFactory(AbstractDaoFactory.MY_SQL);     

        public ObservableCollection<Product> Products { get; private set; }

        public List<string> Manufacturers { get; private set; }

        public List<string> ProductCategories { get; private set; }

        public ProductsViewModel()
        {
            Manufacturers = daoFactory.getManufacturerDao().ReadAll();
            Products = new ObservableCollection<Product>();
            Products.Add(new Product());
            Products.Add(new Product());

            ProductCategories = daoFactory.getCategoryDao().ReadAll();

            TempProduct = new Product();
        }

        private Product currentProduct;

        private Product tempProduct;

        public Product CurrentProduct
        {
            get
            {
                return currentProduct;
            }
            set
            {
                currentProduct = value;
                TempProduct = currentProduct?.Clone() as Product;
                OnPropertyChanged("CurrentProduct");
            }
        }

        public Product TempProduct
        {
            get
            {
                return tempProduct;
            }
            set
            {
                tempProduct = value;
                OnPropertyChanged("TempProduct");
            }
        }

        public IEnumerable<object> WareTable { get; private set; }
    

        #region Utility methods

        private OpenFileDialog GetOpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel files|*.xls;*.xlsx|All files (*.*)|*.*";
            return openFileDialog;
        }

        private SaveFileDialog GetSaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel files|*.xls;*.xlsx|All files (*.*)|*.*";
            //openFileDialog.InitialDirectory = @"\resources\";
            //MessageBox.Show(Environment.CurrentDirectory);//Application.Current.StartupUri.);
            return saveFileDialog;
        }

        private IEnumerable<object> ReadAllFrom(string tableName)
        {
            switch (tableName)
            {
                case "motherboards":
                    return daoFactory.getMotherboardDao().ReadAll();
                case "power_supplies":
                    return daoFactory.getPowerSupplyDao().ReadAllByProcedure(tableName);
                case "graphic_cards":
                    return daoFactory.getGraphicCardDao().ReadAll();
                case "cpu":
                    return daoFactory.getCpuDao().ReadAll();
                case "ram":
                    return daoFactory.getRamDao().ReadAll();
                case "hdd":
                    return daoFactory.getHddDao().ReadAll();
                default:
                    return null;
            }
        }

        private void WriteToExcel(RepExcel exWriter, IEnumerable<object> targetCollection)
        {
            Type type = targetCollection.FirstOrDefault().GetType();
            int ch = 65;
            foreach (PropertyInfo prop in type.GetProperties())
            {
                exWriter.SetValue($"{(char)ch}1", prop.Name, "string", true);
                ch++;
            }
            int i = 2;
            foreach (object temp in targetCollection)
            {
                ch = 65;
                foreach (PropertyInfo prop in type.GetProperties())
                {
                    object value = prop.GetValue(temp)??"null";
                    exWriter.SetValue($"{(char)ch}{i}", value.ToString(), "string");
                    ch++;
                }
                i++;
            }
        }

        private void ReadFromExcel<T>(RepExcel exWriter, ref List<T> targetCollection, Type type)
        {          
            int i = 2;
            int ch;
            while (false)
            {
                ch = 65;
                object val = Activator.CreateInstance(type);
                foreach (PropertyInfo prop in type.GetProperties())
                {
                    prop.SetValue(val, exWriter.GetValue($"{(char)ch}{i}"));
                    ch++;
                }
                i++;
                targetCollection.Add((T)val);
            }
        }

        #endregion

        #region Commands

        private RelayCommand choiseWareCommand;
        public RelayCommand ChoiseWareCommand
        {
            get
            {
                return choiseWareCommand ??
                    (choiseWareCommand = new RelayCommand(obj =>
                    {
                        if (obj != null)
                        {
                            MessageBox.Show(obj.ToString());
                        }
                    }));
            }
        }

        private RelayCommand writeToExcelCommand;
        public RelayCommand WriteToExcelCommand
        {
            get
            {
                return writeToExcelCommand ??
                    (writeToExcelCommand = new RelayCommand(obj =>
                    {
                        if (obj != null)
                        {
                            string wareType = (obj as string).ToLower().Replace(' ', '_');
                            WareTable = ReadAllFrom(wareType);

                            SaveFileDialog saveFileDialog = GetSaveFileDialog();
                            if (saveFileDialog.ShowDialog() == true)
                            {
                                using (RepExcel exWriter = new RepExcel())
                                {
                                    exWriter.CreateNewBook(wareType);
                                    exWriter.ChangeCurrentPage(wareType);

                                    WriteToExcel(exWriter, WareTable);

                                    exWriter.SaveAs(saveFileDialog.FileName);
                                    exWriter.CloseBook();
                                }
                            }
                        }
                    }));
            }
        }

        private RelayCommand openDbViewCommand;
        public RelayCommand OpenDbViewCommand
        {
            get
            {
                return openDbViewCommand ??
                    (openDbViewCommand = new RelayCommand(obj =>
                      {
                          if (obj != null)
                          {
                              string wareType = (obj as string).ToLower().Replace(' ', '_');

                              WareTable = ReadAllFrom(wareType);

                              Window viewTable = new DbViewWindow();
                              viewTable.DataContext = this;
                              viewTable.Owner = Application.Current.Windows[0];
                              viewTable.Show();
                          }
                      }));
            }
        }

        private RelayCommand applyCommand;
        public RelayCommand ApplyCommand
        {
            get
            {
                return applyCommand ??
                  (applyCommand = new RelayCommand(obj =>
                  {
                      if (CurrentProduct != null)
                          CurrentProduct.CopyFrom(TempProduct);
                      else
                          CopyCommand.Execute(TempProduct);
                  }));
            }
        }

        private RelayCommand cancelCommand;
        public RelayCommand CancelCommand
        {
            get
            {
                return cancelCommand ??
                  (cancelCommand = new RelayCommand(obj =>
                  {
                      TempProduct?.CopyFrom(CurrentProduct ?? new Product());
                  }));
            }
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      Product product = new Product();
                      Products.Insert(0, product);
                      CurrentProduct = product;
                  }));
            }
        }

        private RelayCommand copyCommand;
        public RelayCommand CopyCommand
        {
            get
            {
                return copyCommand ??
                  (copyCommand = new RelayCommand(obj =>
                  {
                      if (obj != null)
                      {
                          Product product = ((obj as Product).Clone()) as Product;
                          Products.Insert(0, product);
                          CurrentProduct = product;
                      }
                  }));
            }
        }

        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                    (removeCommand = new RelayCommand(obj =>
                    {
                        if (obj != null)
                        {
                            Product product = obj as Product;
                            Products.Remove(product);
                            TempProduct = new Product();
                        }
                    },
                    (obj) => Products.Count > 0 || CurrentProduct == null));
            }
        }

        #endregion

        #region Dependencies Property

        public static readonly DependencyProperty DataGridDoubleClickProperty =
            DependencyProperty.RegisterAttached("DataGridDoubleClickCommand", typeof(ICommand), typeof(ProductsViewModel),
                new PropertyMetadata(new PropertyChangedCallback(AttachOrRemoveDataGridDoubleClickEvent)));

        public static ICommand GetDataGridDoubleClickCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(DataGridDoubleClickProperty);
        }

        public static void SetDataGridDoubleClickCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(DataGridDoubleClickProperty, value);
        }

        public static void AttachOrRemoveDataGridDoubleClickEvent(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            DataGrid dataGrid = obj as DataGrid;
            if (dataGrid != null)
            {
                ICommand cmd = (ICommand)args.NewValue;

                if (args.OldValue == null && args.NewValue != null)
                {
                    dataGrid.MouseDoubleClick += ExecuteDataGridDoubleClick;
                }
                else if (args.OldValue != null && args.NewValue == null)
                {
                    dataGrid.MouseDoubleClick -= ExecuteDataGridDoubleClick;
                }
            }
        }

        private static void ExecuteDataGridDoubleClick(object sender, MouseButtonEventArgs args)
        {
            DependencyObject obj = sender as DependencyObject;
            ICommand cmd = (ICommand)obj.GetValue(DataGridDoubleClickProperty);
            if (cmd != null)
            {
                if (cmd.CanExecute(obj))
                {
                    cmd.Execute(obj);
                }
            }
        }

        #endregion
    }
}
