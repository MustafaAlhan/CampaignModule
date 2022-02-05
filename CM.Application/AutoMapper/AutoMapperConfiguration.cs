using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CM.Application.Dto;
using CM.Core.Data;

namespace CM.Application.AutoMapper
{
    public class AutoMapperConfiguration
    {
        private static volatile IMapper instance;
        private static object syncRoot = new Object();

        public static IMapper Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new MapperConfiguration(cfg =>
                            {
                                //MapperConfigurations.
                                cfg.CreateMap<Products, ProductsDto>();
                                cfg.CreateMap<ProductsDto, Products>();

                                cfg.CreateMap<Campaigns, CampaignsDto>();
                                cfg.CreateMap<CampaignsDto, Campaigns>();

                                cfg.CreateMap<Orders, OrdersDto>();
                                cfg.CreateMap<OrdersDto, Orders>();

                                cfg.CreateMap<TotalAddedHour, TotalAddedHourDto>();
                                cfg.CreateMap<TotalAddedHourDto, TotalAddedHour>();

                            }).CreateMapper();
                    }
                }
                return instance;
            }
        }
    }
}
