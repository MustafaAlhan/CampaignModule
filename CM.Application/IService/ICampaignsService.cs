using CM.Application.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CM.Application.IService
{
    public interface ICampaignsService
    {
        CampaignsDto Get(string name);
        List<CampaignsDto> GetAll();
        MessageDto Add(CampaignsDto dto);
        void Delete(int campaignId);
        void Update(CampaignsDto dto);
    }
}
