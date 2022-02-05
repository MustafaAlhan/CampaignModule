using CM.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CM.Application.Dto
{
    public class TotalAddedHourDto : IEntity
    {
        public int Id { get; set; }
        public int Hour { get; set; }
    }
}
