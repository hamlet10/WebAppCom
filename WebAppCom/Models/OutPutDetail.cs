using System;
using System.Collections.Generic;

#nullable disable

namespace WebAppCom.Models
{
    public partial class OutPutDetail
    {
        public int LineId { get; set; }
        public int OutputId { get; set; }
        public int ProductId { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int KardexId { get; set; }

        public virtual Kardex Kardex { get; set; }
        public virtual Product Product { get; set; }
    }
}
