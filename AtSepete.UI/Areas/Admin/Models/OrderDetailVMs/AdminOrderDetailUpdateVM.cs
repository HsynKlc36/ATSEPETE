﻿using System.ComponentModel;

namespace AtSepete.UI.Areas.Admin.Models.OrderDetailVMs
{
    public class AdminOrderDetailUpdateVM
    {
        [DisplayName("Sipariş Detay Id")]
        public Guid Id { get; set; }
        [DisplayName("Sipariş Id")]
        public Guid OrderId { get; set; }
        [DisplayName("Ürün Id")]
        public Guid ProductId { get; set; }
        [DisplayName("Sipariş Ürün Adedi")]
        public int Amount { get; set; }
        [DisplayName("Sipariş Oluşturma Tarihi")]
        public DateTime? CreatedDate { get; set; }
        [DisplayName("Ürün Adı")]
        public string? ProductName { get; set; }
        [DisplayName("Market Adı")]
        public string? MarketName { get; set; }
        [DisplayName("Müşteri Adı")]
        public string? CustomerName { get; set; }
    }
}
