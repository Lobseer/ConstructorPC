using ConstructorPC.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructorPC.beans
{
    public class Product : NotifyPropertyChanged, ICloneable
    {
        private int id;
        private string name;
        private string manufacturer;
        private decimal price;
        private int inStock;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Manufacturer
        {
            get { return manufacturer; }
            set
            {
                manufacturer = value;
                OnPropertyChanged("Manufacturer");
            }
        }

        public decimal Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged("Price");
            }
        }
        public int InStock
        {
            get { return inStock; }
            set
            {
                inStock = value;
                OnPropertyChanged("InStock");
            }
        }

        public Product()
        {
            Name = "default";
        }       

        public void CopyFrom(Product val)
        {
            Id = val.Id;
            Name = val.Name;
            Manufacturer = val.Manufacturer;
            Price = val.Price;
            InStock = val.InStock;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
