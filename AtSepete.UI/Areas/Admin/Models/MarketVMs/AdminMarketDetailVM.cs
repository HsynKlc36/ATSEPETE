﻿using System.ComponentModel;

namespace AtSepete.UI.Areas.Admin.Models.MarketVMs
{
    public class AdminMarketDetailVM
    {
         [DisplayName("Market Id")]
        public Guid Id { get; set; }
        [DisplayName("Market Adı")]
        public string MarketName { get; set; }
        [DisplayName("Market Açıklamsı")]
        public string Description { get; set; }
        [DisplayName("Market Adresi")]
        public string Adress { get; set; }
        [DisplayName("Market Numarası")]
        public string PhoneNumber { get; set; }
        [DisplayName("Market Tarihi")]
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
