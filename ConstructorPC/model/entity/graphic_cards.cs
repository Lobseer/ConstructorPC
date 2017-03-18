namespace ConstructorPC.model.entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pc_components.graphic_cards")]
    public partial class graphic_cards
    {
        public int id { get; set; }

        public int? gpu_clock_rate { get; set; }

        [StringLength(20)]
        public string gc_mem_type { get; set; }

        public int? gc_mem_size { get; set; }

        [StringLength(45)]
        public string gc_mem_freq { get; set; }

        public int? px_shad_unit { get; set; }

        public int? vert_shad_unit { get; set; }

        public int? unif_shad_unit { get; set; }

        public int? tmu_count { get; set; }

        public int? rop_count { get; set; }

        public int? gc_consum_pow { get; set; }
    }
}
