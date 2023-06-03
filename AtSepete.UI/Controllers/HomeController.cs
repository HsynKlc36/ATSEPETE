using AtSepete.Dtos.Dto.Categories;
using AtSepete.Dtos.Dto.Users;
using AtSepete.Entities.BaseData;
using AtSepete.Entities.Data;
using AtSepete.Repositories.Abstract;
using AtSepete.Results;
using AtSepete.Results.Concrete;
using AtSepete.UI.ApiResponses.CategoryApiResponse;
using AtSepete.UI.Areas.Admin.Controllers;
using AtSepete.UI.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NToastNotify;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;

namespace AtSepete.UI.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IMapper _mapper;
        public HomeController(IMapper mapper,IToastNotification toastNotification,IConfiguration configuration):base(toastNotification, configuration)
        {
            _mapper = mapper;
        }

        //[Authorize(Roles ="Admin,Customer")] 
        [HttpGet]
        public async Task<IActionResult> Index()
        {           
           return View();
        }
        [HttpGet]
        //[Authorize(Roles = "Customer,Admin")]
        public IActionResult Privacy()
        {
            return View();
        }

  
    }

}
