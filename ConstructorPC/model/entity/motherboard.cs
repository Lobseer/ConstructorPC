namespace ConstructorPC.model.entity
{
    using service;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pc_components.motherboards")]
    public partial class Motherboard : NotifyPropertyChanged
    {
        public int id { get; set; }

        private string form_factor;

        [StringLength(20)]
        public string mb_form_factor
        {
            get
            {
                return form_factor;
            }
            set
            {
                form_factor = value;
                OnPropertyChanged("Mb_form_factor");
            }
        }

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

        [Required]
        public int mb_consum_pow { get; set; }
    }
}
