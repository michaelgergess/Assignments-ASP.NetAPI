
using E_Commerce.Application.Contract;
using E_Commerce.Context;
using E_Commerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace E_Commerce.Infrastructure
{
    public class ProductRepository : Repository<Product, int> , IProductRepository
    {
        public ProductRepository(DB_Context Context) : base(Context)
        {
            
        }
    }
}
