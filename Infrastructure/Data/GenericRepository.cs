using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _context;
        public GenericRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            // here CountAsync() is a builtin method
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            // Set T to which ever type we want to get
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            /* 
                Here _context.Set<T> this is generic DbSet it brings all the data of the Entity type from database.
                In case of GetProductBrands() it is bringing the data from ProductBrands table
                from database.
             */
            var test = _context.Set<T>();

            /* 
                If you use ToList method, entity framework will fetch data from database and load the records into
                memory. when you filter it, you will filter the data from memory.
             */
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            // var tim = _context.Set<T>().AsQueryable();
            // var vim = _context.Set<T>().AsEnumerable();

            // e.g T gets replaced with products and it will be converted into Queryable by AsQueryeble
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }
    }
}