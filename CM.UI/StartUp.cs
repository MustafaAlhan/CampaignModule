using CM.Application.IService;
using CM.Application.Service;
using CM.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;


namespace CM.UI
{
    public static class Startup
    {
        public static IServiceProvider ServiceProvider;
        public static void ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<IOrdersRepository, OrdersRepository>();
            services.AddScoped<ICampaignsRepository, CampaignsRepository>();
            services.AddScoped<ITotalAddedHourRepository, TotalAddedHourRepository>();
            services.AddScoped<IOrdersService, OrdersService>();
            services.AddScoped<ICampaignsService, CampaignsService>();
            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<IOrdersService, OrdersService>();
            services.AddScoped<ICampaignsService, CampaignsService>();
            services.AddScoped<ITotalAddedHourAppService, TotalAddedHourAppService>();

            ServiceProvider = services.BuildServiceProvider();
        }

        public static void DisposeServices()
        {
            if (ServiceProvider == null)
            {
                return;
            }
            if (ServiceProvider is IDisposable)
            {
                ((IDisposable)ServiceProvider).Dispose();
            }
        }

    }
}
