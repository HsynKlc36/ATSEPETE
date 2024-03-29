﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Dtos.Dto.Shop
{
    public class BestSellerProductListDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductTitle { get; set; }
        public string ProductQuantity { get; set; }
        public string ProductUnit { get; set; }
        public string ProductPhotoPath { get; set; }
    }
}
