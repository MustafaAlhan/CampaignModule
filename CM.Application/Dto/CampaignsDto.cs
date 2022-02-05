using System;
using System.Collections.Generic;
using System.Text;

namespace CM.Application.Dto
{
    public class CampaignsDto
    {
        public int CampaignId { get; set; }
        public string CampaignName { get; set; }
        public string ProductCode { get; set; }
        public int Duration { get; set; }
        public decimal Limit { get; set; }
        public int TargetSalesCount { get; set; }
        public decimal CurrentDiscountRate { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalSalesCount { get; set; }
        public decimal Turnover { get; set; }
    }
}
