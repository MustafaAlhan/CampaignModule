using CM.Application.AutoMapper;
using CM.Application.Dto;
using CM.Application.IService;
using CM.Core.Data;
using CM.Data.Repositories;
using System;
using System.Collections.Generic;


namespace CM.Application.Service
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productRepository;
        private readonly ICampaignsRepository _campaignsRepository;
        private readonly ITotalAddedHourAppService _totalAddedHourAppService;

        public ProductsService(IProductsRepository productRepository,
            ICampaignsRepository campaignsRepository,
            ITotalAddedHourAppService totalAddedHourAppService)
        {
            _productRepository = productRepository;
            _campaignsRepository = campaignsRepository;
            _totalAddedHourAppService = totalAddedHourAppService;
        }

        public void Add(ProductsDto dto)
        {
            var item = AutoMapperConfiguration.Instance.Map<ProductsDto, Products>(dto);
            _productRepository.Add(item);
        }

        public void Delete(string productId)
        {
            var item = _productRepository.Get(x => x.ProductCode == productId);
            _productRepository.Delete(item);
        }

        public ProductsDto Get(string productId)
        {
            var res = _productRepository.Get(x => x.ProductCode == productId);
            int addedHour = _totalAddedHourAppService.GetHour();
            var date = DateTime.Now.AddHours(addedHour);
            //Check if there is a campaign
            var campaign = _campaignsRepository.Get(x => x.ProductCode == res.ProductCode && x.EndDate > date);
            if (campaign != null)
            {
                res.Price = res.Price - (res.Price * (campaign.CurrentDiscountRate / 100));
            }
            return AutoMapperConfiguration.Instance.Map<Products, ProductsDto>(res);
        }

        public List<ProductsDto> GetAll()
        {
            var res = _productRepository.GetList();
            return AutoMapperConfiguration.Instance.Map<List<Products>, List<ProductsDto>>(res);
        }

        public void Update(ProductsDto dto)
        {
            var item = AutoMapperConfiguration.Instance.Map<ProductsDto, Products>(dto);
            _productRepository.Update(item);
        }
    }
}
