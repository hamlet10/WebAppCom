using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppCom.Models
{
    public partial class SalesDetails
    {
        public int LineId { get; set; }
        public int SalesId { get; set; }
        public int ProductId { get; set; }
        [Required]
        public string Description { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal Cost { get; set; }
        public int Quantity { get; set; }
        public int KardexId { get; set; }
        [Column("PercentIVA")]
        public int PercentIva { get; set; }
        public int DiscountRate { get; set; }

        [ForeignKey(nameof(DiscountRate))]
        public virtual Iva DiscountRateNavigation { get; set; }
        [ForeignKey(nameof(LineId))]
        public virtual Sale Line { get; set; }
        [ForeignKey(nameof(PercentIva))]
        public virtual Iva PercentIvaNavigation { get; set; }
        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }
    }
}
