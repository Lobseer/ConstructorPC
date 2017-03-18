namespace ConstructorPC.model.entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pc_components.orders")]
    public partial class order
    {
        public int id { get; set; }

        [StringLength(45)]
        public string customer { get; set; }

        [Column(TypeName = "date")]
        public DateTime? order_date { get; set; }

        public int builds_id { get; set; }

        public virtual build build { get; set; }
    }
}
