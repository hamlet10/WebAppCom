using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppCom.Models
{
    public partial class Measure
    {
        public Measure()
        {
            Product = new HashSet<Product>();
        }

        [Key]
        public int MeasureId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Abbreviation { get; set; }

        [InverseProperty("MeasureNavigation")]
        public virtual ICollection<Product> Product { get; set; }
    }
}
