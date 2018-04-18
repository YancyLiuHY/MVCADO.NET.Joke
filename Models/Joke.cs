namespace MVCADO.NET.Joke.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Joke")]
    public partial class Joke
    {
        public int ID { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Content { get; set; }

        [Required]
        [StringLength(50)]
        public string Belong { get; set; }

        public int? State { get; set; }

        public DateTime? AddDate { get; set; }
    }
}
