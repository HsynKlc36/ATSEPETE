using AtSepete.Business.Abstract;
using AtSepete.Entities.Base;
using AtSepete.Entities.BaseData;
using AtSepete.Repositories.Abstract;
using AutoMapper;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Business.Concrete
{
    public class GenericManager<Dto,T>:IGenericService<Dto,T> where T : Base
    {
        private readonly IGenericRepository<T> _repository;
        private readonly IMapper _mapper;

        public GenericManager(IGenericRepository<T> repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<BaseResponse<bool>> Activate(Guid id)
        {
            var tempEntity=await _repository.GetByIdAsync(id);
            if (tempEntity==null)
            {
                return new BaseResponse<bool>("data bulunamadı");
            }
            var result = await _repository.ActivateAsync(id);
            return new BaseResponse<bool>(result);
            //try
            //{

            //if ( GetById(id) == null)
            //{
            //    return false;
            //}
            //return  await _repository.Activate(id);
            //}
            //catch (Exception)
            //{

            //    throw new Exception("Aktivasyon yapılırken hata oluştu");
            //}
        }

        public async Task<BaseResponse<bool>> Add(Dto item)
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

        public async Task<BaseResponse<IEnumerable<Dto>>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<BaseResponse<Dto>> GetByDefault(Expression<Func<Dto, bool>> exp)
        {
            return await _repository.GetByDefault(exp);
        }

        public async Task<BaseResponse<Dto>> GetById(Guid id)
        {
            return await _repository.GetById(id);
           
        }

        public async Task<BaseResponse<IEnumerable<Dto>>> GetDefault(Expression<Func<Dto, bool>> exp)
        {
            return await _repository.GetDefault(exp);
        }

        public async Task<BaseResponse<bool>> SetPassive(Guid id)
        {
            
            if ( GetById(id) == null)
            {
                return false;
            }
            return await _repository.SetPassive(id);
        }

        public async Task<BaseResponse<bool>> Remove(Dto item)
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

        public async Task<BaseResponse<bool>> SetPassive(Expression<Func<Dto, bool>> exp)
        {
            return await _repository.SetPassive(exp);
        }

        public async Task<BaseResponse<bool>> Update(Dto item)
        {
            if (item == null)
            {
                return false;
            }
            return await _repository.Update(item);
        }

        public async Task<BaseResponse<bool>> Update(IEnumerable<Dto> items)
        {
            if (items == null)
            {
                return false;
            }
            return await _repository.Update(items);
        }

      
        //public async Task<BaseResponse<IEnumerable<Dto>>> GetActive(string[] includes)
        //{
        //    return await _repository.GetActive(includes);
        //}
        //public async Task<BaseResponse<IEnumerable<Dto>>> GetAll(string[] includes)
        //{
        //    return await _repository.GetAll(includes);
        //}
    }
}
