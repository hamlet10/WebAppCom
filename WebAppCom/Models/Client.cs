using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppCom.Models
{
    public partial class Client
    {
        public Client()
        {
            Sale = new HashSet<Sale>();
        }

        [Key]
        public int ClientId { get; set; }
        public int DocumentTypeId { get; set; }
        [Required]
        [StringLength(20)]
        public string Document { get; set; }
        [Required]
        public string TradeName { get; set; }
        [Required]
        public string ContactName { get; set; }
        [Required]
        public string ContactLastName { get; set; }
        public string Address { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Email { get; set; }
        [Column(TypeName = "text")]
        public string Note { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Anniversary { get; set; }

        [ForeignKey(nameof(DocumentTypeId))]
        [InverseProperty("Client")]
        public virtual DocumentType DocumentType { get; set; }
        [InverseProperty("Client")]
        public virtual ICollection<Sale> Sale { get; set; }
    }
}
