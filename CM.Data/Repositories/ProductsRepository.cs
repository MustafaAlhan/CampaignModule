using CM.Core.Data;
using CM.Data.Context;
using CM.Data.Repositories.BaseRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CM.Data.Repositories
{
    public class ProductsRepository : EntityRepository<Products, CMDbContext>, IProductsRepository
    {
    }
}
