using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppCom.Models
{
    public partial class Bodega
    {
        public Bodega()
        {
            BodegaProduct = new HashSet<BodegaProduct>();
            Kardex = new HashSet<Kardex>();
            Purchase = new HashSet<Purchase>();
            Sale = new HashSet<Sale>();
            TransferBidegaDestiny = new HashSet<Transfer>();
            TransferBodegaOrigin = new HashSet<Transfer>();
        }

        [Key]
        [Column("BodegaID")]
        public int BodegaId { get; set; }
        [Required]
        public string Description { get; set; }

        [InverseProperty("Bodega")]
        public virtual ICollection<BodegaProduct> BodegaProduct { get; set; }
        [InverseProperty("Bodega")]
        public virtual ICollection<Kardex> Kardex { get; set; }
        [InverseProperty("Bodega")]
        public virtual ICollection<Purchase> Purchase { get; set; }
        [InverseProperty("Bodega")]
        public virtual ICollection<Sale> Sale { get; set; }
        [InverseProperty(nameof(Transfer.BidegaDestiny))]
        public virtual ICollection<Transfer> TransferBidegaDestiny { get; set; }
        [InverseProperty(nameof(Transfer.BodegaOrigin))]
        public virtual ICollection<Transfer> TransferBodegaOrigin { get; set; }
    }
}
