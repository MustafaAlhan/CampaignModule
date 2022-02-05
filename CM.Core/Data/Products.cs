using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CM.Core.Data
{
    public class Products : IEntity
    {
        [Key]
        public string ProductCode { get; set; }
        public decimal Price { get; set; }
        public int StartStock { get; set; }
        public int CurrentStock { get; set; }
    }
}
