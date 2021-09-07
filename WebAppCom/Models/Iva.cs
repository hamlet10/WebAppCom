using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppCom.Models
{
    [Table("IVA")]
    public partial class Iva
    {
        public Iva()
        {
            Product = new HashSet<Product>();
            PurchaseDetailsDiscountRateNavigation = new HashSet<PurchaseDetails>();
            PurchaseDetailsPercentIvaNavigation = new HashSet<PurchaseDetails>();
            RepaymentClientDetailsDiscountRateNavigation = new HashSet<RepaymentClientDetails>();
            RepaymentClientDetailsPercentIvaNavigation = new HashSet<RepaymentClientDetails>();
            RepaymentSupplierDetailsDiscountRateNavigation = new HashSet<RepaymentSupplierDetails>();
            RepaymentSupplierDetailsPercentIvaNavigation = new HashSet<RepaymentSupplierDetails>();
        }

        [Key]
        [Column("IVAId")]
        public int Ivaid { get; set; }
        [Required]
        public string Description { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal Rate { get; set; }

        [InverseProperty("Iva")]
        public virtual ICollection<Product> Product { get; set; }
        [InverseProperty(nameof(PurchaseDetails.DiscountRateNavigation))]
        public virtual ICollection<PurchaseDetails> PurchaseDetailsDiscountRateNavigation { get; set; }
        [InverseProperty(nameof(PurchaseDetails.PercentIvaNavigation))]
        public virtual ICollection<PurchaseDetails> PurchaseDetailsPercentIvaNavigation { get; set; }
        [InverseProperty(nameof(RepaymentClientDetails.DiscountRateNavigation))]
        public virtual ICollection<RepaymentClientDetails> RepaymentClientDetailsDiscountRateNavigation { get; set; }
        [InverseProperty(nameof(RepaymentClientDetails.PercentIvaNavigation))]
        public virtual ICollection<RepaymentClientDetails> RepaymentClientDetailsPercentIvaNavigation { get; set; }
        [InverseProperty(nameof(RepaymentSupplierDetails.DiscountRateNavigation))]
        public virtual ICollection<RepaymentSupplierDetails> RepaymentSupplierDetailsDiscountRateNavigation { get; set; }
        [InverseProperty(nameof(RepaymentSupplierDetails.PercentIvaNavigation))]
        public virtual ICollection<RepaymentSupplierDetails> RepaymentSupplierDetailsPercentIvaNavigation { get; set; }
    }
}
