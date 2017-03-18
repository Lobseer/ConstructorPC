using ConstructorPC.model.entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructorPC.service
{
    public class ViewModel : NotifyPropertyChanged
    {
        private EntityModel model;

        public ObservableCollection<Product> Products { get; private set; }

        public BindingList<Manufacturer> Manufacturers { get; private set; }

        public BindingList<Category> ProductCategories { get; private set; }

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

            Products = new ObservableCollection<Product>(model.products.Local);

            //CurrentProduct = new Product();
            //CurrentProduct.id = 115;
            //TempProduct.id = 228;

            //ApplyCommand.Execute(null);

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
                          Products. Insert(0, product);
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
