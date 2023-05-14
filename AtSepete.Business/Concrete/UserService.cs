using AtSepete.Business.Abstract;
using AtSepete.Entities.Data;
using AtSepete.Repositories.Abstract;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AtSepete.Results;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using AtSepete.Business.Constants;
using AtSepete.Results.Concrete;
using System.Security.Cryptography;
using AtSepete.Entities.Enums;
using AtSepete.Dtos.Dto.Users;
using IResult = AtSepete.Results.IResult;
using AtSepete.Repositories.Concrete;
using Microsoft.AspNetCore.Identity;
using AtSepete.Dtos.Dto.OrderDetails;
using System.Web;
using CloudinaryDotNet;
using Newtonsoft.Json;
using AtSepete.Business.Logger;
using Microsoft.AspNetCore.Authentication.Cookies;
using AtSepete.Business.JWT;
using SendGrid.Helpers.Mail;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.UI.Services;


namespace AtSepete.Business.Concrete
{
    // hatalar ve data dönülmeye gerek olmayan tüm resultları kontrol et düzenle!!
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILoggerService _loggerService;
        private readonly IHttpContextAccessor _httpContext;


        public UserService(IUserRepository userRepository, IMapper mapper, ILoggerService loggerService, IHttpContextAccessor httpContextAccessor = null)
        {

            _userRepository = userRepository;
            _mapper = mapper;
            _loggerService = loggerService;
            _httpContext = httpContextAccessor;

        }
        public async Task<IDataResult<CreateUserDto>> AddUserAsync(CreateUserDto entity)//kullanıcı ekler
        {

            try
            {
                if (entity == null)
                {
                    _loggerService.LogWarning(LogMessages.User_Object_Not_Valid);
                    return new ErrorDataResult<CreateUserDto>(Messages.ObjectNotValid);
                }
                var currentUser = await _userRepository.GetByDefaultAsync(x => x.Email == entity.Email);
                if (currentUser is not null)
                {
                    _loggerService.LogWarning(LogMessages.User_Add_Fail_Already_Exists);
                    return new ErrorDataResult<CreateUserDto>(Messages.AddFailAlreadyExists);
                }
                entity.Password = await PasswordHashAsync(entity.Password);
                var userMap = _mapper.Map<CreateUserDto, User>(entity);
                await _userRepository.AddAsync(userMap);
                await _userRepository.SaveChangesAsync();

                _loggerService.LogInfo(LogMessages.User_Added_Success);
                return new SuccessDataResult<CreateUserDto>(_mapper.Map<User, CreateUserDto>(userMap), Messages.AddUserSuccess);

            }
            catch (Exception)
            {
                _loggerService.LogError(LogMessages.User_Added_Failed);
                return new ErrorDataResult<CreateUserDto>(Messages.AddUserFail);
            }
        }
        public async Task<IDataResult<UserDto>> FindUserByEmailAsync(string email)// email ile user getirir
        {

            try
            {
                var user = await _userRepository.GetByDefaultAsync(x => x.Email == email);
                if (user is null)
                {
                    _loggerService.LogWarning(LogMessages.User_Object_Not_Found);
                    return new ErrorDataResult<UserDto>(Messages.UserNotFound);
                }
                var userDto = _mapper.Map<User, UserDto>(user);
                _loggerService.LogInfo(LogMessages.User_Object_Found_Success);
                return new SuccessDataResult<UserDto>(userDto, Messages.UserFoundSuccess);
            }
            catch (Exception)
            {
                _loggerService.LogError(LogMessages.User_Object_Found_Fail);
                return new ErrorDataResult<UserDto>(Messages.UserFoundFail);
            }
        }
        public async Task<IDataResult<UserDto>> FindUserByIdAsync(Guid id)//ıd ile user getirir
        {
            try
            {
                var user = await _userRepository.GetByDefaultAsync(x => x.Id == id);
                if (user is null)
                {
                    _loggerService.LogWarning(LogMessages.User_Object_Not_Found);
                    return new ErrorDataResult<UserDto>(Messages.UserNotFound);
                }
                var userDto = _mapper.Map<User, UserDto>(user);

                _loggerService.LogInfo(LogMessages.User_Object_Found_Success);
                return new SuccessDataResult<UserDto>(userDto, Messages.UserFoundSuccess);
            }
            catch (Exception)
            {
                _loggerService.LogError(LogMessages.User_Object_Found_Fail);
                return new ErrorDataResult<UserDto>(Messages.UserFoundFail);

            }

        }
        public async Task<IDataResult<List<UserDto>>> FindUsersByRoleAsync(string roleName)//role göre user getirir
        {
            try
            {
                var users = await _userRepository.GetDefaultAsync(x => x.Role.ToString().Trim().ToLower() == roleName.Trim().ToLower());//buradaki rol int mi gelecek yoksa string mi dene !!
                if (!users.Any())
                {
                    _loggerService.LogWarning(LogMessages.User_Object_Not_Found);
                    return new ErrorDataResult<List<UserDto>>(Messages.UsersNotFound);
                }
                var userDto = _mapper.Map<List<User>, List<UserDto>>(users.ToList());
                _loggerService.LogInfo(LogMessages.User_Object_Found_Success);
                return new SuccessDataResult<List<UserDto>>(userDto, Messages.UsersFoundSuccess);
            }
            catch (Exception)
            {
                _loggerService.LogError(LogMessages.User_Object_Found_Fail);
                return new ErrorDataResult<List<UserDto>>(Messages.UserFoundFail);
            }

        }
        public async Task<IDataResult<List<UserListDto>>> GetAllUserAsync()//tüm aktif userları getirir
        {
            try
            {
                IEnumerable<User> users = await _userRepository.GetAllAsync();
                if (!users.Any())
                {
                    _loggerService.LogWarning(LogMessages.User_Object_Not_Found);
                    return new ErrorDataResult<List<UserListDto>>(Messages.UsersNotFound);
                }
                var usersDto = _mapper.Map<IEnumerable<User>, List<UserListDto>>(users);
                _loggerService.LogInfo(LogMessages.User_Object_Found_Success);
                return new SuccessDataResult<List<UserListDto>>(usersDto, Messages.UsersFoundSuccess);
            }
            catch (Exception)
            {
                _loggerService.LogError(LogMessages.User_Object_Found_Fail);
                return new ErrorDataResult<List<UserListDto>>(Messages.UserFoundFail);
            }

        }
        public async Task<IDataResult<UserDto>> GetUserAsync(ClaimsPrincipal principal)//login olan kullanıcıyı getirir!!
        {
            try
            {
                //login Taskında login olan kullanıcıya claims ataması yapılmalı ki burası düzgün bir şekilde çalışşın!!
                var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                {
                    _loggerService.LogWarning(LogMessages.User_Object_Logged_Not_UserId);
                    return new ErrorDataResult<UserDto>(Messages.UserNotFound);
                }
                // Veritabanından kullanıcıyı bulmak için UserRepository kullanılır
                var user = await _userRepository.GetByIdAsync(Guid.Parse(userId));
                if (user == null)
                {
                    _loggerService.LogWarning(LogMessages.User_Object_Not_Found);
                    return new ErrorDataResult<UserDto>(Messages.UserNotFound);
                }
                // UserDto nesnesine çevirme yapılır ve sonuç döndürülür
                var userDto = _mapper.Map<UserDto>(user);
                _loggerService.LogInfo(LogMessages.User_Object_Found_Success);
                return new SuccessDataResult<UserDto>(userDto, Messages.UserFoundSuccess);
            }
            catch (Exception)
            {
                _loggerService.LogError(LogMessages.User_Object_Found_Fail);
                return new ErrorDataResult<UserDto>(Messages.UserFoundFail);
            }

        }
        public async Task<IDataResult<UpdateUserDto>> UpdateUserAsync(Guid id, UpdateUserDto updateUserDto)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user is null)
                {
                    _loggerService.LogWarning(LogMessages.User_Object_Not_Found);
                    return new ErrorDataResult<UpdateUserDto>(Messages.UserNotFound);
                }

                if (user.Id != updateUserDto.Id)
                {
                    _loggerService.LogWarning(LogMessages.User_Object_Not_Found);
                    return new ErrorDataResult<UpdateUserDto>(Messages.ObjectNotValid);
                }

                var updateUser = _mapper.Map(updateUserDto, user);

                var result = await _userRepository.UpdateAsync(updateUser);
                await _userRepository.SaveChangesAsync();
                _loggerService.LogInfo(LogMessages.User_Updated_Success);
                return new SuccessDataResult<UpdateUserDto>(_mapper.Map<User, UpdateUserDto>(result), Messages.UpdateSuccess);


            }
            catch (Exception)
            {
                _loggerService.LogError(LogMessages.User_Updated_Failed);
                return new ErrorDataResult<UpdateUserDto>(Messages.UpdateFail);
            }

        }
        public async Task<IResult> HardDeleteUserAsync(Guid id)//tamamen veritabanından siler
        {
            try
            {
                var user = await _userRepository.GetByIdActiveOrPassiveAsync(id);
                if (user is null)
                {
                    _loggerService.LogWarning(LogMessages.User_Object_Not_Found);
                    return new ErrorResult(Messages.UserNotFound);
                }
                await _userRepository.DeleteAsync(user);
                await _userRepository.SaveChangesAsync();
                _loggerService.LogInfo(LogMessages.User_Deleted_Success);
                return new SuccessResult(Messages.DeleteSuccess);
            }
            catch (Exception)
            {
                _loggerService.LogError(LogMessages.User_Deleted_Failed);
                return new ErrorResult(Messages.DeleteFail);
            }

        }
        public async Task<IResult> SoftDeleteUserAsync(Guid id)//sadece inaktife çeker kullanıcıyı
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user is null)
                {
                    _loggerService.LogWarning(LogMessages.User_Object_Not_Found);
                    return new ErrorResult(Messages.UserNotFound);
                }

                user.IsActive = false;
                user.DeletedDate = DateTime.Now;
                await _userRepository.UpdateAsync(user);
                await _userRepository.SaveChangesAsync();
                _loggerService.LogInfo(LogMessages.User_Deleted_Success);
                return new SuccessResult(Messages.DeleteSuccess);
            }
            catch (Exception)
            {
                _loggerService.LogError(LogMessages.User_Deleted_Failed);
                return new ErrorResult(Messages.DeleteFail);
            }
        }
        protected async Task<string> PasswordHashAsync(string password)//şifre hashlemek için kullanırız sadece burada yazdık o yüzden protected
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);//salt, her kullanıcı için farklıdır ve hash'in sonucunu değiştirir. Bu nedenle, aynı şifreyi kullanan iki kullanıcının hash'leri farklı olacaktır.
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            string savedPasswordHash = Convert.ToBase64String(hashBytes);
            _loggerService.LogInfo(LogMessages.User_PasswordHash_Success);
            return savedPasswordHash;
        }


    }
}
