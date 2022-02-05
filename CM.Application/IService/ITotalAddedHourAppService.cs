using System;
using System.Collections.Generic;
using System.Text;

namespace CM.Application.IService
{
    public interface ITotalAddedHourAppService
    {
        int AddHour(int hour);
        int GetHour();
    }
}
