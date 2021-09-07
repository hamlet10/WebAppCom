using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppCom.Models
{
    public partial class Rol
    {
        public Rol()
        {
            User = new HashSet<User>();
        }

        [Key]
        [Column("RolID")]
        public int RolId { get; set; }
        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        [InverseProperty("Rol")]
        public virtual ICollection<User> User { get; set; }
    }
}
