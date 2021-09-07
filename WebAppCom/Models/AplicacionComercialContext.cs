using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebAppCom.Models
{
    public partial class AplicacionComercialContext : DbContext
    {
        public AplicacionComercialContext()
        {
        }

        public AplicacionComercialContext(DbContextOptions<AplicacionComercialContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bar> Bar { get; set; }
        public virtual DbSet<Bodega> Bodega { get; set; }
        public virtual DbSet<BodegaProduct> BodegaProduct { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Concept> Concept { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<DocumentType> DocumentType { get; set; }
        public virtual DbSet<ImagenPathArrs> ImagenPathArrs { get; set; }
        public virtual DbSet<Iva> Iva { get; set; }
        public virtual DbSet<Kardex> Kardex { get; set; }
        public virtual DbSet<Measure> Measure { get; set; }
        public virtual DbSet<OutPut> OutPut { get; set; }
        public virtual DbSet<OutPutDetails> OutPutDetails { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Purchase> Purchase { get; set; }
        public virtual DbSet<PurchaseDetails> PurchaseDetails { get; set; }
        public virtual DbSet<RepaymentClient> RepaymentClient { get; set; }
        public virtual DbSet<RepaymentClientDetails> RepaymentClientDetails { get; set; }
        public virtual DbSet<RepaymentSupplier> RepaymentSupplier { get; set; }
        public virtual DbSet<RepaymentSupplierDetails> RepaymentSupplierDetails { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Sale> Sale { get; set; }
        public virtual DbSet<SalesDetails> SalesDetails { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
        public virtual DbSet<Transfer> Transfer { get; set; }
        public virtual DbSet<TransferDetails> TransferDetails { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=TN011\\SQLEXPRESS;Database=AplicacionComercial;User=sa;Password=Kwdlp1989;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bar>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.Bar1 });

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Bar)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bar_Product");
            });

            modelBuilder.Entity<BodegaProduct>(entity =>
            {
                entity.HasKey(e => new { e.BodegaId, e.ProductId });

                entity.HasIndex(e => e.ProductId);

                entity.HasOne(d => d.Bodega)
                    .WithMany(p => p.BodegaProduct)
                    .HasForeignKey(d => d.BodegaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BodegaProduct_Bodega");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.BodegaProduct)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BodegaProduct_Product");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasIndex(e => new { e.DocumentTypeId, e.Document })
                    .HasName("IX_Client")
                    .IsUnique();

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.Client)
                    .HasForeignKey(d => d.DocumentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Client_DocumentType");
            });

            modelBuilder.Entity<ImagenPathArrs>(entity =>
            {
                entity.HasIndex(e => e.ProductId);
            });

            modelBuilder.Entity<Kardex>(entity =>
            {
                entity.HasIndex(e => e.BodegaId);

                entity.HasIndex(e => e.ProductId);

                entity.HasOne(d => d.Bodega)
                    .WithMany(p => p.Kardex)
                    .HasForeignKey(d => d.BodegaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Kardex_Bodega");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Kardex)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Kardex_Product");
            });

            modelBuilder.Entity<OutPut>(entity =>
            {
                entity.HasNoKey();

                entity.HasIndex(e => e.BodegaId);

                entity.HasIndex(e => e.ConceptId);

                entity.HasIndex(e => e.OutputId);

                entity.HasOne(d => d.Bodega)
                    .WithMany()
                    .HasForeignKey(d => d.BodegaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OutPut_Bodega");

                entity.HasOne(d => d.Concept)
                    .WithMany()
                    .HasForeignKey(d => d.ConceptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OutPut_Concept");

                entity.HasOne(d => d.Output)
                    .WithMany()
                    .HasForeignKey(d => d.OutputId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OutPut_OutPutDetails");
            });

            modelBuilder.Entity<OutPutDetails>(entity =>
            {
                entity.HasIndex(e => e.KardexId);

                entity.HasIndex(e => e.ProductId);

                entity.HasOne(d => d.Kardex)
                    .WithMany(p => p.OutPutDetails)
                    .HasForeignKey(d => d.KardexId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OutPutDetails_Kardex");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OutPutDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OutPutDetails_Product");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.DepartmentId);

                entity.HasIndex(e => e.Ivaid);

                entity.HasIndex(e => e.MeasureId);

                entity.Property(e => e.Measure).IsFixedLength();

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Department");

                entity.HasOne(d => d.Iva)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.Ivaid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_IVA");

                entity.HasOne(d => d.MeasureNavigation)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.MeasureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Measure");
            });

            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.HasIndex(e => e.BodegaId);

                entity.HasIndex(e => e.SupplierId);

                entity.HasOne(d => d.Bodega)
                    .WithMany(p => p.Purchase)
                    .HasForeignKey(d => d.BodegaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Purchase_Bodega");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Purchase)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Purchase_Supplier");
            });

            modelBuilder.Entity<PurchaseDetails>(entity =>
            {
                entity.HasIndex(e => e.DiscountRate);

                entity.HasIndex(e => e.KardexId);

                entity.HasIndex(e => e.PercentIva);

                entity.HasIndex(e => e.ProductId);

                entity.HasIndex(e => e.PurchaseId);

                entity.HasOne(d => d.DiscountRateNavigation)
                    .WithMany(p => p.PurchaseDetailsDiscountRateNavigation)
                    .HasForeignKey(d => d.DiscountRate)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PurchaseDetails_IVA1");

                entity.HasOne(d => d.Kardex)
                    .WithMany(p => p.PurchaseDetails)
                    .HasForeignKey(d => d.KardexId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PurchaseDetails_Kardex");

                entity.HasOne(d => d.PercentIvaNavigation)
                    .WithMany(p => p.PurchaseDetailsPercentIvaNavigation)
                    .HasForeignKey(d => d.PercentIva)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PurchaseDetails_IVA");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.PurchaseDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PurchaseDetails_Product");

                entity.HasOne(d => d.Purchase)
                    .WithMany(p => p.PurchaseDetails)
                    .HasForeignKey(d => d.PurchaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PurchaseDetails_Purchase");
            });

            modelBuilder.Entity<RepaymentClient>(entity =>
            {
                entity.HasIndex(e => e.SalesId);

                entity.HasOne(d => d.Sales)
                    .WithMany(p => p.RepaymentClient)
                    .HasForeignKey(d => d.SalesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RepaymentClient_Sale");
            });

            modelBuilder.Entity<RepaymentClientDetails>(entity =>
            {
                entity.HasIndex(e => e.DiscountRate);

                entity.HasIndex(e => e.KardexId);

                entity.HasIndex(e => e.PercentIva);

                entity.HasIndex(e => e.ProductId);

                entity.HasIndex(e => e.RepaymentClientId);

                entity.Property(e => e.LineId).ValueGeneratedNever();

                entity.HasOne(d => d.DiscountRateNavigation)
                    .WithMany(p => p.RepaymentClientDetailsDiscountRateNavigation)
                    .HasForeignKey(d => d.DiscountRate)
                    .HasConstraintName("FK_RepaymentClientDetails_IVA1");

                entity.HasOne(d => d.Kardex)
                    .WithMany(p => p.RepaymentClientDetails)
                    .HasForeignKey(d => d.KardexId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RepaymentClientDetails_Kardex");

                entity.HasOne(d => d.PercentIvaNavigation)
                    .WithMany(p => p.RepaymentClientDetailsPercentIvaNavigation)
                    .HasForeignKey(d => d.PercentIva)
                    .HasConstraintName("FK_RepaymentClientDetails_IVA");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.RepaymentClientDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RepaymentClientDetails_Product");

                entity.HasOne(d => d.RepaymentClient)
                    .WithMany(p => p.RepaymentClientDetails)
                    .HasForeignKey(d => d.RepaymentClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RepaymentClientDetails_RepaymentClient");
            });

            modelBuilder.Entity<RepaymentSupplier>(entity =>
            {
                entity.HasIndex(e => e.PurchaseId);

                entity.HasOne(d => d.Purchase)
                    .WithMany(p => p.RepaymentSupplier)
                    .HasForeignKey(d => d.PurchaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RepaymentSupplier_Purchase");
            });

            modelBuilder.Entity<RepaymentSupplierDetails>(entity =>
            {
                entity.HasIndex(e => e.DiscountRate);

                entity.HasIndex(e => e.KardexId);

                entity.HasIndex(e => e.PercentIva);

                entity.HasIndex(e => e.ProductId);

                entity.HasIndex(e => e.RepaymentSupplierId);

                entity.HasOne(d => d.DiscountRateNavigation)
                    .WithMany(p => p.RepaymentSupplierDetailsDiscountRateNavigation)
                    .HasForeignKey(d => d.DiscountRate)
                    .HasConstraintName("FK_RepaymentSupplierDetails_IVA1");

                entity.HasOne(d => d.Kardex)
                    .WithMany(p => p.RepaymentSupplierDetails)
                    .HasForeignKey(d => d.KardexId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RepaymentSupplierDetails_Kardex");

                entity.HasOne(d => d.PercentIvaNavigation)
                    .WithMany(p => p.RepaymentSupplierDetailsPercentIvaNavigation)
                    .HasForeignKey(d => d.PercentIva)
                    .HasConstraintName("FK_RepaymentSupplierDetails_IVA");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.RepaymentSupplierDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RepaymentSupplierDetails_Product");

                entity.HasOne(d => d.RepaymentSupplier)
                    .WithMany(p => p.RepaymentSupplierDetails)
                    .HasForeignKey(d => d.RepaymentSupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RepaymentSupplierDetails_RepaymentSupplier");
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasIndex(e => e.BodegaId);

                entity.HasIndex(e => e.ClientId);

                entity.Property(e => e.SalesId).ValueGeneratedNever();

                entity.HasOne(d => d.Bodega)
                    .WithMany(p => p.Sale)
                    .HasForeignKey(d => d.BodegaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sale_Bodega");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Sale)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sale_Client");
            });

            modelBuilder.Entity<SalesDetails>(entity =>
            {
                entity.HasNoKey();

                entity.HasIndex(e => e.DiscountRate);

                entity.HasIndex(e => e.LineId);

                entity.HasIndex(e => e.PercentIva);

                entity.HasIndex(e => e.ProductId);

                entity.HasOne(d => d.DiscountRateNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.DiscountRate)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SalesDetails_IVA1");

                entity.HasOne(d => d.Line)
                    .WithMany()
                    .HasForeignKey(d => d.LineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SalesDetails_Sale");

                entity.HasOne(d => d.PercentIvaNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.PercentIva)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SalesDetails_IVA");

                entity.HasOne(d => d.Product)
                    .WithMany()
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SalesDetails_Product");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasIndex(e => new { e.DocumentTypeId, e.DocumentNo })
                    .HasName("IX_Supplier")
                    .IsUnique();

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.Supplier)
                    .HasForeignKey(d => d.DocumentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Supplier_DocumentType");
            });

            modelBuilder.Entity<Transfer>(entity =>
            {
                entity.HasIndex(e => e.BidegaDestinyId);

                entity.HasIndex(e => e.BodegaOriginId);

                entity.HasOne(d => d.BidegaDestiny)
                    .WithMany(p => p.TransferBidegaDestiny)
                    .HasForeignKey(d => d.BidegaDestinyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transfer_Bodega1");

                entity.HasOne(d => d.BodegaOrigin)
                    .WithMany(p => p.TransferBodegaOrigin)
                    .HasForeignKey(d => d.BodegaOriginId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transfer_Bodega");
            });

            modelBuilder.Entity<TransferDetails>(entity =>
            {
                entity.HasNoKey();

                entity.HasIndex(e => e.KardexDestinyId);

                entity.HasIndex(e => e.KardexOriginId);

                entity.HasIndex(e => e.ProductId);

                entity.HasIndex(e => e.TransferId);

                entity.HasOne(d => d.KardexDestiny)
                    .WithMany()
                    .HasForeignKey(d => d.KardexDestinyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TransferDetails_Kardex3");

                entity.HasOne(d => d.KardexOrigin)
                    .WithMany()
                    .HasForeignKey(d => d.KardexOriginId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TransferDetails_Kardex2");

                entity.HasOne(d => d.Product)
                    .WithMany()
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TransferDetails_Product");

                entity.HasOne(d => d.Transfer)
                    .WithMany()
                    .HasForeignKey(d => d.TransferId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TransferDetails_Transfer");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.RolId);

                entity.Property(e => e.Identification).IsFixedLength();

                entity.HasOne(d => d.Rol)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.RolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Rol");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
