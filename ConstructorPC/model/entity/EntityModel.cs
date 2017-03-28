namespace ConstructorPC.model.entity
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EntityModel : DbContext
    {
        public EntityModel()
            : base("name=EntityModel")
        {
        }

        public virtual DbSet<build> builds { get; set; }
        public virtual DbSet<Category> categories { get; set; }
        public virtual DbSet<cpu> cpus { get; set; }
        public virtual DbSet<graphic_cards> graphic_cards { get; set; }
        public virtual DbSet<hdd> hdds { get; set; }
        public virtual DbSet<Interface> interfaces { get; set; }
        public virtual DbSet<Manufacturer> manufacturers { get; set; }
        public virtual DbSet<Motherboard> motherboards { get; set; }
        public virtual DbSet<order> orders { get; set; }
        public virtual DbSet<power_supplies> power_supplies { get; set; }
        public virtual DbSet<Product> products { get; set; }
        public virtual DbSet<ram> rams { get; set; }
        public virtual DbSet<Ware> wares { get; set; }
        public virtual DbSet<complect> complects { get; set; }
        public virtual DbSet<itf_to_ware> itf_to_ware { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<build>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<build>()
                .HasMany(e => e.complects)
                .WithRequired(e => e.build)
                .HasForeignKey(e => e.builds_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<build>()
                .HasMany(e => e.orders)
                .WithRequired(e => e.build)
                .HasForeignKey(e => e.builds_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.ct_name)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.wares)
                .WithRequired(e => e.category)
                .HasForeignKey(e => e.category_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<cpu>()
                .Property(e => e.cpu_core_arch)
                .IsUnicode(false);

            modelBuilder.Entity<cpu>()
                .Property(e => e.cpu_soket)
                .IsUnicode(false);

            modelBuilder.Entity<cpu>()
                .Property(e => e.cpu_cache3L_size)
                .IsUnicode(false);

            modelBuilder.Entity<cpu>()
                .Property(e => e.ga_name)
                .IsUnicode(false);

            modelBuilder.Entity<cpu>()
                .Property(e => e.avalable_techonology)
                .IsUnicode(false);

            modelBuilder.Entity<graphic_cards>()
                .Property(e => e.gc_mem_type)
                .IsUnicode(false);

            modelBuilder.Entity<graphic_cards>()
                .Property(e => e.gc_mem_freq)
                .IsUnicode(false);

            modelBuilder.Entity<Interface>()
                .Property(e => e.itf_name)
                .IsUnicode(false);

            modelBuilder.Entity<Interface>()
                .Property(e => e.itf_type)
                .IsUnicode(false);

            //modelBuilder.Entity<Interface>()
            //    .HasMany(e => e.itf_to_ware)
            //    .WithRequired(e => e._interface)
            //    .HasForeignKey(e => e.interface_id)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Manufacturer>()
                .Property(e => e.mf_name)
                .IsUnicode(false);

            modelBuilder.Entity<Manufacturer>()
                .HasMany(e => e.products)
                .WithRequired(e => e.Manufacturer)
                .HasForeignKey(e => e.manufacturers_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Motherboard>()
                .Property(e => e.mb_form_factor)
                .IsUnicode(false);

            modelBuilder.Entity<Motherboard>()
                .Property(e => e.mb_socket)
                .IsUnicode(false);

            modelBuilder.Entity<Motherboard>()
                .Property(e => e.chipset)
                .IsUnicode(false);

            modelBuilder.Entity<Motherboard>()
                .Property(e => e.audio_adapter)
                .IsUnicode(false);

            modelBuilder.Entity<Motherboard>()
                .Property(e => e.netcard)
                .IsUnicode(false);

            modelBuilder.Entity<order>()
                .Property(e => e.customer)
                .IsUnicode(false);

            modelBuilder.Entity<power_supplies>()
                .Property(e => e.ps_features)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.complects)
                .WithRequired(e => e.product)
                .HasForeignKey(e => e.product_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ram>()
                .Property(e => e.ram_type)
                .IsUnicode(false);

            modelBuilder.Entity<Ware>()
                .Property(e => e.ware_name)
                .IsUnicode(false);

            modelBuilder.Entity<Ware>()
                .HasMany(e => e.products)
                .WithRequired(e => e.Ware)
                .HasForeignKey(e => e.ware_id)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Ware>()
            //    .HasMany(e => e.itf_to_ware)
            //    .WithRequired(e => e.ware)
            //    .HasForeignKey(e => e.ware_id)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ware>()
                .HasMany(c => c.interfaces)
                .WithMany(p => p.wares)
                .Map(m =>
                    {
                        // Ссылка на промежуточную таблицу
                        m.ToTable("pc_components.itf_to_ware");

                        // Настройка внешних ключей промежуточной таблицы
                        m.MapLeftKey("ware_id");
                        m.MapRightKey("interface_id");
                    });
        }
    }
}
