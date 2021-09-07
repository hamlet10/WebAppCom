using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppCom.Models
{
    public partial class Product
    {
        public Product()
        {
            Bar = new HashSet<Bar>();
            BodegaProduct = new HashSet<BodegaProduct>();
            ImagenPathArrs = new HashSet<ImagenPathArrs>();
            Kardex = new HashSet<Kardex>();
            OutPutDetails = new HashSet<OutPutDetails>();
            PurchaseDetails = new HashSet<PurchaseDetails>();
            RepaymentClientDetails = new HashSet<RepaymentClientDetails>();
            RepaymentSupplierDetails = new HashSet<RepaymentSupplierDetails>();
        }

        [Key]
        public int ProductId { get; set; }
        [Required]
        public string Description { get; set; }
        public int DepartmentId { get; set; }
        [Column("IVAId")]
        public int Ivaid { get; set; }
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        [Column(TypeName = "text")]
        public string Note { get; set; }
        [Column("MeasureID")]
        public int MeasureId { get; set; }
        [Required]
        [StringLength(10)]
        public string Measure { get; set; }
        public string ImagePath { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        [InverseProperty("Product")]
        public virtual Department Department { get; set; }
        [ForeignKey(nameof(Ivaid))]
        [InverseProperty("Product")]
        public virtual Iva Iva { get; set; }
        [ForeignKey(nameof(MeasureId))]
        [InverseProperty("Product")]
        public virtual Measure MeasureNavigation { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<Bar> Bar { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<BodegaProduct> BodegaProduct { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<ImagenPathArrs> ImagenPathArrs { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<Kardex> Kardex { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<OutPutDetails> OutPutDetails { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<PurchaseDetails> PurchaseDetails { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<RepaymentClientDetails> RepaymentClientDetails { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<RepaymentSupplierDetails> RepaymentSupplierDetails { get; set; }
    }
}
