using System;
using System.Collections.Generic;

#nullable disable

namespace WebAppCom.Models
{
    public partial class TransferDetail
    {
        public int LineId { get; set; }
        public int TransferId { get; set; }
        public int ProductId { get; set; }
        public string Descripton { get; set; }
        public int KardexOriginId { get; set; }
        public int KardexDestinyId { get; set; }

        public virtual Kardex KardexDestiny { get; set; }
        public virtual Kardex KardexOrigin { get; set; }
        public virtual Product Product { get; set; }
        public virtual Transfer Transfer { get; set; }
    }
}
