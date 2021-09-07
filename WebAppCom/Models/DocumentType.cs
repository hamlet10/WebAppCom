using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppCom.Models
{
    public partial class DocumentType
    {
        public DocumentType()
        {
            Client = new HashSet<Client>();
            Supplier = new HashSet<Supplier>();
        }

        [Key]
        public int DocumentTypeId { get; set; }
        [Required]
        public string Description { get; set; }

        [InverseProperty("DocumentType")]
        public virtual ICollection<Client> Client { get; set; }
        [InverseProperty("DocumentType")]
        public virtual ICollection<Supplier> Supplier { get; set; }
    }
}
