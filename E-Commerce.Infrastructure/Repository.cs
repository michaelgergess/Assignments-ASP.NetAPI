using E_Commerce.Application.Contract;
using E_Commerce.Context;
using E_Commerce.Model;
using Microsoft.EntityFrameworkCore;



namespace E_Commerce.Infrastructure
{
    public class Repository<TEntity, Tid> : IRepository<TEntity, Tid> where TEntity : BaseClass2
    {
        private readonly DB_Context _Context;
        private readonly DbSet<TEntity> _Dbset;
        public Repository(DB_Context Context)
        {
            _Context = Context;
            _Dbset = _Context.Set<TEntity>();
        }
        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            return (await _Dbset.AddAsync(entity)).Entity;
        }

        public Task<TEntity> DeleteAsync(TEntity entity)
        {

            return Task.FromResult((_Dbset.Remove(entity)).Entity);
        }

        public Task<IQueryable<TEntity>> GetAllAsync()
        {
            return Task.FromResult(_Dbset.Select(s=>s));
        }

        public async Task<TEntity> GetByIdAsync(Tid id)
        {
            return await _Dbset.FindAsync( id);
        }

        public async Task<int> SaveChangesAsync()
        {
          return await _Context.SaveChangesAsync();
        }

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            return Task.FromResult( _Dbset.Update(entity).Entity);

        }
    }
}
