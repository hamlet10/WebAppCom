using System;
using System.Collections.Generic;

#nullable disable

namespace WebAppCom.Models
{
    public partial class RepaymentClientDetail
    {
        public int LineId { get; set; }
        public int RepaymentClientId { get; set; }
        public int ProductId { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public int Quantity { get; set; }
        public int KardexId { get; set; }
        public int? PercentIva { get; set; }
        public int? DiscountRate { get; set; }

        public virtual Iva DiscountRateNavigation { get; set; }
        public virtual Kardex Kardex { get; set; }
        public virtual Iva PercentIvaNavigation { get; set; }
        public virtual Product Product { get; set; }
        public virtual RepaymentClient RepaymentClient { get; set; }
    }
}
