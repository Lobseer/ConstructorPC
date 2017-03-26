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

            Manufacturers = model.manufacturers.Local.ToBindingList();
            ProductCategories = model.categories.Local.ToBindingList();

            Products = model.products.Local;
          

            InfoPage = new pgProduct();
            InfoPage.DataContext = this;

            TempProduct = new Product();
        }

        #region Utility methods

        //private IEnumerable<object> ReadAllFrom(string tableName)
        //{
        //    switch (tableName)
        //    {
        //        case "motherboards":
        //            return daoFactory.getMotherboardDao().ReadAll();
        //        case "power_supplies":
        //            return daoFactory.getPowerSupplyDao().ReadAllByProcedure(tableName);
        //        case "graphic_cards":
        //            return daoFactory.getGraphicCardDao().ReadAll();
        //        case "cpu":
        //            return daoFactory.getCpuDao().ReadAll();
        //        case "ram":
        //            return daoFactory.getRamDao().ReadAll();
        //        case "hdd":
        //            return daoFactory.getHddDao().ReadAll();
        //        default:
        //            return null;
        //    }
        //}

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
                      IEnumerable<Product> temp = model.products.Local.Where((p) => p.Category.id==tabIndex);
                      Products = new ObservableCollection<Product>(temp);
                      //Products = new ObservableCollection<Product>( model.products.Local.Where((p) => p.Category.id.Equals(tabIndex)));
                      OnPropertyChanged("Products");
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
                      report1.Load(@"D:\lobseer\Documents\Projects\Project C\С#\ConstructorPC\ConstructorPC\resources\FrTest.frx");
                      //report1.Show();
                      // prepare a report
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

    }
}
