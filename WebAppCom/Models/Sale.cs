using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppCom.Models
{
    public partial class Sale
    {
        public Sale()
        {
            RepaymentClient = new HashSet<RepaymentClient>();
        }

        [Key]
        public int SalesId { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        public int ClientId { get; set; }
        [Column("BodegaID")]
        public int BodegaId { get; set; }

        [ForeignKey(nameof(BodegaId))]
        [InverseProperty("Sale")]
        public virtual Bodega Bodega { get; set; }
        [ForeignKey(nameof(ClientId))]
        [InverseProperty("Sale")]
        public virtual Client Client { get; set; }
        [InverseProperty("Sales")]
        public virtual ICollection<RepaymentClient> RepaymentClient { get; set; }
    }
}
