using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppCom.Models
{
    public partial class RepaymentSupplierDetails
    {
        [Key]
        public int LineId { get; set; }
        public int RepaymentSupplierId { get; set; }
        public int ProductId { get; set; }
        [Required]
        public string Description { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal Cost { get; set; }
        public int Quantity { get; set; }
        public int KardexId { get; set; }
        [Column("PercentIVA")]
        public int? PercentIva { get; set; }
        public int? DiscountRate { get; set; }

        [ForeignKey(nameof(DiscountRate))]
        [InverseProperty(nameof(Iva.RepaymentSupplierDetailsDiscountRateNavigation))]
        public virtual Iva DiscountRateNavigation { get; set; }
        [ForeignKey(nameof(KardexId))]
        [InverseProperty("RepaymentSupplierDetails")]
        public virtual Kardex Kardex { get; set; }
        [ForeignKey(nameof(PercentIva))]
        [InverseProperty(nameof(Iva.RepaymentSupplierDetailsPercentIvaNavigation))]
        public virtual Iva PercentIvaNavigation { get; set; }
        [ForeignKey(nameof(ProductId))]
        [InverseProperty("RepaymentSupplierDetails")]
        public virtual Product Product { get; set; }
        [ForeignKey(nameof(RepaymentSupplierId))]
        [InverseProperty("RepaymentSupplierDetails")]
        public virtual RepaymentSupplier RepaymentSupplier { get; set; }
    }
}
