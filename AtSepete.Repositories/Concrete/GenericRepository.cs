using AtSepete.Entities.BaseData;
using AtSepete.Repositories.Abstract;
using AtSepete.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AtSepete.Repositories.Concrete
{
    public class GenericRepository<T>:IGenericRepository<T> where T:Base
    {
        private readonly AtSepeteDbContext _context;
        private DbSet<T> _db;

        public GenericRepository(AtSepeteDbContext Context)
        {
            _context = Context;
            _db=_context.Set<T>();
        }
        public async Task<bool> Activate(Guid id)
        {
            T item = await GetById(id);
            item.IsActive = true;
            return await Save();
        }


        public async Task<IEnumerable<T>> GetAll()
        {
            return _db.ToList();
        }

        public async Task<T> GetByDefault(Expression<Func<T, bool>> exp)
        {
            return  _db.FirstOrDefault(exp);
        }

        public async Task<T> GetById(Guid id)
        {
            return _db.Find(id);
        }

        public async Task<IEnumerable<T>> GetDefault(Expression<Func<T, bool>> exp)
        {
            return _db.Where(exp).ToList();
        }
        public async Task<IEnumerable<T>> GetAll(string[] includes)
        {
            var query = _db.AsQueryable();
            foreach (var include in includes)
                query = query.Include(include);
            return query.ToList();
        }
        public async Task<IEnumerable<T>> GetActive(string[] includes)
        {
            var query = _db.AsQueryable();
            foreach (var include in includes)
                query = query.Include(include);
            return query.Where(x => x.IsActive == true).ToList();
        }
        public async Task<bool> Add(T item)
        {
            _db.Add(item);
            return await Save();
        }


        public async Task<bool> SetPassive(Guid id)
        {
            T item =await GetById(id);
            item.IsActive = true;
            return await Save();
        }
        public async Task<bool> Remove(T item)
        {
            _db.Remove(item);
            return await Save();
        }

        public async Task<bool> SetPassive(Expression<Func<T, bool>> exp)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    var items =await GetDefault(exp);
                    int count = 0;

                    foreach (var item in items)
                    {
                        item.IsActive = false;
                        bool result = await Update(item);
                        if (result) count++;
                    }

                    if ( items.Count() == count)  ts.Complete();
                    else return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }




        public async Task<bool> Update(T item)
        {
            try
            {
                _db.Update(item);
                return await Save();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Update(IEnumerable<T> items)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    int count = 0;

                    foreach (var item in items)
                    {
                        bool result = await Update(item);
                        if (result) count++;
                    }

                    if (items.Count() == count) ts.Complete();
                    else return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> Save()
        {
            return _context.SaveChanges() > 0;
        }

    }
}
