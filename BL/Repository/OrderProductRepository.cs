using BLL.inteface;
using Dal.Context;
using Dal.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repository
{
    public class OrderProductRepository : IOrderProductRepository
    {
        private readonly EcommerceContext context;

        public OrderProductRepository(EcommerceContext context)
        {
            this.context = context;
        }

        public async Task<int> Create(int ProductId, int OrderId)
        {
            OrderProduct product = new OrderProduct()
            {
                OrderId = OrderId,
                ProductId = ProductId,
            };
            await context.OrderProducts.AddAsync(product);
            return await context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetProduct(int OrderId)
        {


            var result = await context.OrderProducts.Where(o => o.OrderId == OrderId).ToListAsync();
            List<Product> productNames = new List<Product>();
            foreach (var item in result)
            {

                var data = await context.Products.FirstOrDefaultAsync(p => p.Id == item.ProductId);
                productNames.Add(data);
            }
            return productNames;
        }
    }
}
