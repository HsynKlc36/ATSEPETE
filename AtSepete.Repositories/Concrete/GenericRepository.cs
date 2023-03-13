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
        protected readonly AtSepeteDbContext _context;
        protected DbSet<T> _db;

        public GenericRepository(AtSepeteDbContext Context)
        {
            _context = Context;
            _db=_context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return _db.ToList();
        }


        public async Task<T> GetByIdAsync(Guid id)
        {
            return _db.Find(id);
            
        }
        public async Task<T> GetByDefaultAsync(Expression<Func<T, bool>> exp)
        {
            return  _db.FirstOrDefault(exp);
        }

        public async Task<IEnumerable<T>> GetDefaultAsync(Expression<Func<T, bool>> exp)
        {  
            return _db.Where(exp).ToList();
        }
        public async Task<bool> AddAsync(T item)
        {
            _db.Add(item);          
            return  await Save();
        }

        public async Task<bool> RemoveAsync(T item)
        {

            _db.Remove(item);
            return await Save();
        }

        public async Task<bool> UpdateAsync(T item)
        {
            try
            {
                item.ModifiedDate= DateTime.Now;
                _db.Update(item);
                return await Save();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateAsync(IEnumerable<T> items)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    int count = 0;

                    foreach (var item in items)
                    {
                        item.ModifiedDate= DateTime.Now;
                        bool result = await UpdateAsync(item);
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
        public  async Task<IEnumerable<T>> Where(Expression<Func<T, bool>> exp)
        {
            return _db.Where(exp).AsQueryable();
        }
        public async Task<bool> Save()
        {
            return _context.SaveChanges() > 0;
        }


        public async Task<bool> SetPassiveAsync(Expression<Func<T, bool>> exp)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    var items = await GetDefaultAsync(exp);
                    int count = 0;

                    foreach (var item in items)
                    {
                        item.IsActive = false;
                        item.DeletedDate = DateTime.Now;
                        bool result = await UpdateAsync(item);
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


        //public async Task<bool> ActivateAsync(Guid id)
        //{
        //    var item = await GetByIdAsync(id);
        //    _db.Update(item);
        //    return await Save();
        //}
        //public async Task<bool> SetPassiveAsync(Guid id)
        //{
        //    T item =await GetByIdAsync(id);
        //    item.IsActive = true;
        //    return await Save();
        //}

    }
}
