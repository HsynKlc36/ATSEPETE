﻿namespace AtSepete.UI.Areas.Customer.Models.ShopVMs
{
    public class CustomerShopProductDetailsVM
    {
        public Guid ProductId { get; set; }
        public Guid MarketId { get; set; }
        public string ProductName { get; set; }
        public string ProductQuantity { get; set; }
        public string ProductUnit { get; set; }
        public string ProductTitle { get; set; }
        public string ProductPhotoPath { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductStock { get; set; }
        public string CategoryName { get; set; }
        public string MarketName { get; set; }
    }
}
