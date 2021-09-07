using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppCom.Models
{
    public partial class OutPutDetails
    {
        [Key]
        public int LineId { get; set; }
        public int OutputId { get; set; }
        public int ProductId { get; set; }
        [Required]
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int KardexId { get; set; }

        [ForeignKey(nameof(KardexId))]
        [InverseProperty("OutPutDetails")]
        public virtual Kardex Kardex { get; set; }
        [ForeignKey(nameof(ProductId))]
        [InverseProperty("OutPutDetails")]
        public virtual Product Product { get; set; }
    }
}
