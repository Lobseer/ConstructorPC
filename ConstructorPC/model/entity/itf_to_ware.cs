namespace ConstructorPC.model.entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pc_components.itf_to_ware")]
    public partial class itf_to_ware
    {
        public int? itf_count { get; set; }

        [Key]
        [Column("itf_in/out", Order = 0)]
        public bool itf_in_out { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ware_id { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int interface_id { get; set; }

        public virtual Interface _interface { get; set; }

        public virtual Ware ware { get; set; }
    }
}
