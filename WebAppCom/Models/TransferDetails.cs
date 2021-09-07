using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppCom.Models
{
    public partial class TransferDetails
    {
        public int LineId { get; set; }
        public int TransferId { get; set; }
        public int ProductId { get; set; }
        [Required]
        public string Descripton { get; set; }
        public int KardexOriginId { get; set; }
        public int KardexDestinyId { get; set; }

        [ForeignKey(nameof(KardexDestinyId))]
        public virtual Kardex KardexDestiny { get; set; }
        [ForeignKey(nameof(KardexOriginId))]
        public virtual Kardex KardexOrigin { get; set; }
        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }
        [ForeignKey(nameof(TransferId))]
        public virtual Transfer Transfer { get; set; }
    }
}
