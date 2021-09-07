using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppCom.Models
{
    public partial class ImagenPathArrs
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ImagePath { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("ImagenPathArrs")]
        public virtual Product Product { get; set; }
    }
}
