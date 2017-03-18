namespace ConstructorPC.model.entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pc_components.motherboards")]
    public partial class motherboard
    {
        public int id { get; set; }

        [StringLength(20)]
        public string mb_form_factor { get; set; }

        [Required]
        [StringLength(20)]
        public string mb_socket { get; set; }

        public int? mb_ram_max_frequency { get; set; }

        [StringLength(45)]
        public string chipset { get; set; }

        [StringLength(45)]
        public string audio_adapter { get; set; }

        [StringLength(45)]
        public string netcard { get; set; }

        public int mb_consum_pow { get; set; }
    }
}
