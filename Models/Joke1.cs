namespace MVCADO.NET.Joke.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Jokes")]
    public partial class Joke1
    {
        public int ID { get; set; }

        public string Content { get; set; }

        public int State { get; set; }

        public string Belong { get; set; }

        public DateTime AddDate { get; set; }
    }
}
