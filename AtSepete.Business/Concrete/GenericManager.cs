using AtSepete.Business.Abstract;
using AtSepete.Entities.BaseMessage;
using AtSepete.Entities.BaseData;
using AtSepete.Repositories.Abstract;
using AtSepete.Repositories.Concrete;
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
    public abstract class GenericManager<Dto,T> : IGenericService<Dto,T> where T : Base
    {
        protected readonly IGenericRepository<T> _repository;
        protected readonly IMapper _mapper;

        public GenericManager(IGenericRepository<T> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<BaseResponse<bool>> ActivateAsync(Guid id)
        {
            var tempEntity = await _repository.GetByIdAsync(id);
            if (tempEntity is null)
            {
                return new BaseResponse<bool>("NoData");
            }
            tempEntity.IsActive= true;
            tempEntity.ModifiedDate = DateTime.Now;
            var result = await _repository.UpdateAsync(tempEntity);
            return new BaseResponse<bool>(result);

        }

        public async Task<BaseResponse<IEnumerable<Dto>>> GetAllAsync()
        { 
            var tempEntity = await _repository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<T>, IEnumerable<Dto>>(tempEntity);
            return new BaseResponse<IEnumerable<Dto>>(result); 
        }

   

        public async Task<BaseResponse<Dto>> GetByIdAsync(Guid id)
        {
            var tempEntity = await _repository.GetByIdAsync(id);
            var result = _mapper.Map<T, Dto>(tempEntity);
            return new BaseResponse<Dto>(result);

        }


        public async Task<BaseResponse<bool>> SetPassiveAsync(Guid id)
        {
            var tempEntity = await _repository.GetByIdAsync(id);
            if (tempEntity is null)
            {
                return new BaseResponse<bool>("NoData"); ;
            }
            tempEntity.IsActive=false;
            tempEntity.ModifiedDate= DateTime.Now;
            var result = await _repository.UpdateAsync(tempEntity);
            return new BaseResponse<bool>(result);


        }

        public async Task<BaseResponse<bool>> RemoveAsync(Guid id)
        {
            try
            {

                var entity = await _repository.GetByIdAsync(id);
                if (entity is null)
                {
                    return new BaseResponse<bool>("NoData");
                }
                var result = await _repository.RemoveAsync(entity);
                return new BaseResponse<bool>(result);
            }
            catch (Exception)
            {

                return new BaseResponse<bool>("Deleting_Error");
            }


        }

        //public async Task<BaseResponse<Dto>> GetByDefaultAsync(Expression<Func<T, bool>> exp)
        //{
        //    var tempEntity=await _repository.GetByDefaultAsync(exp);          
        //    var mapped=_mapper.Map<T,Dto>(tempEntity);
        //    return new BaseResponse<Dto>(mapped);
        //}

        //public async Task<BaseResponse<IEnumerable<Dto>>> GetDefaultAsync(Expression<Func<T, bool>> exp)
        //{
        //    var tempEntity=await _repository.GetDefaultAsync(exp);
        //    var mapped = _mapper.Map<IEnumerable<T>,IEnumerable<Dto>>(tempEntity);
        //    return new BaseResponse<IEnumerable<Dto>>(mapped);
        //}

        //public Task<BaseResponse<bool>> SetPassiveAsync(Expression<Func<T, bool>> exp)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
