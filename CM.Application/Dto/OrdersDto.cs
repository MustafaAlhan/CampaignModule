using System;
using System.Collections.Generic;
using System.Text;

namespace CM.Application.Dto
{
    public class OrdersDto
    {
        public int OrderId { get; set; }
        public string ProductCode { get; set; }
        public int? CampaignId { get; set; }
        public int Quantity { get; set; }
        public decimal CurentUnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
