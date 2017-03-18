namespace ConstructorPC.model.entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pc_components.ram")]
    public partial class ram
    {
        public int id { get; set; }

        [StringLength(15)]
        public string ram_type { get; set; }

        public int? ram_mem_size { get; set; }

        public int? ram_freq { get; set; }
    }
}
