using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppCom.Models
{
    public partial class Purchase
    {
        public Purchase()
        {
            PurchaseDetails = new HashSet<PurchaseDetails>();
            RepaymentSupplier = new HashSet<RepaymentSupplier>();
        }

        [Key]
        public int PurchaseId { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        public int SupplierId { get; set; }
        [Column("BodegaID")]
        public int BodegaId { get; set; }

        [ForeignKey(nameof(BodegaId))]
        [InverseProperty("Purchase")]
        public virtual Bodega Bodega { get; set; }
        [ForeignKey(nameof(SupplierId))]
        [InverseProperty("Purchase")]
        public virtual Supplier Supplier { get; set; }
        [InverseProperty("Purchase")]
        public virtual ICollection<PurchaseDetails> PurchaseDetails { get; set; }
        [InverseProperty("Purchase")]
        public virtual ICollection<RepaymentSupplier> RepaymentSupplier { get; set; }
    }
}
