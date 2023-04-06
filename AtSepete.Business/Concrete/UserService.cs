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

namespace AtSepete.Business.Concrete
{
    // hatalar ve data dönülmeye gerek olmayan tüm resultları kontrol et düzenle!!
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }



        public async Task<IDataResult<CreateUserDto>> AddUserAsync(CreateUserDto entity)
        {
            try
            {
                if (entity == null)
                {
                    return new ErrorDataResult<CreateUserDto>(Messages.ObjectNotFound);
                }
                var currentUser = await _userRepository.GetByDefaultAsync(x => x.Email == entity.Email);
                if (currentUser is not null)
                {

                    return new ErrorDataResult<CreateUserDto>(Messages.AddFailAlreadyExists);
                }
                entity.Password = await PasswordHashAsync(entity.Password);
                var userMap = _mapper.Map<CreateUserDto, User>(entity);
                await _userRepository.AddAsync(userMap);
                await _userRepository.SaveChangesAsync();
                return new SuccessDataResult<CreateUserDto>(_mapper.Map<User, CreateUserDto>(userMap), Messages.AddUserSuccess);
            }
            catch (Exception)
            {
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
                    return new ErrorDataResult<ChangePasswordDto>(Messages.ObjectNotFound);
                }
                var currentUser = await _userRepository.GetByDefaultAsync(x => x.Email == changePasswordDto.Email);
                if (currentUser is null)
                {

                    return new ErrorDataResult<ChangePasswordDto>(Messages.UserNotFound);
                }
                changePasswordDto.NewPassword = await PasswordHashAsync(changePasswordDto.NewPassword);
                var userMap = _mapper.Map<ChangePasswordDto, User>(changePasswordDto);
                await _userRepository.UpdateAsync(userMap);
                await _userRepository.SaveChangesAsync();
                return new SuccessDataResult<ChangePasswordDto>(_mapper.Map<User, ChangePasswordDto>(userMap), Messages.ChangePasswordSucces);
            }
            catch (Exception)
            {

                return new ErrorDataResult<ChangePasswordDto>(Messages.ChangePasswordFail);
            }


        }

        public async Task<IResult> CheckPasswordAsync(CheckPasswordDto checkPasswordDto)
        {
            try
            {
                if (checkPasswordDto == null)
                {
                    return new ErrorResult(Messages.ObjectNotFound);
                }
                var currentUser = await _userRepository.GetByDefaultAsync(x => x.Email == checkPasswordDto.Email);
                if (currentUser is null)
                {

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
                        return new ErrorResult(Messages.CheckPasswordNotValid);
                }
                return new SuccessResult(Messages.CheckPasswordValid);
            }
            catch (Exception)
            {

                return new ErrorDataResult<CheckPasswordDto>(Messages.CheckPasswordFail);
            }

        }
        public async Task<IDataResult<UserDto>> FindUserByEmailAsync(string email)
        {
            try
            {
                var user = await _userRepository.GetByDefaultAsync(x => x.Email == email);
                if (user is null)
                {
                    return new ErrorDataResult<UserDto>(Messages.UserNotFound);
                }
                var userDto = _mapper.Map<User, UserDto>(user);
                return new SuccessDataResult<UserDto>(userDto, Messages.UserFoundSuccess);
            }
            catch (Exception)
            {

                return new ErrorDataResult<UserDto>(Messages.UserFoundFail);
            }


        }

        public async Task<IDataResult<UserDto>> FindUserByIdAsync(Guid id)
        {
            try
            {
                var user = await _userRepository.GetByDefaultAsync(x => x.Id == id);
                if (user is null)
                {
                    return new ErrorDataResult<UserDto>(Messages.UserNotFound);
                }
                var userDto = _mapper.Map<User, UserDto>(user);
                return new SuccessDataResult<UserDto>(userDto, Messages.UserFoundSuccess);
            }
            catch (Exception)
            {

                return new ErrorDataResult<UserDto>(Messages.UserFoundFail);
            }

        }

        public async Task<IDataResult<List<UserDto>>> FindUsersByRoleAsync(string roleName)
        {
            try
            {
                var users = await _userRepository.GetDefaultAsync(x => x.Role.ToString().Trim().ToLower() == roleName.Trim().ToLower());//buradaki rol int mi gelecek yoksa string mi dene !!
                if (!users.Any())
                {
                    return new ErrorDataResult<List<UserDto>>(Messages.UsersNotFound);
                }
                var userDto = _mapper.Map<List<User>, List<UserDto>>(users.ToList());
                return new SuccessDataResult<List<UserDto>>(userDto, Messages.UsersFoundSuccess);
            }
            catch (Exception)
            {

                return new ErrorDataResult<List<UserDto>>(Messages.UserFoundFail);
            }

        }

        public async Task<IDataResult<List<UserDto>>> GetAllUserAsync()
        {
            var users = await _userRepository.GetAllAsync();
            if (!users.Any())
            {
                return new ErrorDataResult<List<UserDto>>(Messages.UsersNotFound);
            }
            var usersDto = _mapper.Map<List<User>, List<UserDto>>(users.ToList());
            return new SuccessDataResult<List<UserDto>>(usersDto, Messages.UsersFoundSuccess);
        }

        public async Task<IDataResult<UserDto>> GetUserAsync(ClaimsPrincipal principal)
        {
            var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return new ErrorDataResult<UserDto>(Messages.UserNotFound);
            }

            // Veritabanından kullanıcıyı bulmak için UserRepository kullanılır
            var user = await _userRepository.GetByIdAsync(Guid.Parse(userId));
            if (user == null)
            {
                return new ErrorDataResult<UserDto>(Messages.UserNotFound);
            }

            // UserDto nesnesine çevirme yapılır ve sonuç döndürülür
            var userDto = _mapper.Map<UserDto>(user);
            return new SuccessDataResult<UserDto>(userDto, Messages.UserFoundSuccess);
        }

        public async Task<IResult> HardDeleteUserAsync(Guid id)
        {
            var user = await _userRepository.GetByIdActiveOrPassiveAsync(id);
            if (user is null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            await _userRepository.DeleteAsync(user);
            await _userRepository.SaveChangesAsync();

            return new SuccessResult(Messages.DeleteSuccess);
        }

        public async Task<string> PasswordHashAsync(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            string savedPasswordHash = Convert.ToBase64String(hashBytes);
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

        public async Task<IResult> ResetPasswordAsync(UserDto user, string token, string newPassword)
        {
            var currentUser = await _userRepository.GetByDefaultAsync(x => x.Id == user.Id);

            string uniqueName = $"{Guid.NewGuid().ToString().ToLower()}";



            if (currentUser is not null)
            {
                var fromAddress = new MailAddress("i_am_hr@outlook.com");
                var toAddress = new MailAddress(user.Email);
                var Link = "Şifrenizi Yenilemek İçin Linki Tıklayınız<a href= " + user.Email + ">Buraya Tıklayınız</a>.";
                //Email yerine Guid veya token ile değişiklik yapılacak
                string resetPass = "Şifre Yenileme Bağlantınız";
                using (var smtp = new SmtpClient
                {
                    Host = "smtp-mail.outlook.com",
                    /**/
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,


                    Credentials = new NetworkCredential(fromAddress.Address, "ik-123456")
                }) ;
                    //try
                    //{
                    //    {
                    //        using (var message = new MailMessage(fromAddress, toAddress) { Subject = resetPass, Body = Link, IsBodyHtml = true })
                    //        {
                    //            smtp.Send(message);
                    //        }
                    //    }
                    //    ViewBag.Sonuc = "Mail Başarıyla Gönderildi.";
                    //}
                    //catch (Exception)
                    //{
                    //    ViewBag.Sonuc = "Mail Gönderiminde Hata Oluştu.";
                    //}
            }
            return new SuccessResult(Messages.DeleteSuccess);
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
           
        }

        public async Task<IResult> SoftDeleteUserAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user is null)
            {
                return new ErrorResult(Messages.ProductNotFound);
            }
            else
            {
                user.IsActive = false;
                user.DeletedDate = DateTime.Now;
                await _userRepository.UpdateAsync(user);
                await _userRepository.SaveChangesAsync();
                return new SuccessResult(Messages.DeleteSuccess);
            }
        }

        public Task UpdateRefreshToken(string refreshToken, UserDto userDto, DateTime accessTokenDate, int AddOnAccessTokenDate)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<UserDto>> UpdateUserAsync(Guid id, UserDto userDto)
        {
            throw new NotImplementedException();
        }


        //public IActionResult CreateUser()
        //{
        //    CreatePerson createManager = new CreatePerson();
        //    createManager.Companies = _company.GetAll();
        //    createManager.Person = new Person();
        //    return View(createManager);
        //}

        //[HttpPost]
        //public IActionResult CreateManager(CreatePerson createPerson)
        //{
        //    var manager = createPerson.Person;
        //    var tryMail = _context.Employees.Where(x => x.Mail == createPerson.Person.Mail).Select(y => y.Mail).FirstOrDefault();
        //    if (tryMail != null)
        //    {
        //        ViewBag.AllReadyAddedd = "Mail adresi Sistemde Kayıtlı";
        //        return View();
        //    }
        //    else
        //    {
        //        if (manager.Photo == null)
        //        {
        //            if (manager.Gender == ENTITIES.Enums.GenderEnum.Erkek)
        //            {
        //                manager.PhotoName = "\\img\\3586f868_94a2_4fd2_8933_17cd1ff7605e.jpeg";
        //            }
        //            else
        //            {
        //                manager.PhotoName = "\\img\\3a172567_ce6c_4e78_964a_6860b74eeff1.jpeg";
        //            }
        //        }
        //        else
        //        {
        //            manager.PhotoName = SaveThePicture(manager.Photo);
        //        }
        //        manager.RoleEnum = RoleEnum.Yönetici;
        //        var fromAddress = new MailAddress("i_am_hr@outlook.com");
        //        var toAddress = new MailAddress(manager.Mail);
        //        var Link = "Şifrenizi Oluşturmak İçin Linke Tıklayınız<a href= http://imhere.azurewebsites.net/Home/ResetPass/" + manager.Mail + ">Buraya Tıklayınız</a>.";

        //        string resetPass = "Şifre Oluşturma Bağlantınız";
        //        using (var smtp = new SmtpClient
        //        {
        //            Host = "smtp-mail.outlook.com",
        //            /**/
        //            Port = 587,
        //            EnableSsl = true,
        //            DeliveryMethod = SmtpDeliveryMethod.Network,
        //            UseDefaultCredentials = false,

        //            Credentials = new NetworkCredential(fromAddress.Address, "ik-123456")
        //        })
        //            try
        //            {
        //                using (var message = new MailMessage(fromAddress, toAddress) { Subject = resetPass, Body = Link, IsBodyHtml = true })
        //                {
        //                    smtp.Send(message);
        //                }
        //                ViewBag.Success = "Mail Başarıyla Gönderildi.";
        //            }
        //            catch (Exception)
        //            {
        //                ViewBag.Unsuccess = "Mail Gönderiminde Hata Oluştu.";
        //                return View();
        //            }
        //        _person.Add(manager);
        //        return RedirectToAction("GetManager");
        //    }
        //}


        //[HttpGet]
        //public IActionResult CreatePackage()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult CreatePackage(Package package)
        //{
        //    if (package.StartDate < package.EndDate)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            if (package.Photo != null)
        //            {
        //                package.PhotoName = SaveThePicture(package.Photo);
        //            }
        //            else
        //            {
        //                ViewBag.PackageHata = "Fotoğraf Seçiniz";
        //                return View();
        //            }
        //            _package.Add(package);
        //            return RedirectToAction("GetPackage");
        //        }
        //    }
        //    else
        //    {
        //        ViewBag.Hata = "Başlangıç Tarihi Bitiş Tarihinden Büyük Olamaz.";
        //    }
        //    return View();
        //}

        //public IActionResult GetPackage()
        //{
        //    return View(_package.GetAll());
        //}


        //public IActionResult GetManager()
        //{
        //    return View(_person.GetAll());
        //}


        //[HttpGet]
        //public IActionResult CreateCompany()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult CreateCompany(CompanyPackage companies)
        //{
        //    var company = companies.Company;
        //    if (company.Photo == null)
        //        company.PhotoName = "\\img\\3586f868_94a2_4fd2_8933_17cd1ff7605e.jpeg";
        //    else
        //        company.PhotoName = SaveThePicture(company.Photo);
        //    _company.Add(company);
        //    return RedirectToAction("GetCompanies");
        //}

        //[HttpGet]
        //public IActionResult GetCompanies()
        //{
        //    return View(_company.GetAll());
        //}

        //private string SaveThePicture(IFormFile img)
        //{
        //    string filePath = Path.Combine(_env.WebRootPath, "img"); // ~/img

        //    string uniqueName = $"{Guid.NewGuid().ToString().Replace("-", "_").ToLower()}.{img.ContentType.Split('/')[1]}"; // Benzersiz isim oluşturma. İsimler Guid oluşturulacak. Küçük harf olacak ve - işaretleri yerine _ işareti olacak.

        //    string newFilePath = Path.Combine(filePath, uniqueName); //~/img/Dosyadı

        //    using (FileStream fs = new FileStream(newFilePath, FileMode.Create))
        //    {
        //        img.CopyTo(fs);
        //        return newFilePath.Substring(newFilePath.IndexOf("\\img\\")); // burada da dosya yolunun tammaı yerine \img\ kısmını substirng olarak alsın ve return etsin istiyorum. 
        //    }
        //}


    }
}
