using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppCom.Models
{
    public partial class PurchaseDetails
    {
        [Key]
        public int LineId { get; set; }
        public int PurchaseId { get; set; }
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
        [InverseProperty(nameof(Iva.PurchaseDetailsDiscountRateNavigation))]
        public virtual Iva DiscountRateNavigation { get; set; }
        [ForeignKey(nameof(KardexId))]
        [InverseProperty("PurchaseDetails")]
        public virtual Kardex Kardex { get; set; }
        [ForeignKey(nameof(PercentIva))]
        [InverseProperty(nameof(Iva.PurchaseDetailsPercentIvaNavigation))]
        public virtual Iva PercentIvaNavigation { get; set; }
        [ForeignKey(nameof(ProductId))]
        [InverseProperty("PurchaseDetails")]
        public virtual Product Product { get; set; }
        [ForeignKey(nameof(PurchaseId))]
        [InverseProperty("PurchaseDetails")]
        public virtual Purchase Purchase { get; set; }
    }
}
