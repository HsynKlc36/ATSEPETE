using AtSepete.Core.CoreInterfaces;
using AtSepete.Entities.BaseData;

using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AtSepete.DataAccess.Context;

namespace AtSepete.Core.GenericRepository
{
    public abstract class EFBaseRepository<T> : IAsyncDeleteableRepository<T>, IAsyncFindableRepository<T>, IAsyncInsertableRepository<T>, IAsyncOrderableRepository<T>, IAsyncQueryableRepository<T>, IAsyncTransactionRepository, IRepository, IAsyncRepository, IAsyncUpdateableRepository<T> where T : Base
    {
        protected readonly AtSepeteDbContext _context;
        protected readonly DbSet<T> _table;

        public EFBaseRepository(AtSepeteDbContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }
        public async Task<T> AddAsync(T entity)
        {
            var entry = await _table.AddAsync(entity);

            return entry.Entity;
        }

        public Task AddRangeAsync(IEnumerable<T> entities)
        {
            return _table.AddRangeAsync(entities);
        }

        public Task<bool> AnyAsync(Expression<Func<T, bool>>? expression = null)
        {
            return expression is null ? GetAllActives().AnyAsync() : GetAllActives().AnyAsync(expression);
        }

        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return _context.Database.BeginTransactionAsync(cancellationToken);
        }

        public Task<IExecutionStrategy> CreateExecutionStrategy()
        {
            return Task.FromResult(_context.Database.CreateExecutionStrategy());
        }

        public void Delete(T entity)
        {
            _table.Remove(entity);
        }

        public Task DeleteAsync(T entity)
        {
            return Task.FromResult(_table.Remove(entity));
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _table.ToListAsync();
        }

        public async Task<T?> GetByDefaultAsync(Expression<Func<T, bool>> exp)
        {
            return await _table.FirstOrDefaultAsync(exp);
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _table.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetDefaultAsync(Expression<Func<T, bool>> exp)
        {
            return await _table.Where(exp).ToListAsync();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var entry = await Task.FromResult(_table.Update(entity));
            return entry.Entity;
        }

        public async Task<IEnumerable<T>> Where(Expression<Func<T, bool>> exp)
        {
            return _table.Where(exp).AsQueryable();
        }
        protected IQueryable<T> GetAllActives()
        {
            return _table.Where(x => x.IsActive == true);
        }
    }
}
