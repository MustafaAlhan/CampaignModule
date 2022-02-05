using CM.Application.Dto;
using CM.Application.IService;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace CM.UI
{
    public static class Engine
    {
        #region Products
        public static string CreateProduct(string productCode, decimal price, int stock)
        {
            var productService = Startup.ServiceProvider.GetService<IProductsService>();
            var productDTO = new ProductsDto();
            productDTO.ProductCode = productCode;
            productDTO.Price = price;
            productDTO.CurrentStock = stock;
            productDTO.StartStock = stock;

            productService.Add(productDTO);

            return $"Product is created; Product Code: {productDTO.ProductCode}, Price: {productDTO.Price}, Stock: {productDTO.StartStock}";
        }

        public static string GetProduct(string productCode)
        {
            var productService = Startup.ServiceProvider.GetService<IProductsService>();
            var productDTO = productService.Get(productCode);

            if (productDTO != null)
                return $"Product Code: {productDTO.ProductCode}, Price: {productDTO.Price}, Stock: {productDTO.CurrentStock}";
            else
                return "Error: Product Does not Exist!";
        }
        #endregion

        #region Orders
        public static string CreateOrder(string productCode, int quantity)
        {
            var orderService = Startup.ServiceProvider.GetService<IOrdersService>();
            var orderDTO = new OrdersDto();
            orderDTO.ProductCode = productCode;
            orderDTO.Quantity = quantity;
            orderDTO.CreatedDate = DateTime.Now;

            var res = orderService.Add(orderDTO);

            return res.Message;
        }
        #endregion

        #region Campaigns
        public static string CreateCampaign(string name, string productCode, int duration, decimal limit, int target)
        {
            if (limit <= 0 || limit >= 100)
                return "Error: Invalid Limit Value.";
            else if (duration <= 0)
                return "Error: Invalid Duration Value.";

            var campaignService = Startup.ServiceProvider.GetService<ICampaignsService>();
            var campaignDTO = new CampaignsDto();
            campaignDTO.ProductCode = productCode;
            campaignDTO.CampaignName = name;
            campaignDTO.Duration = duration;
            campaignDTO.Limit = limit;
            campaignDTO.TargetSalesCount = target;
            campaignDTO.CurrentDiscountRate = limit;
            campaignDTO.BeginDate = DateTime.Now;
            campaignDTO.EndDate = DateTime.Now.AddHours(duration);

            var res = campaignService.Add(campaignDTO);

            return res.Message;
        }

        public static string GetCampaignInfo(string name)
        {
            var campaignService = Startup.ServiceProvider.GetService<ICampaignsService>();
            var addedHourService = Startup.ServiceProvider.GetService<ITotalAddedHourAppService>();
            var campaignDTO = campaignService.Get(name);
            var addedHour = addedHourService.GetHour();
            if (campaignDTO == null)
            {
                return "Error: Campaign Not Found!";
            }
            else
            {
                var status = DateTime.Now.AddHours(addedHour) < campaignDTO.EndDate ? "Active" : "Ended";
                string avg = campaignDTO.TotalSalesCount == 0 ? "-" : (campaignDTO.Turnover / campaignDTO.TotalSalesCount).ToString();

                return $"Campaign {campaignDTO.CampaignName} info; Status: {status}, Target Sales: {campaignDTO.TargetSalesCount}, " +
                    $"Total Sales: {campaignDTO.TotalSalesCount}, Turnover: {campaignDTO.Turnover}, Average Item Price: {avg}";
            }
        }
        #endregion

        #region Time
        public static string AddHour(int hour)
        {
            var totalAddedHourService = Startup.ServiceProvider.GetService<ITotalAddedHourAppService>();
            var h = totalAddedHourService.AddHour(hour);

            return $"Time is {DateTime.Now.AddHours(h)}. ({h} Hours Added From Previous Commands)";
        }

        public static string GetTime()
        {
            var totalAddedHourService = Startup.ServiceProvider.GetService<ITotalAddedHourAppService>();
            var h = totalAddedHourService.GetHour();

            return $"Time is {DateTime.Now.AddHours(h)}. ({h} Hours Added From Previous Commands)";
        }
        #endregion
    }
}
