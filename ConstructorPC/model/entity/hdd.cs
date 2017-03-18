namespace ConstructorPC.model.entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pc_components.hdd")]
    public partial class hdd
    {
        public int id { get; set; }

        public int? hdd_capacity { get; set; }

        public int? hdd_transfer_rate { get; set; }

        public int? hdd_buff_size { get; set; }

        public int? hdd_spindle_speed { get; set; }

        public float? hdd_form_factor { get; set; }

        public int? hdd_noize_level { get; set; }

        public int? hdd_consum_pow { get; set; }
    }
}
