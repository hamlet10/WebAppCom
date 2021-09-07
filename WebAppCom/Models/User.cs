using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppCom.Models
{
    public partial class User
    {
        [Key]
        [Column("UserID")]
        public int UserId { get; set; }
        [Required]
        [StringLength(13)]
        public string Identification { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string LastName { get; set; }
        [Required]
        [StringLength(100)]
        public string Password { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LastModifierPass { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [Column("RolID")]
        public int RolId { get; set; }

        [ForeignKey(nameof(RolId))]
        [InverseProperty("User")]
        public virtual Rol Rol { get; set; }
    }
}
