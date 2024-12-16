using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.IRepositories;
using Talabat.Repositry.Data;

namespace Talabat.Repositry
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbcontext _dbcontext;
        private Hashtable _Repo; 

        public UnitOfWork(StoreDbcontext dbcontext) 
        {
            _dbcontext = dbcontext;
            _Repo = new Hashtable();

        }
        public IGenericRepository<TEntity> Repositories<TEntity>() where TEntity : BaseEntity
        {
            var type = typeof(TEntity).Name;
            if(!_Repo.ContainsKey(type))
            {
                var repo = new GenericRepository<TEntity>(_dbcontext);
                _Repo.Add(type, repo);
            }
            return _Repo[type] as IGenericRepository<TEntity>; 
        }



        public async Task<int> Complet()
            => await _dbcontext.SaveChangesAsync();

        public async ValueTask DisposeAsync()
            =>await _dbcontext.DisposeAsync();
    }
}
