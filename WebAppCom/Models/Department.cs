using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppCom.Models
{
    public partial class Department
    {
        public Department()
        {
            Product = new HashSet<Product>();
        }

        [Key]
        public int DepartmentId { get; set; }
        [Required]
        public string Description { get; set; }

        [InverseProperty("Department")]
        public virtual ICollection<Product> Product { get; set; }
    }
}
