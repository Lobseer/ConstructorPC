namespace ConstructorPC.model.entity
{
    using service;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pc_components.power_supplies")]
    public partial class power_supplies : NotifyPropertyChanged
    {
        public int id { get; set; }

        public int? power { get; set; }

        public float? efficiency { get; set; }

        public bool? pfc_modul { get; set; }

        public bool? modular { get; set; }

        public int? noize_level { get; set; }

        [StringLength(120)]
        public string ps_features { get; set; }
    }
}
