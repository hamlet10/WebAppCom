using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppCom.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            Purchase = new HashSet<Purchase>();
        }

        [Key]
        public int SupplierId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public int DocumentTypeId { get; set; }
        [Required]
        [StringLength(20)]
        public string DocumentNo { get; set; }
        [Required]
        [StringLength(50)]
        public string ContactName { get; set; }
        [Required]
        [StringLength(50)]
        public string ContactLastName { get; set; }
        public string Address { get; set; }
        [StringLength(50)]
        public string Phone1 { get; set; }
        [StringLength(50)]
        public string Phone2 { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [Column(TypeName = "text")]
        public string Note { get; set; }

        [ForeignKey(nameof(DocumentTypeId))]
        [InverseProperty("Supplier")]
        public virtual DocumentType DocumentType { get; set; }
        [InverseProperty("Supplier")]
        public virtual ICollection<Purchase> Purchase { get; set; }
    }
}
