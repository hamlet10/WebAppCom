using System;
using System.Collections.Generic;

#nullable disable

namespace WebAppCom.Models
{
    public partial class SalesDetail
    {
        public int LineId { get; set; }
        public int SalesId { get; set; }
        public int ProductId { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public int Quantity { get; set; }
        public int KardexId { get; set; }
        public int PercentIva { get; set; }
        public int DiscountRate { get; set; }

        public virtual Iva DiscountRateNavigation { get; set; }
        public virtual Sale Line { get; set; }
        public virtual Iva PercentIvaNavigation { get; set; }
        public virtual Product Product { get; set; }
    }
}
