using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        /*Here option is the connection string and the connection string will be passed to the 
          the base "option" parameter
        */

        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }
        // When we use DbSet on the Entity it allows us to query the entity and use some of the 
        // methods in the DbContext to retrive the data ve are looking for.
        public DbSet<Product> Products { get; set; }

        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }

        // We are overring this method which is in DbContext.
        // This method is responsible for creating migrations
        //StoreContext class is going to overide OnModelCreating method present in DbContext Class.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // the line below is permanant.
            base.OnModelCreating(modelBuilder);
            /* ApplyConfigurationsFromAssembly Applies configuration from all IEntityTypeConfiguration<TEntity> />
               instances that are defined in provided assembly.ProductConfiguration). 
            */
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // converting decimal into double.
            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));
                    foreach (var property in properties)
                    {
                        modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion<double>();
                    }

                }
            }
        }
    }
}