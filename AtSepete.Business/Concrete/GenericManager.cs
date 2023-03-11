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
    public class GenericManager<Dto, T> : IGenericService<Dto, T> where T : Base
    {
        protected readonly IGenericRepository<T> _repository;
        protected readonly IMapper _mapper;

        public GenericManager(IGenericRepository<T> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<BaseResponse<bool>> ActivateAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<IEnumerable<Dto>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<Dto>> GetByDefaultAsync(Expression<Func<Dto, bool>> exp)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<Dto>> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<IEnumerable<Dto>>> GetDefaultAsync(Expression<Func<Dto, bool>> exp)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<bool>> RemoveAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<bool>> SetPassiveAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<bool>> SetPassiveAsync(Expression<Func<Dto, bool>> exp)
        {
            throw new NotImplementedException();
        }
        //public async Task<BaseResponse<bool>> ActivateAsync(Guid id)
        //{
        //    var tempEntity = await _repository.GetByIdAsync(id);
        //    if (tempEntity is null)
        //    {
        //        return new BaseResponse<bool>("NoData");
        //    }
        //    var result = await _repository.ActivateAsync(id);
        //    return new BaseResponse<bool>(result);

        //}

        //public async Task<BaseResponse<IEnumerable<Dto>>> GetAllAsync()
        //{
        //    return await _repository.GetAllAsync();
        //}

        //public async Task<BaseResponse<Dto>> GetByDefaultAsync(Expression<Func<Dto, bool>> exp)
        //{
        //    return await _repository.GetByDefaultAsync(exp);
        //}

        //public async Task<BaseResponse<Dto>> GetByIdAsync(Guid id)
        //{
        //    return await _repository.GetByIdAsync(id);

        //}

        //public async Task<BaseResponse<IEnumerable<Dto>>> GetDefaultAsync(Expression<Func<Dto, bool>> exp)
        //{
        //    return await _repository.GetDefaultAsync(exp);
        //}

        //public async Task<BaseResponse<bool>> SetPassiveAsync(Guid id)
        //{

        //    if (GetById(id) is null)
        //    {
        //        return false;
        //    }
        //    return await _repository.SetPassiveAsync(id);

        //}

        //public async Task<BaseResponse<bool>> RemoveAsync(Guid id)
        //{
        //    try
        //    {

        //        var entity = await _repository.GetByIdAsync(id);
        //        if (entity is null)
        //        {
        //            return new BaseResponse<bool>("NoData");
        //        }
        //        var result = await _repository.RemoveAsync(entity);
        //        return new BaseResponse<bool>(result);
        //    }
        //    catch (Exception)
        //    {

        //        return new BaseResponse<bool>("Deleting_Error");
        //    }


        //}

        //public async Task<BaseResponse<bool>> SetPassiveAsync(Expression<Func<Dto, bool>> exp)
        //{
        //    return await _repository.SetPassiveAsync(exp);
        //}




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
