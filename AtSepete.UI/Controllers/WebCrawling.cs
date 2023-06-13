using Microsoft.AspNetCore.Mvc;
using HtmlAgilityPack;
using AtSepete.Entities.Data;
using AtSepete.Repositories.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtSepete.UI.Controllers
{
    public class WebCrawling : Controller
    {
        private readonly IProductRepository _productRepository;

        public WebCrawling(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<IActionResult> Index()
        {
            HtmlParser htmlParser = new HtmlParser();
            List<Product> products=htmlParser.ParseHtml();
            await _productRepository.AddAsync(products.FirstOrDefault());
             await _productRepository.SaveChangesAsync();
            return Ok();
        }
    }
}
public class HtmlParser
{
    public List<Product> ParseHtml()
    {
        var web = new HtmlWeb();
        var document = web.Load("https://www.trendyol.com/sr?q=lavanta+ya%C4%9F%C4%B1&qt=lavanta+ya%C4%9F%C4%B1&st=lavanta+ya%C4%9F%C4%B1&os=1&pi=1");
        var titleElements = document.DocumentNode.SelectNodes("//div[@class='prdct-desc-cntnr-ttl-w two-line-text']//span");

     
        var priceElements = document.DocumentNode.SelectNodes("//div[@class='prc-box-dscntd']"); 
       
        List<Product> products = new List<Product>();



        if (titleElements != null && priceElements != null)
        {
            int itemCount = Math.Min(titleElements.Count, priceElements.Count);
            for (int i = 0; i < 2; i++)
            {
                var title = titleElements[i].GetAttributeValue("title", "");
                var price = priceElements[i].InnerText.Trim();



                Product product = new Product
                {
                    Title = title + " " + titleElements[0].GetAttributeValue("title", ""),
                    ProductName = titleElements[1].GetAttributeValue("title", ""),
                    Description = price,
                    Barcode = "1111111111",
                    CategoryId =Guid.Parse("380e4880-5e9d-4418-d3c4-08db5a025aed"),
                    Quantity="5",
                    Unit="ml",
                    IsActive=false
                };

                products.Add(product);

                // Her bir ürün için 3 adım atladığımız için i'yi güncelliyoruz
                i += 2;
            }
        }

        return products;
     
    }
}
