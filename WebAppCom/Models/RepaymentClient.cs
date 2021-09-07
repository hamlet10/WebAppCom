using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppCom.Models
{
    public partial class RepaymentClient
    {
        public RepaymentClient()
        {
            RepaymentClientDetails = new HashSet<RepaymentClientDetails>();
        }

        [Key]
        public int RepaymentClientId { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        public int SalesId { get; set; }

        [ForeignKey(nameof(SalesId))]
        [InverseProperty(nameof(Sale.RepaymentClient))]
        public virtual Sale Sales { get; set; }
        [InverseProperty("RepaymentClient")]
        public virtual ICollection<RepaymentClientDetails> RepaymentClientDetails { get; set; }
    }
}
