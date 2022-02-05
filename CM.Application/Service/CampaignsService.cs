using CM.Application.AutoMapper;
using CM.Application.Dto;
using CM.Application.IService;
using CM.Core.Data;
using CM.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CM.Application.Service
{
    public class CampaignsService : ICampaignsService
    {
        private readonly ICampaignsRepository _campaignsRepository;
        private readonly IProductsRepository _productsRepository;
        private readonly ITotalAddedHourAppService _totalAddedHourAppService;

        public CampaignsService(ICampaignsRepository campaignsRepository,
            IProductsRepository productsRepository,
            ITotalAddedHourAppService totalAddedHourAppService)
        {
            _campaignsRepository = campaignsRepository;
            _productsRepository = productsRepository;
            _totalAddedHourAppService = totalAddedHourAppService;
        }

        public MessageDto Add(CampaignsDto dto)
        {
            var item = AutoMapperConfiguration.Instance.Map<CampaignsDto, Campaigns>(dto);
            var product = _productsRepository.Get(x => x.ProductCode == dto.ProductCode);
            var m = new MessageDto();
            var addedHour = _totalAddedHourAppService.GetHour();
            //Check if product exist
            if (product == null)
            {
                m.IsSuccess = false;
                m.Message = "Error: Product Does not Exist!";
            }

            //Check if there is an active campaign for the product
            var existingCampaign = _campaignsRepository.Get(x => x.ProductCode == product.ProductCode && x.EndDate > DateTime.Now.AddHours(addedHour));
            if (existingCampaign != null)
            {
                m.IsSuccess = false;
                m.Message = $"Error: This product has an ongoing campaign with name: {existingCampaign.CampaignName}!";
            }
            item.Turnover = 0;
            item.TotalSalesCount = 0;
            item.BeginDate = item.BeginDate.AddHours(addedHour);
            item.EndDate = item.EndDate.AddHours(addedHour);
            _campaignsRepository.Add(item);
            m.IsSuccess = true;
            m.Message = $"Campaign created: Name: {item.CampaignName}, Product: {item.ProductCode}, Duration: {item.Duration}, Limit: {item.Limit}, Target Sales Count: {item.TargetSalesCount}.";
            return m;
        }

        public void Delete(int campaignId)
        {
            var item = _campaignsRepository.Get(x => x.CampaignId == campaignId);
            _campaignsRepository.Delete(item);
        }

        public CampaignsDto Get(string name)
        {
            var res = _campaignsRepository.Get(x => x.CampaignName == name);
            return AutoMapperConfiguration.Instance.Map<Campaigns, CampaignsDto>(res);
        }

        public List<CampaignsDto> GetAll()
        {
            var res = _campaignsRepository.GetList();
            return AutoMapperConfiguration.Instance.Map<List<Campaigns>, List<CampaignsDto>>(res);
        }

        public void Update(CampaignsDto dto)
        {
            var item = AutoMapperConfiguration.Instance.Map<CampaignsDto, Campaigns>(dto);
            _campaignsRepository.Update(item);
        }
    }
}
