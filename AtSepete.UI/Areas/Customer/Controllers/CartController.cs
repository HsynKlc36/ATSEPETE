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
                    AddOrderListResponse addedOrderList = JsonConvert.DeserializeObject<AddOrderListResponse>(apiResponse);//burası patlıyor!!!
                    if (!addedOrderList.IsSuccess)
                    {
                        NotifyError(addedOrderList.Message);
                        return RedirectToAction("CreditCardPaymentPage");
                    }
                   
                };
              
            }

            return Json(jsonModel);
        }

     
    
    }
}
//public async Task<IActionResult> HomePageSideBarFilter([FromQuery] string sideBarFilter)
//{

//    using (var httpClient = new HttpClient())
//    {
//        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserToken);
//        using (HttpResponseMessage response = await httpClient.GetAsync($"{ApiBaseUrl}/Shop/ShopSideBarFilterList/{sideBarFilter}"))
//        {
//            if (response.StatusCode == HttpStatusCode.Unauthorized)
//            {
//                return RedirectToAction("RefreshTokenLogin", "Login", new { returnUrl = HttpContext.Request.Path, area = "" });
//            }
//            string apiResponse = await response.Content.ReadAsStringAsync();
//            ShopSideBarFilterListResponse shopSideBarFilterList = JsonConvert.DeserializeObject<ShopSideBarFilterListResponse>(apiResponse);

//            if (shopSideBarFilterList.IsSuccess)
//            {
//                var SideBarFilterproducts = _mapper.Map<List<ShopSideBarFilterListDto>, List<CustomerShopSideBarFilterListVM>>(shopSideBarFilterList.Data);
//                var serializedSideBarFilterProducts = JsonSerializer.Serialize(SideBarFilterproducts);
//                TempData["ShopSideBarFilterList"] = serializedSideBarFilterProducts;
//                NotifySuccess(shopSideBarFilterList.Message);

//                return Json(new { success = shopSideBarFilterList.IsSuccess });
//            }
//            else
//            {
//                NotifyError(shopSideBarFilterList.Message);

//                return Json(new { success = shopSideBarFilterList.IsSuccess });
//            }

//        };



//    }
//}





