using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppCom.Models
{
    public partial class RepaymentClientDetails
    {
        [Key]
        public int LineId { get; set; }
        public int RepaymentClientId { get; set; }
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
        [InverseProperty(nameof(Iva.RepaymentClientDetailsDiscountRateNavigation))]
        public virtual Iva DiscountRateNavigation { get; set; }
        [ForeignKey(nameof(KardexId))]
        [InverseProperty("RepaymentClientDetails")]
        public virtual Kardex Kardex { get; set; }
        [ForeignKey(nameof(PercentIva))]
        [InverseProperty(nameof(Iva.RepaymentClientDetailsPercentIvaNavigation))]
        public virtual Iva PercentIvaNavigation { get; set; }
        [ForeignKey(nameof(ProductId))]
        [InverseProperty("RepaymentClientDetails")]
        public virtual Product Product { get; set; }
        [ForeignKey(nameof(RepaymentClientId))]
        [InverseProperty("RepaymentClientDetails")]
        public virtual RepaymentClient RepaymentClient { get; set; }
    }
}
