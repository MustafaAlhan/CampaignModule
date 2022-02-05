using CM.Application.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CM.Application.IService
{
    public interface IOrdersService
    {
        OrdersDto Get(int orderId);
        List<OrdersDto> GetAll();
        MessageDto Add(OrdersDto dto);
        void Delete(int orderId);
        void Update(OrdersDto dto);
    }
}
