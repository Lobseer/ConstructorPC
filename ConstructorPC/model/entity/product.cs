namespace ConstructorPC.model.entity
{
    using service;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pc_components.products")]
    public partial class Product : NotifyPropertyChanged, ICloneable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            complects = new HashSet<complect>();
        }

        private int id;
        private decimal? price;
        private int? inStock;
        private Ware ware;
        private Manufacturer manufacturer;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        [Column("price")]
        public decimal? PriceProp
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged("PriceProp");
            }
        }

        [Column("in_stock")]
        public int? InStock
        {
            get { return inStock; }
            set
            {
                inStock = value;
                OnPropertyChanged("InStock");
            }
        }

        public Manufacturer Manufacturer
        {
            get { return manufacturer; }
            set
            {
                manufacturer = value;
                OnPropertyChanged("WareName");
            }
        }

        public Ware Ware
        {
            get { return ware; }
            set
            {
                ware = value;
                OnPropertyChanged("WareName");
                OnPropertyChanged("Category");
            }
        }

        [NotMapped]
        public string WareName
        {
            get
            {
                return $"{Manufacturer?.mf_name} {Ware?.ware_name}";
            }
        }

        [NotMapped]
        public Category Category
        {
            get { return Ware?.category; }
            set
            {
                OnPropertyChanged("Category");
            }
        }


        public int manufacturers_id { get; set; }

        public int ware_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<complect> complects { get; set; }

        public void CopyFrom(Product source)
        {
            Id = source.Id;
            PriceProp = source.price;
            InStock = source.InStock;
            manufacturers_id = source.manufacturers_id;
            ware_id = source.ware_id;
            Manufacturer = source.Manufacturer;
            Ware = source.Ware;
        }

        public object Clone()
        {
            Product result = new Product();
            result.CopyFrom(this);
            return result;
        }
    }
}
