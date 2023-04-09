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

namespace AtSepete.Business.Concrete
{
    // hatalar ve data dönülmeye gerek olmayan tüm resultları kontrol et düzenle!!
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILoggerService _loggerService;
        //private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IUserRepository userRepository, IMapper mapper, ILoggerService loggerService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _loggerService = loggerService;
        }



        public async Task<IDataResult<CreateUserDto>> AddUserAsync(CreateUserDto entity)
        {
            try
            {
                if (entity == null)
                {
                    _loggerService.LogWarning(Messages.ObjectNotFound);
                    return new ErrorDataResult<CreateUserDto>(Messages.ObjectNotFound);
                }
                var currentUser = await _userRepository.GetByDefaultAsync(x => x.Email == entity.Email);
                if (currentUser is not null)
                {
                    _loggerService.LogWarning(Messages.AddFailAlreadyExists);
                    return new ErrorDataResult<CreateUserDto>(Messages.AddFailAlreadyExists);
                }
                entity.Password = await PasswordHashAsync(entity.Password);
                var userMap = _mapper.Map<CreateUserDto, User>(entity);
                await _userRepository.AddAsync(userMap);
                await _userRepository.SaveChangesAsync();

                _loggerService.LogInfo(Messages.AddUserFail);
                return new SuccessDataResult<CreateUserDto>(_mapper.Map<User, CreateUserDto>(userMap), Messages.AddUserSuccess);
            }
            catch (Exception)
            {
                _loggerService.LogError(Messages.AddUserFail);
                return new ErrorDataResult<CreateUserDto>(Messages.AddUserFail);

            }

        }

        #region resetPassword


        //var fromAddress = new MailAddress("i_am_hr@outlook.com");
        //var toAddress = new MailAddress(manager.Mail);
        //var Link = "Şifrenizi Oluşturmak İçin Linke Tıklayınız<a href= http://imhere.azurewebsites.net/Home/ResetPass/" + manager.Mail + ">Buraya Tıklayınız</a>.";

        //string resetPass = "Şifre Oluşturma Bağlantınız";
        //using (var smtp = new SmtpClient
        //{
        //    Host = "smtp-mail.outlook.com",
        //    /**/
        //    Port = 587,
        //    EnableSsl = true,
        //    DeliveryMethod = SmtpDeliveryMethod.Network,
        //    UseDefaultCredentials = false,

        //    Credentials = new NetworkCredential(fromAddress.Address, "ik-123456")
        //})
        //    try
        //    {
        //        using (var message = new MailMessage(fromAddress, toAddress) { Subject = resetPass, Body = Link, IsBodyHtml = true })
        //        {
        //            smtp.Send(message);
        //        }
        //        ViewBag.Success = "Mail Başarıyla Gönderildi.";
        //    }
        //    catch (Exception)
        //    {
        //        ViewBag.Unsuccess = "Mail Gönderiminde Hata Oluştu.";
        //        return View();
        //    }
        //_person.Add(manager);
        //return RedirectToAction("GetManager");

        #endregion


        public async Task<IDataResult<ChangePasswordDto>> ChangePasswordAsync(ChangePasswordDto changePasswordDto)
        {
            try
            {
                if (changePasswordDto == null)
                {
                    _loggerService.LogWarning(Messages.ObjectNotFound);
                    return new ErrorDataResult<ChangePasswordDto>(Messages.ObjectNotFound);
                }
                var currentUser = await _userRepository.GetByDefaultAsync(x => x.Email == changePasswordDto.Email);
                if (currentUser is null)
                {
                    _loggerService.LogWarning(Messages.UserNotFound);
                    return new ErrorDataResult<ChangePasswordDto>(Messages.UserNotFound);
                }
                changePasswordDto.CurrentPassword = await PasswordHashAsync(changePasswordDto.CurrentPassword);
                if (currentUser.Password != changePasswordDto.CurrentPassword)//burada hashlemeler şifre aynı olsa bile farklı oluyor hashlemeleri çözümleyip sitring şifreden kontrol edin!
                {
                    _loggerService.LogWarning(Messages.PasswordNotMatch);
                    return new ErrorDataResult<ChangePasswordDto>(Messages.PasswordNotMatch);
                    
                }
                changePasswordDto.NewPassword = await PasswordHashAsync(changePasswordDto.NewPassword);
                var userMap = _mapper.Map<ChangePasswordDto, User>(changePasswordDto);
                await _userRepository.UpdateAsync(userMap);
                await _userRepository.SaveChangesAsync();
                _loggerService.LogInfo(Messages.ChangePasswordSucces);
                return new SuccessDataResult<ChangePasswordDto>(_mapper.Map<User, ChangePasswordDto>(userMap), Messages.ChangePasswordSucces);
            }
            catch (Exception)
            {
                _loggerService.LogError(Messages.ChangePasswordFail);
                return new ErrorDataResult<ChangePasswordDto>(Messages.ChangePasswordFail);
            }

        }

        public async Task<IResult> CheckPasswordAsync(CheckPasswordDto checkPasswordDto)//login olurken şifre kontrolü
        {
            try
            {
                if (checkPasswordDto == null)
                {
                    _loggerService.LogWarning(Messages.ObjectNotFound);
                    return new ErrorResult(Messages.ObjectNotFound);
                }
                var currentUser = await _userRepository.GetByDefaultAsync(x => x.Email == checkPasswordDto.Email);
                if (currentUser is null)
                {
                    _loggerService.LogWarning(Messages.UserNotFound);
                    return new ErrorResult(Messages.UserNotFound);
                }
                var passhased = currentUser.Password;
                byte[] hashBytes = Convert.FromBase64String(passhased);
                byte[] salt = new byte[16];
                Array.Copy(hashBytes, 0, salt, 0, 16);
                var pbkdf2 = new Rfc2898DeriveBytes(checkPasswordDto.Password, salt, 100000);
                byte[] hash = pbkdf2.GetBytes(20);

                for (int i = 0; i < 20; i++)
                {
                    if (hashBytes[i + 16] != hash[i])
                    {
                        _loggerService.LogWarning(Messages.CheckPasswordNotValid);
                        return new ErrorResult(Messages.CheckPasswordNotValid);
                    }
                }
                _loggerService.LogInfo(Messages.CheckPasswordValid);
                return new SuccessResult(Messages.CheckPasswordValid);
            }
            catch (Exception)
            {
                _loggerService.LogError(Messages.CheckPasswordFail);
                return new ErrorDataResult<CheckPasswordDto>(Messages.CheckPasswordFail);
            }

        }
        public async Task<IDataResult<UserDto>> FindUserByEmailAsync(string email)// email ile user getirir
        {
            try
            {
                var user = await _userRepository.GetByDefaultAsync(x => x.Email == email);
                if (user is null)
                {
                    _loggerService.LogWarning(Messages.UserNotFound);
                    return new ErrorDataResult<UserDto>(Messages.UserNotFound);
                }
                var userDto = _mapper.Map<User, UserDto>(user);
                _loggerService.LogInfo(Messages.UserFoundSuccess);
                return new SuccessDataResult<UserDto>(userDto, Messages.UserFoundSuccess);
            }
            catch (Exception)
            {
                _loggerService.LogError(Messages.UserFoundFail);
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
                    _loggerService.LogWarning(Messages.UserNotFound);
                    return new ErrorDataResult<UserDto>(Messages.UserNotFound);
                }
                var userDto = _mapper.Map<User, UserDto>(user);

                _loggerService.LogInfo(Messages.UserFoundSuccess);
                return new SuccessDataResult<UserDto>(userDto, Messages.UserFoundSuccess);
            }
            catch (Exception)
            {
                _loggerService.LogError(Messages.UserFoundFail);
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
                    _loggerService.LogWarning(Messages.UserNotFound);
                    return new ErrorDataResult<List<UserDto>>(Messages.UsersNotFound);
                }
                var userDto = _mapper.Map<List<User>, List<UserDto>>(users.ToList());
                _loggerService.LogInfo(Messages.UserFoundSuccess);
                return new SuccessDataResult<List<UserDto>>(userDto, Messages.UsersFoundSuccess);
            }
            catch (Exception)
            {
                _loggerService.LogError(Messages.UserFoundFail);
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
                    _loggerService.LogWarning(Messages.UserNotFound);
                    return new ErrorDataResult<List<UserListDto>>(Messages.UsersNotFound);
                }
                var usersDto = _mapper.Map<IEnumerable<User>, List<UserListDto>>(users);
                _loggerService.LogInfo(Messages.UsersFoundSuccess);
                return new SuccessDataResult<List<UserListDto>>(usersDto, Messages.UsersFoundSuccess);
            }
            catch (Exception)
            {
                _loggerService.LogError(Messages.UserFoundFail);
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
                    _loggerService.LogWarning(Messages.UserNotFound);
                    return new ErrorDataResult<UserDto>(Messages.UserNotFound);
                }
                // Veritabanından kullanıcıyı bulmak için UserRepository kullanılır
                var user = await _userRepository.GetByIdAsync(Guid.Parse(userId));
                if (user == null)
                {
                    _loggerService.LogWarning(Messages.UsersNotFound);
                    return new ErrorDataResult<UserDto>(Messages.UserNotFound);
                }
                // UserDto nesnesine çevirme yapılır ve sonuç döndürülür
                var userDto = _mapper.Map<UserDto>(user);
                _loggerService.LogInfo(Messages.UserFoundSuccess);
                return new SuccessDataResult<UserDto>(userDto, Messages.UserFoundSuccess);
            }
            catch (Exception)
            {
                _loggerService.LogError(Messages.UserFoundFail);
                return new ErrorDataResult<UserDto>(Messages.UserFoundFail);
            }

        }

        public async Task<IResult> HardDeleteUserAsync(Guid id)//tamamen veritabanından siler
        {
            try
            {
                var user = await _userRepository.GetByIdActiveOrPassiveAsync(id);
                if (user is null)
                {
                    _loggerService.LogWarning(Messages.UserNotFound);
                    return new ErrorResult(Messages.UserNotFound);
                }
                await _userRepository.DeleteAsync(user);
                await _userRepository.SaveChangesAsync();
                _loggerService.LogInfo(Messages.DeleteSuccess);
                return new SuccessResult(Messages.DeleteSuccess);
            }
            catch (Exception)
            {
                _loggerService.LogError(Messages.DeleteFail);
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
                    _loggerService.LogWarning(Messages.UserNotFound);
                    return new ErrorResult(Messages.UserNotFound);
                }

                user.IsActive = false;
                user.DeletedDate = DateTime.Now;
                await _userRepository.UpdateAsync(user);
                await _userRepository.SaveChangesAsync();
                _loggerService.LogInfo(Messages.DeleteSuccess);
                return new SuccessResult(Messages.DeleteSuccess);
            }
            catch (Exception)
            {
                _loggerService.LogError(Messages.DeleteFail);
                return new ErrorResult(Messages.DeleteFail);
            }
        }

        protected async Task<string> PasswordHashAsync(string password)//şifre hashlemek için kullanırız sadece burada yazdık o yüzden protected
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            string savedPasswordHash = Convert.ToBase64String(hashBytes);
            _loggerService.LogInfo("PasswordHash is saved");
            return savedPasswordHash;
        }

        public Task<IResult> PasswordSignInAsync(UserDto user, string password, bool isPersistent, bool lockoutOnFailure)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> ResetPasswordAsync(UserDto user, string token, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> SignInAsync(UserDto user, bool isPersistent, string authenticationMethod = null)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> SignInAsync(UserDto user, AuthenticationProperties authenticationProperties, string authenticationMethod = null)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> SignOutAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IResult> UpdateRefreshToken(string refreshToken, UserDto userDto, DateTime accessTokenDate, int AddOnAccessTokenDate)
        {
            if (userDto is not null)
            {
                userDto.RefreshToken = refreshToken;
                userDto.RefreshTokenEndDate = accessTokenDate.AddMinutes(AddOnAccessTokenDate);
                var userMap = _mapper.Map<UserDto, User>(userDto);
                await _userRepository.UpdateAsync(userMap);
                await _userRepository.SaveChangesAsync();
                _loggerService.LogInfo($"Updated refresh token");
                return new SuccessResult();
            }
            _loggerService.LogError("update refresh token is fail");
            return new ErrorResult();
        }
        public async Task<IDataResult<UpdateUserDto>> UpdateUserAsync(Guid id, UpdateUserDto updateUserDto)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user is null)
                {
                    _loggerService.LogWarning(Messages.UserNotFound);
                    return new ErrorDataResult<UpdateUserDto>(Messages.UserNotFound);
                }

                if (user.Id != updateUserDto.Id)
                {
                    _loggerService.LogWarning(Messages.ObjectNotValid);
                    return new ErrorDataResult<UpdateUserDto>(Messages.ObjectNotValid);
                }
                
                var updateUser = _mapper.Map(updateUserDto, user);

                var result = await _userRepository.UpdateAsync(updateUser);
                await _userRepository.SaveChangesAsync();
                _loggerService.LogInfo(Messages.UpdateSuccess);
                return new SuccessDataResult<UpdateUserDto>(_mapper.Map<User, UpdateUserDto>(result), Messages.UpdateSuccess);


            }
            catch (Exception)
            {
                _loggerService.LogError(Messages.UpdateFail);
                return new ErrorDataResult<UpdateUserDto>(Messages.UpdateFail);
            }

        }
      
        #region Şifremi unuttum deneme
        //        [HttpPost]
        //        public async Task<IResult> SifremiUnuttum(string email)
        //        {
        //            //apideki send mail metoduna ui dan ailınan mail adresi gönderme işlemi yapılır
        //            sendMailForRestPass(email); //apinin restpass metoduna eamili gönder
        //        }
        //        public async Task sendMailForRestPass(string email)//apinin mail gönderme metodu
        //        {
        //            //burada bana ui tarafından mail adresi gelir ben token üretirim mail adresiyle tokenı karıştırır kullanıcıya mail atarım ki şiğfresini değiştirebilsimn
        //            var token = handlerToken();
        //            //kullanıcıya mail gönder(mailin url'inde token olacak kullanıcının mail adresi olacak)
        //            //burada url üzerinden nesne olarak email ve token gönderilecek.
        //        }
        //        [HttpGet]
        //        public async Task şifremiDeğiştir(string email, string token)//UI controller
        //        {
        //            ///kullanıcı mail adresindeki linke tıklar bana gelir ben sayfanın arka planında mail adresi ve token bilgilkerini tutarım ben bir ui controlleryım

        //        }
        //        [HttpPost]
        //        public async Task şifremiDeğiştir(string email, string token, string pass1, string pass2)//uı controller
        //        {//bende bir ui controlrıyım getten gelen bilgilerle birlikte kullanıcının yeni şifresini alrım

        //            ///buraya api ile bağlanmak içiçn bir kod yazarım kullanıcın yeni şifresini mail adresini token bilgisini api ye gönderirim ben bir ui controllerıyım
        //            using (var httpClient = new HttpClient())//api ile localhosttan bağlantı kurarak istekleri yönetir.
        //            {
        //                using (var answer = await httpClient.GetAsync("https://localhost:7286/AtSepeteApi/user/apiresetpass"))
        //                {
        //                    string apiAnswer = await
        ////post et apiye email token pnewwpass
        //answer.Content.ReadAsStringAsync();
        //                    products = JsonConvert.DeserializeObject<Product>(apiAnswer);
        //                }


        //            }
        //        }
        //        public async Task apicontroller(string email, string token, string pass1, string pass2)
        //        {
        //            _userservice.apiResetPass(string email, string token, string pass1, string pass2)// burada aşağıdaki servis metoduna bağlanacak
        //        }
        //        public async Task apiResetPass(string email, string token, string pass1, string pass2)//user apiservis metodu
        //        {
        //            ///ben bir api servisiyimapi contollerımdan gelen kullanıcı mail adresi token ı şifresini burda kontrol eder ve değiştiritim
        //            ///
        //            AppUser user = await _userManager.FindByEmailAsync(vm.Email);
        //            user.UpdateDate = DateTime.Now;
        //            string decodedtoken = HttpUtility.UrlDecode(vm.Token); // Encode edilen token decode ediliyor
        //            IdentityResult passwordChangeResult = await _userManager.ResetPasswordAsync(user, decodedtoken, vm.Password);
        //            if (passwordChangeResult.Succeeded)
        //            {
        //                await _userManager.UpdateSecurityStampAsync(user);
        //                TempData["Message"] = "Şifreniz Değiştirildi";
        //            }
        //            else
        //            {
        //                TempData["Message2"] = "Şifreniz Değiştirilemedi";
        //            }
        //            return RedirectToAction("Login");
        //        }
        #endregion


        //public async Task<IResult> ResetPasswordAsync(UserDto user, string token, string newPassword)//unutulan şifreyi sıfırlama
        //{
        //    var currentUser = await _userRepository.GetByDefaultAsync(x => x.Id == user.Id);

        //    string uniqueName = $"{Guid.NewGuid().ToString().ToLower()}";



        //    if (currentUser is not null)
        //    {
        //        var fromAddress = new MailAddress("i_am_hr@outlook.com");
        //        var toAddress = new MailAddress(user.Email);
        //        string encodedToken = HttpUtility.UrlEncode(token);
        //        var Link = $"Merhaba {currentUser.GetFullName()} , <br />" +
        //                    $"Şifreni yenilemek için linke tıklayabilirsin: " +
        //                    $"<a href='https://localhost:7286{Url.Action("NewPassword", "Login", new { email = currentUser.Email, token = encodedToken })}'>Şifre Yenile</a> <br />" +
        //                    $"İyi alışverişler dileriz.. <br /> <br />";
        //        //Email yerine Guid veya token ile değişiklik yapılacak
        //        string resetPass = "Şifre Yenileme Bağlantınız";
        //        using (var smtp = new SmtpClient
        //        {
        //            Host = "smtp-mail.outlook.com",
        //            /**/
        //            Port = 587,
        //            EnableSsl = true,
        //            DeliveryMethod = SmtpDeliveryMethod.Network,
        //            UseDefaultCredentials = false,


        //            Credentials = new NetworkCredential(fromAddress.Address, "ik-123456")
        //        }) ;
        //        //try
        //        //{
        //        //    {
        //        //        using (var message = new MailMessage(fromAddress, toAddress) { Subject = resetPass, Body = Link, IsBodyHtml = true })
        //        //        {
        //        //            smtp.Send(message);
        //        //        }
        //        //    }
        //        //    ViewBag.Sonuc = "Mail Başarıyla Gönderildi.";
        //        //}
        //        //catch (Exception)
        //        //{
        //        //    ViewBag.Sonuc = "Mail Gönderiminde Hata Oluştu.";
        //        //}
        //    }
        //    return new SuccessResult(Messages.DeleteSuccess);
        //}

        //public Task<IResult> SignInAsync(UserDto user, bool isPersistent, string authenticationMethod = null)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IResult> SignInAsync(UserDto user, AuthenticationProperties authenticationProperties, string authenticationMethod = null)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<IResult> SignOutAsyncA()
        //{
        //    await SignOutAsyncMethod(IdentityConstants.ApplicationScheme);

        //    return new SuccessResult(result);
        //}
        //protected async Task SignOutAsyncMethod(string scheme)
        //{
        //    var result = await _httpContextAccessor.HttpContext.AuthenticateAsync(scheme);
        //    if (result?.Principal != null)
        //    {

        //        var properties = result.Properties;
        //        var options = SchemeOptions.Get(scheme);
        //        if (options?.Events != null)
        //        {
        //            await options.Events.SigningOut(new AuthenticationProperties(properties), Context.Request.HttpContext);
        //            properties = new AuthenticationProperties(options.Cookie);
        //        }
        //        await Context.SignOutAsync(scheme, properties);
        //    }
        //}

        //public async Task<IResult> SoftDeleteUserAsync(Guid id)
        //{
        //    var user = await _userRepository.GetByIdAsync(id);
        //    if (user is null)
        //    {
        //        return new ErrorResult(Messages.ProductNotFound);
        //    }
        //    else
        //    {
        //        user.IsActive = false;
        //        user.DeletedDate = DateTime.Now;
        //        await _userRepository.UpdateAsync(user);
        //        await _userRepository.SaveChangesAsync();
        //        return new SuccessResult(Messages.DeleteSuccess);
        //    }
        //}
        /// <summary>
        ///login olan kullanıcı token oluşturduktan sonra Updaterefreshtoken metodu ile refresh token üretir
        /// </summary>
        /// <param name="refreshToken">token.RefreshToken</param>
        /// <param name="userDto">user</param>
        /// <param name="accessTokenDate">token.Expiration</param>
        /// <param name="AddOnAccessTokenDate">accessToken'a eklenecek süre yani refresh token süresi</param>
        /// <returns></returns>


      

      
    }
}
