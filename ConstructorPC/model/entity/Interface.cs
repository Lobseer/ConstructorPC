namespace ConstructorPC.model.entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pc_components.interfaces")]
    public partial class Interface
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Interface()
        {
            wares = new HashSet<Ware>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(45)]
        public string itf_name { get; set; }

        [Column(TypeName = "enum")]
        [Required]
        [StringLength(65532)]
        public string itf_type { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ware> wares { get; set; }
    }
}
