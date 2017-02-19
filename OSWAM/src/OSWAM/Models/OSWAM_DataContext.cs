using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OSWAM
{
    public partial class OSWAM_DataContext : DbContext
    {
        public virtual DbSet<Product> Product { get; set; }

        // Unable to generate entity type for table 'dbo.Order'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(@"Server=tcp:oswam.database.windows.net,1433;Initial Catalog=OSWAM_Data;Persist Security Info=False;User ID=cap_admin;Password=Wham@05w;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        public OSWAM_DataContext(DbContextOptions<OSWAM_DataContext> options) 
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("char(10)");

                entity.Property(e => e.DimHeight).HasColumnType("decimal");

                entity.Property(e => e.DimLength).HasColumnType("decimal");

                entity.Property(e => e.DimWidth).HasColumnType("decimal");

                entity.Property(e => e.ItemName).HasColumnType("varchar(476)");

                entity.Property(e => e.Price).HasColumnType("smallmoney");

                entity.Property(e => e.Weight).HasColumnType("decimal");
            });
        }
    }
}