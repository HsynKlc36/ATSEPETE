using AtSepete.Business.Abstract;
using AtSepete.Dtos.Dto;
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

namespace AtSepete.Business.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public Task<IDataResult<UserDto>> AddToRoleAsync(UserDto user, string role)
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<CreateUserDto>> AddUserAsync(CreateUserDto entity)
        {
            if (entity == null)
            {
                return new ErrorDataResult<CreateUserDto>(Messages.ObjectNotValid);
            }
            var currentUser = await _userRepository.GetByDefaultAsync(x => x.Email == entity.Email);
            if (currentUser is null)
            {
                var userMap = _mapper.Map<CreateUserDto, User>(entity);
                await _userRepository.AddAsync(userMap);
                await _userRepository.SaveChangesAsync();
            }
            else
            {
                return new ErrorDataResult<CreateUserDto>(Messages.AddFailAlreadyExists);
            }

            else
            {
             
                else
                {
                    manager.PhotoName = SaveThePicture(manager.Photo);
                }
                manager.RoleEnum = RoleEnum.Yönetici;
                var fromAddress = new MailAddress("i_am_hr@outlook.com");
                var toAddress = new MailAddress(manager.Mail);
                var Link = "Şifrenizi Oluşturmak İçin Linke Tıklayınız<a href= http://imhere.azurewebsites.net/Home/ResetPass/" + manager.Mail + ">Buraya Tıklayınız</a>.";

                string resetPass = "Şifre Oluşturma Bağlantınız";
                using (var smtp = new SmtpClient
                {
                    Host = "smtp-mail.outlook.com",
                    /**/
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,

                    Credentials = new NetworkCredential(fromAddress.Address, "ik-123456")
                })
                    try
                    {
                        using (var message = new MailMessage(fromAddress, toAddress) { Subject = resetPass, Body = Link, IsBodyHtml = true })
                        {
                            smtp.Send(message);
                        }
                        ViewBag.Success = "Mail Başarıyla Gönderildi.";
                    }
                    catch (Exception)
                    {
                        ViewBag.Unsuccess = "Mail Gönderiminde Hata Oluştu.";
                        return View();
                    }
                _person.Add(manager);
                return RedirectToAction("GetManager");
            }

            public Task<Results.IResult> ChangePasswordAsync(UserDto user, string currentPassword, string newPassword)
            {
                throw new NotImplementedException();
            }

            public Task<Results.IResult> CheckPasswordAsync(UserDto user, string password)
            {
                throw new NotImplementedException();
            }

            public Task<IDataResult<UserDto>> FindUserByEmailAsync(string email)
            {
                throw new NotImplementedException();
            }

            public Task<IDataResult<UserDto>> FindUserByIdAsync(Guid id)
            {
                throw new NotImplementedException();
            }

            public Task<IDataResult<List<UserDto>>> FindUsersByRoleAsync(string roleName)
            {
                throw new NotImplementedException();
            }

            public Task<IDataResult<List<UserDto>>> GetAllUserAsync()
            {
                throw new NotImplementedException();
            }

            public Task<UserDto> GetUserAsync(ClaimsPrincipal principal)
            {
                throw new NotImplementedException();
            }

            public Task<Results.IResult> HardDeleteUserAsync(Guid id)
            {
                throw new NotImplementedException();
            }

            public Task<Results.IResult> PasswordSignInAsync(UserDto user, string password, bool isPersistent, bool lockoutOnFailure)
            {
                throw new NotImplementedException();
            }

            public Task<Results.IResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
            {
                throw new NotImplementedException();
            }

            public Task<Results.IResult> ResetPasswordAsync(UserDto user, string token, string newPassword)
            {
                throw new NotImplementedException();
            }

            public Task<Results.IResult> SignInAsync(UserDto user, bool isPersistent, string authenticationMethod = null)
            {
                throw new NotImplementedException();
            }

            public Task<Results.IResult> SignInAsync(UserDto user, AuthenticationProperties authenticationProperties, string authenticationMethod = null)
            {
                throw new NotImplementedException();
            }

            public Task<Results.IResult> SignOutAsync()
            {
                throw new NotImplementedException();
            }

            public Task<Results.IResult> SoftDeleteUserAsync(Guid id)
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
