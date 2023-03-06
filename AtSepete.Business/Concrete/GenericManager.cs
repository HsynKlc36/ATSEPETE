using AtSepete.Business.Abstract;
using AtSepete.Entities.BaseData;
using AtSepete.Repositories.Abstract;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Business.Concrete
{
    public class GenericManager<T>:IGenericService<T> where T : Base
    {
        private readonly IGenericRepository<T> _repository;

        public GenericManager(IGenericRepository<T> repository)
        {
            _repository = repository;
        }
        public async Task<bool> Activate(Guid id)
        {
            try
            {

            if ( GetById(id) == null)
            {
                return false;
            }
            return  await _repository.Activate(id);
            }
            catch (Exception)
            {

                throw new Exception("Aktivasyon yapılırken hata oluştu");
            }
        }

        public async Task<bool> Add(T item)
        {
            try
            {
                if (item == null)
                {
                    return false;
                }
                return await _repository.Add(item);
            }
            catch (Exception)
            {

                throw new Exception("Ekleme işlemi sırasında hata oluştu");
            }

        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<T> GetByDefault(Expression<Func<T, bool>> exp)
        {
            return await _repository.GetByDefault(exp);
        }

        public async Task<T> GetById(Guid id)
        {
            return await _repository.GetById(id);
           
        }

        public async Task<IEnumerable<T>> GetDefault(Expression<Func<T, bool>> exp)
        {
            return await _repository.GetDefault(exp);
        }

        public async Task<bool> SetPassive(Guid id)
        {
            
            if ( GetById(id) == null)
            {
                return false;
            }
            return await _repository.SetPassive(id);
        }

        public async Task<bool> Remove(T item)
        {
            try
            {

            if (item==null)
            {
                return false;
            }
            return  await _repository.Remove(item);
            }
            catch (Exception)
            {

                throw new Exception("Silme işlemi sırasında hata oluştu");
            }
        }

        public async Task<bool> SetPassive(Expression<Func<T, bool>> exp)
        {
            return await _repository.SetPassive(exp);
        }

        public async Task<bool> Update(T item)
        {
            if (item == null)
            {
                return false;
            }
            return await _repository.Update(item);
        }

        public async Task<bool> Update(IEnumerable<T> items)
        {
            if (items == null)
            {
                return false;
            }
            return await _repository.Update(items);
        }
        public async Task<IEnumerable<T>> GetActive(string[] includes)
        {
            return await _repository.GetActive(includes);
        }
        public async Task<IEnumerable<T>> GetAll(string[] includes)
        {
            return await _repository.GetAll(includes);
        }
    }
}
