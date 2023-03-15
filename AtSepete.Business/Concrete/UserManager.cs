using AtSepete.Business.Abstract;
using AtSepete.Dtos.Dto;
using AtSepete.Entities.BaseMessage;
using AtSepete.Entities.Data;
using AtSepete.Repositories.Abstract;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Business.Concrete
{
    public class UserManager : GenericManager<UserDto, User>, IUserService
    {
        private readonly IUserRepository _userRepository;


        public UserManager(IUserRepository userRepository,IGenericRepository<User> genericRepository,IMapper mapper):base(genericRepository,mapper)
        {
            _userRepository = userRepository;

        }

        public Task<BaseResponse<bool>> AddAsync(UserDto item)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<bool>> UpdateAsync(Guid id, UserDto item)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<bool>> UpdateAsync(IEnumerable<UserDto> items)
        {
            throw new NotImplementedException();
        }
    }
}
