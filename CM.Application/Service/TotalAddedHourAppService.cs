using CM.Application.IService;
using CM.Core.Data;
using CM.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CM.Application.Service
{
    public class TotalAddedHourAppService : ITotalAddedHourAppService
    {
        private readonly ITotalAddedHourRepository _totalAddedHourRepository;
        private readonly ICampaignsRepository _campaignsRepository;

        public TotalAddedHourAppService(ITotalAddedHourRepository totalAddedHourRepository, 
            ICampaignsRepository campaignsRepository)
        {
            _totalAddedHourRepository = totalAddedHourRepository;
            _campaignsRepository = campaignsRepository;
        }

        public int AddHour(int hour)
        {
            var item = _totalAddedHourRepository.GetList().FirstOrDefault();
            if(item == null)
            {
                item = new TotalAddedHour();
                item.Hour = hour;
                _totalAddedHourRepository.Add(item);
            }
            else
            {
                item.Hour = item.Hour + hour;
                _totalAddedHourRepository.Update(item);
            }

            //Changes the discount rates of active campaigns via: PMLIMIT x (1 - ((Total Sales Count) / Target)))
            var activeCampaignList = _campaignsRepository.GetList(x => x.EndDate > DateTime.Now.AddHours(item.Hour));
            foreach(var c in activeCampaignList)
            {
                c.CurrentDiscountRate = c.Limit * (1 - (Convert.ToDecimal(c.TotalSalesCount) / Convert.ToDecimal(c.TargetSalesCount)));
                _campaignsRepository.Update(c);
            }

            return item.Hour;
        }

        public int GetHour()
        {
            var item = _totalAddedHourRepository.GetList().FirstOrDefault();
            return item == null ? 0 : item.Hour;
        }
    }
}
