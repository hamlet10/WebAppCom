using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppCom.Models
{
    public partial class Bar
    {
        [Key]
        public int ProductId { get; set; }
        [Key]
        [Column("Bar")]
        public string Bar1 { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("Bar")]
        public virtual Product Product { get; set; }
    }
}
