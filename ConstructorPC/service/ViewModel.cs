using ConstructorPC.model.entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastReport;
using System.IO;
using System.Windows.Controls;
using System.Windows.Markup;
using ConstructorPC.view.pages;
using System.Windows;
using System.Reflection;
using Microsoft.Win32;

namespace ConstructorPC.service
{
    public class ViewModel : NotifyPropertyChanged
    {
        private EntityModel model;

        public ObservableCollection<Product> Products { get; private set; }

        public BindingList<Manufacturer> Manufacturers { get; private set; }

        public BindingList<Category> ProductCategories { get; private set; }

        public Page InfoPage { get; private set; }

        public Page DetailedInfoPage { get; private set; }

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
                LoadDetailedInfo();
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

        public ViewModel()
        {
            model = new EntityModel();
            model.manufacturers.Load();
            model.categories.Load();
            model.interfaces.Load();
            model.wares.Load();
            model.products.Load();

            model.motherboards.Load();
            model.power_supplies.Load();
            model.graphic_cards.Load();
            model.cpus.Load();
            model.rams.Load();
            model.hdds.Load();

            Manufacturers = model.manufacturers.Local.ToBindingList();
            ProductCategories = model.categories.Local.ToBindingList();

            Products = new ObservableCollection<Product>(model.products.Local);

            InfoPage = new pgProduct();
            InfoPage.DataContext = this;

            TempProduct = new Product();
        }

        #region Utility methods

        private Motherboard curMb;
        private power_supplies curPs;
        private graphic_cards curGc;
        private cpu curCpu;
        private ram curRam;
        private hdd curHdd;

        private void LoadDetailedInfo()
        {
            if (CurrentProduct == null)
            {
                DetailedInfoPage = null;
                OnPropertyChanged("DetailedInfoPage");
            }
            else
            {
                switch (CurrentProduct.Category.id)
                {
                    case 1:
                        DetailedInfoPage = new pgMotherboard();
                        curMb = model.motherboards.Find(CurrentProduct?.Ware.ware_id);
                        DetailedInfoPage.DataContext = curMb;
                        OnPropertyChanged("DetailedInfoPage");
                        break;
                    case 2:
                        DetailedInfoPage = new pgPowerSupply();
                        curPs = model.power_supplies.Find(CurrentProduct?.Ware.ware_id);
                        DetailedInfoPage.DataContext = curPs;
                        OnPropertyChanged("DetailedInfoPage");
                        break;
                    case 3:
                        DetailedInfoPage = new pgGraphicCard();
                        curGc = model.graphic_cards.Find(CurrentProduct?.Ware.ware_id);
                        DetailedInfoPage.DataContext = curGc;
                        OnPropertyChanged("DetailedInfoPage");
                        break;
                    case 4:
                        DetailedInfoPage = new pgCpu();
                        curCpu = model.cpus.Find(CurrentProduct?.Ware.ware_id);
                        DetailedInfoPage.DataContext = curCpu;
                        OnPropertyChanged("DetailedInfoPage");
                        break;
                    case 5:
                        DetailedInfoPage = new pgRam();
                        curRam = model.rams.Find(CurrentProduct?.Ware.ware_id);
                        DetailedInfoPage.DataContext = curRam;
                        OnPropertyChanged("DetailedInfoPage");

                        break;
                    case 6:
                        DetailedInfoPage = new pgHdd();
                        curHdd = model.hdds.Find(CurrentProduct?.Ware.ware_id);
                        DetailedInfoPage.DataContext = curHdd;
                        OnPropertyChanged("DetailedInfoPage");
                        break;
                }
            }
        }

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
                    object value = prop.GetValue(temp) ?? "null";
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

        private RelayCommand saveChangesCommand;
        public RelayCommand SaveChangesCommand
        {
            get
            {
                return saveChangesCommand ??
                  (saveChangesCommand = new RelayCommand(obj =>
                  {
                      if (MessageBox.Show("Are you sure? All changes will be modificated in data base!", "Reminder", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.Yes) == MessageBoxResult.Yes)
                      {
                          model.SaveChanges();
                      }
                  }));
            }
        }

        private RelayCommand changeTabCommand;
        public RelayCommand ChangeTabCommand
        {
            get
            {
                return changeTabCommand ??
                  (changeTabCommand = new RelayCommand(obj =>
                  {
                      int tabIndex = (int)obj + 1;
                      IEnumerable<Product> tempList = model.products.Local.Where((p) => p.Category.id == tabIndex);

                      Products = new ObservableCollection<Product>(tempList);
                      //Products = new ObservableCollection<Product>( model.products.Local.Where((p) => p.Category.id.Equals(tabIndex)));
                      OnPropertyChanged("Products");
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
                        SaveFileDialog saveFileDialog = GetSaveFileDialog();
                        if (saveFileDialog.ShowDialog() == true)
                        {
                            using (RepExcel exWriter = new RepExcel())
                            {
                                exWriter.CreateNewBook("page");
                                exWriter.ChangeCurrentPage("page");

                                WriteToExcel(exWriter, Products);

                                exWriter.SaveAs(saveFileDialog.FileName);
                                exWriter.CloseBook();
                            }
                        }
                    }));
            }
        }

        private RelayCommand showReportCommand;
        public RelayCommand ShowReportCommand
        {
            get
            {
                return showReportCommand ??
                  (showReportCommand = new RelayCommand(obj =>
                  {
                      Report report1 = new Report();
                      report1.Load("resources/FrTest.frx");
                      //report1.Show();

                      report1.Prepare();
                      // create an instance of XAML export filter
                      FastReport.Export.XAML.XAMLExport export = new FastReport.Export.XAML.XAMLExport();
                      // export in xaml
                      report1.Export(export, "result.xaml");
                      // Создание потока для чтения выбранного XAML файла
                      using (FileStream fs = new FileStream("result.xaml", FileMode.Open))
                      { // Создание нового окна для графического вывода содержимого XAML файла
                          Page pgReport = (Page)XamlReader.Load(fs);
                          InfoPage = pgReport;
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
                      model.products.Add(product);
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
                          model.products.Add(product);
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
                            model.products.Remove(product);
                            TempProduct = new Product();
                        }
                    },
                    (obj) => Products.Count > 0 || CurrentProduct == null));
            }
        }

        #endregion

    }
}
