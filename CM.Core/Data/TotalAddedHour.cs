using System;
using System.Collections.Generic;
using System.Text;

namespace CM.Core.Data
{
    public class TotalAddedHour : IEntity
    {
        public int Id { get; set; }
        public int Hour { get; set; }
    }
}
