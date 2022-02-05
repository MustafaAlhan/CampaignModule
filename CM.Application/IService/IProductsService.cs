using CM.Application.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CM.Application.IService
{
    public interface IProductsService
    {
        ProductsDto Get(string productId);
        List<ProductsDto> GetAll();
        void Add(ProductsDto dto);
        void Delete(string productId);
        void Update(ProductsDto dto);
    }
}
