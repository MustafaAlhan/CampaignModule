using System;
using System.Collections.Generic;
using System.Text;

namespace CM.Application.Dto
{
    public class ProductsDto
    {
        public string ProductCode { get; set; }
        public decimal Price { get; set; }
        public int StartStock { get; set; }
        public int CurrentStock { get; set; }
    }
}
