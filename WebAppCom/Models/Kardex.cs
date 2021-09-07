using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppCom.Models
{
    public partial class Kardex
    {
        public Kardex()
        {
            OutPutDetails = new HashSet<OutPutDetails>();
            PurchaseDetails = new HashSet<PurchaseDetails>();
            RepaymentClientDetails = new HashSet<RepaymentClientDetails>();
            RepaymentSupplierDetails = new HashSet<RepaymentSupplierDetails>();
        }

        [Key]
        public int KardexId { get; set; }
        public int BodegaId { get; set; }
        public int ProductId { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        [Required]
        [StringLength(20)]
        public string Documents { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Entry { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Output { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? Balance { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? LastCost { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? AverageCost { get; set; }

        [ForeignKey(nameof(BodegaId))]
        [InverseProperty("Kardex")]
        public virtual Bodega Bodega { get; set; }
        [ForeignKey(nameof(ProductId))]
        [InverseProperty("Kardex")]
        public virtual Product Product { get; set; }
        [InverseProperty("Kardex")]
        public virtual ICollection<OutPutDetails> OutPutDetails { get; set; }
        [InverseProperty("Kardex")]
        public virtual ICollection<PurchaseDetails> PurchaseDetails { get; set; }
        [InverseProperty("Kardex")]
        public virtual ICollection<RepaymentClientDetails> RepaymentClientDetails { get; set; }
        [InverseProperty("Kardex")]
        public virtual ICollection<RepaymentSupplierDetails> RepaymentSupplierDetails { get; set; }
    }
}
