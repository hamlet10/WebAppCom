using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppCom.Models
{
    public partial class Transfer
    {
        [Key]
        public int TransferId { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        [Column("BodegaOriginID")]
        public int BodegaOriginId { get; set; }
        [Column("BidegaDestinyID")]
        public int BidegaDestinyId { get; set; }

        [ForeignKey(nameof(BidegaDestinyId))]
        [InverseProperty(nameof(Bodega.TransferBidegaDestiny))]
        public virtual Bodega BidegaDestiny { get; set; }
        [ForeignKey(nameof(BodegaOriginId))]
        [InverseProperty(nameof(Bodega.TransferBodegaOrigin))]
        public virtual Bodega BodegaOrigin { get; set; }
    }
}
