using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.IRepositories;
using Talabat.Core.Specifications;
using Talabat.Repositry;
using Talabat.Repositry.Data;

namespace Talabat.Core
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreDbcontext _dbcontext;

        public GenericRepository(StoreDbcontext dbcontext) 
        {
            _dbcontext = dbcontext;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            //if (typeof(T)==typeof(Product))
            //    return (IEnumerable<T>) await _dbcontext.Set<Product>().Include(p=>p.ProductType).Include(p=>p.ProductBrand).ToListAsync();
            
            return await _dbcontext.Set<T>().ToListAsync(); 
        }


        public async Task<T> GetByIdAsync(int id)
        {
            //return await _dbcontext.Set<Product>().Where(p => p.Id == id).Include(p => p.ProductType).Include(p => p.ProductBrand);
            return await _dbcontext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpec(spec).ToListAsync();
        }

        public async Task<T> GetByIdWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpec(spec).FirstOrDefaultAsync(); 
        }

        public async Task<int> GetCountWithSpec(ISpecification<T> spec)
        {
            return await ApplySpec(spec).CountAsync();
        }
        private IQueryable<T> ApplySpec(ISpecification<T> spec)
        {
            return SpecificationEvalutor<T>.GetQuery(_dbcontext.Set<T>(), spec);
        }

        public async Task Add(T entity)
            =>await _dbcontext.Set<T>().AddAsync(entity);

        public void Update(T entity)
            =>_dbcontext.Set<T>().Update(entity);

        public void Delete(T entity)
            => _dbcontext.Set<T>().Remove(entity);
    }
}
