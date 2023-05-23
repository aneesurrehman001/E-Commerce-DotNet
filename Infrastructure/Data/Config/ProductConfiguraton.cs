using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    /* IEntityTypeConfiguration this allows to write the configurations in a seperate class
       insted of under the OnModelCreating(ModelBuilder modelBuilder) which is in the context class
       Implement this interface, applying configuration for the entity in the Configure(EntityTypeBuilder<TEntity>)
       method, and then apply the configuration to the model using ApplyConfiguration<TEntity>(IEntityTypeConfiguration<TEntity>)
       in OnModelCreating(ModelBuilder).
     */
    public class ProductConfiguraton : IEntityTypeConfiguration<Product>
    {
        // Configure the Entity of type T (Product)
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).IsRequired();
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            builder.Property(p => p.PictureUrl).IsRequired();
            builder.HasOne(b => b.ProductBrand).WithMany().HasForeignKey(p => p.ProductBrandId);
            builder.HasOne(t => t.ProductType).WithMany().HasForeignKey(p => p.ProductTypeId);
        }
    }
}