using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppCom.Models
{
    public partial class BodegaProduct
    {
        [Key]
        public int BodegaId { get; set; }
        [Key]
        public int ProductId { get; set; }
        public int Stock { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public int MakeUpDay { get; set; }
        public int? MinQuantity { get; set; }

        [ForeignKey(nameof(BodegaId))]
        [InverseProperty("BodegaProduct")]
        public virtual Bodega Bodega { get; set; }
        [ForeignKey(nameof(ProductId))]
        [InverseProperty("BodegaProduct")]
        public virtual Product Product { get; set; }
    }
}
