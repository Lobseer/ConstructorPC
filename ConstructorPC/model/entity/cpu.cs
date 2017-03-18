namespace ConstructorPC.model.entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pc_components.cpu")]
    public partial class cpu
    {
        public int id { get; set; }

        [StringLength(20)]
        public string cpu_core_arch { get; set; }

        [Required]
        [StringLength(20)]
        public string cpu_soket { get; set; }

        public int cpu_clock_rate { get; set; }

        public int? cpu_max_clock_rate { get; set; }

        public int cpu_core_count { get; set; }

        [StringLength(20)]
        public string cpu_cache3L_size { get; set; }

        public int? cpu_peak_temeprature { get; set; }

        public int cpu_consum_pow { get; set; }

        public int? max_mem_freq { get; set; }

        [StringLength(20)]
        public string ga_name { get; set; }

        public bool? free_factor { get; set; }

        [StringLength(120)]
        public string avalable_techonology { get; set; }
    }
}
