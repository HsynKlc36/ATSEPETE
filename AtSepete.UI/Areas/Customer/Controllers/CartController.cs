using AtSepete.Dtos.Dto.Carts;
using AtSepete.Dtos.Dto.JsonObjects;
using AtSepete.Dtos.Dto.Orders;
using AtSepete.Dtos.Dto.ProductMarkets;
using AtSepete.Dtos.Dto.Shop;
using AtSepete.UI.ApiResponses.OrderApiResponse;
using AtSepete.UI.ApiResponses.ProductMarketApiResponse;
using AtSepete.UI.ApiResponses.ShopApiResponse;
using AtSepete.UI.Areas.Admin.Models.ProductMarketVMs;
using AtSepete.UI.Areas.Customer.Models.ShopVMs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NToastNotify;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace AtSepete.UI.Areas.Customer.Controllers
{
    public class CartController : CustomerBaseController
    {
        private readonly IMapper _mapper;

        public CartController(IToastNotification toastNotification, IConfiguration configuration ,IMapper mapper) : base(toastNotification, configuration)
        {
            _mapper = mapper;
        }
        public async Task<IActionResult> ShoppingCartPage()
        {
            return View();
        }
        public async Task<IActionResult> CreditCardPaymentPage()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CustomerCreateOrder([FromQuery]string cartData)
        {
           
            List<JsonShoppingCartDto> jsonModel = JsonConvert.DeserializeObject<List<JsonShoppingCartDto>>(cartData);

            using (var httpClient = new HttpClient())
            {
                List<CreateShoppingCartDto> createShoppingCartDtos = _mapper.Map<List<JsonShoppingCartDto>, List<CreateShoppingCartDto>>(jsonModel);
                foreach (var shoppingCartDto in createShoppingCartDtos)
                {
                    shoppingCartDto.CustomerId = Guid.Parse(UserId);
                }
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserToken);
                StringContent content = new StringContent(JsonConvert.SerializeObject(createShoppingCartDtos), Encoding.UTF8, "application/Json");
                using (HttpResponseMessage response = await httpClient.PostAsync($"{ApiBaseUrl}/ShoppingCart/CreateOrderList", content))
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("RefreshTokenLogin", "Login", new { returnUrl = HttpContext.Request.Path, area = "" });
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    AddOrderListResponse addedOrderList = JsonConvert.DeserializeObject<AddOrderListResponse>(apiResponse);
                    if (addedOrderList.IsSuccess)
                    {
                        NotifySuccess(addedOrderList.Message);
                        return Json(addedOrderList.IsSuccess);
                    }
                    else
                    {
                        NotifyError(addedOrderList.Message);
                        return Json(addedOrderList.IsSuccess);
                    }
                   
                };
              
            }
           
        }
    }
}









