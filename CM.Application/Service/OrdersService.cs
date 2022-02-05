using CM.Application.Dto;
using CM.Application.IService;
using System;
using System.Collections.Generic;
using System.Text;
using CM.Data.Repositories;
using CM.Core.Data;
using CM.Application.AutoMapper;

namespace CM.Application.Service
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IProductsRepository _productsRepository;
        private readonly ICampaignsRepository _campaignsRepository;
        private readonly ITotalAddedHourAppService _totalAddedHourAppService;

        public OrdersService(IOrdersRepository ordersRepository,
            IProductsRepository productsRepository,
            ICampaignsRepository campaignsRepository,
            ITotalAddedHourAppService totalAddedHourAppService)
        {
            _ordersRepository = ordersRepository;
            _productsRepository = productsRepository;
            _campaignsRepository = campaignsRepository;
            _totalAddedHourAppService = totalAddedHourAppService;
        }

        public MessageDto Add(OrdersDto dto)
        {
            var m = new MessageDto();
            var item = AutoMapperConfiguration.Instance.Map<OrdersDto, Orders>(dto);
            int addedHour = _totalAddedHourAppService.GetHour();
            //Check if product exist
            var product = _productsRepository.Get(x => x.ProductCode == item.ProductCode);
            if (product == null)
            {
                m.IsSuccess = false;
                m.Message = "Error: Product Does Not Exist!";
                return m;
            }

            //Check if stock is enough
            if (product.CurrentStock < item.Quantity)
            {
                m.IsSuccess = false;
                m.Message = "Error: Product Does Not Have Enough Stock!";
                return m;
            }
            var date = DateTime.Now.AddHours(addedHour);
            //Check if there is a campaign
            var campaign = _campaignsRepository.Get(x => x.ProductCode == item.ProductCode && x.EndDate > date);
            if (campaign != null)
            {
                item.CampaignId = campaign.CampaignId;
                item.CurentUnitPrice = product.Price - (product.Price * (campaign.CurrentDiscountRate / 100));
                item.TotalPrice = item.CurentUnitPrice * item.Quantity;
                campaign.TotalSalesCount = campaign.TotalSalesCount + item.Quantity;
                campaign.Turnover = campaign.Turnover + item.TotalPrice;

                //Changes the discount rate of product in active campaign via: PMLIMIT x (1 - ((Total Sales Count) / Target)))
                campaign.CurrentDiscountRate = campaign.Limit * (1 - (Convert.ToDecimal(campaign.TotalSalesCount) / Convert.ToDecimal(campaign.TargetSalesCount)));

                _campaignsRepository.Update(campaign);
            }
            else
            {
                item.CampaignId = null;
                item.CurentUnitPrice = product.Price;
                item.TotalPrice = item.CurentUnitPrice * item.Quantity;
            }
            item.CreatedDate = item.CreatedDate.AddHours(addedHour);

            //Update Product Stock
            product.CurrentStock = product.CurrentStock - item.Quantity;
            _productsRepository.Update(product);

            m.IsSuccess = true;
            m.Message = $"Order is completed; Product Code : {item.ProductCode}, Unit Price : {item.CurentUnitPrice}, Total Price : {item.TotalPrice}";
            _ordersRepository.Add(item);

            return m;
        }

        public void Delete(int orderId)
        {
            var item = _ordersRepository.Get(x => x.OrderId == orderId);
            _ordersRepository.Delete(item);
        }

        public OrdersDto Get(int orderId)
        {
            var res = _ordersRepository.Get(x => x.OrderId == orderId);
            return AutoMapperConfiguration.Instance.Map<Orders, OrdersDto>(res);
        }

        public List<OrdersDto> GetAll()
        {
            var res = _ordersRepository.GetList();
            return AutoMapperConfiguration.Instance.Map<List<Orders>, List<OrdersDto>>(res);
        }

        public void Update(OrdersDto dto)
        {
            var item = AutoMapperConfiguration.Instance.Map<OrdersDto, Orders>(dto);
            _ordersRepository.Update(item);
        }
    }
}
