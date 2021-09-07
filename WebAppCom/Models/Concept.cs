using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppCom.Models
{
    public partial class Concept
    {
        [Key]
        public int ConceptId { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
