using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppCom.Models
{
    public partial class RepaymentSupplier
    {
        public RepaymentSupplier()
        {
            RepaymentSupplierDetails = new HashSet<RepaymentSupplierDetails>();
        }

        [Key]
        public int RepaymentSupplierId { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        public int PurchaseId { get; set; }

        [ForeignKey(nameof(PurchaseId))]
        [InverseProperty("RepaymentSupplier")]
        public virtual Purchase Purchase { get; set; }
        [InverseProperty("RepaymentSupplier")]
        public virtual ICollection<RepaymentSupplierDetails> RepaymentSupplierDetails { get; set; }
    }
}
