using BLL.inteface;
using Dal.Context;
using Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly EcommerceContext context;

        public CustomerRepository(EcommerceContext context)
        {
            this.context = context;
        }
        public IEnumerable<Product> GetProductByCustomerId(int id)
        {
            return context.Customers.Where(C => C.Id == id).Join(
                   context.OrderProducts,
                   C => C.Id,
                   O => O.Order.CustomerId,
                   (C, O) => new Product
                   {
                       Id = O.Product.Id,
                       Name = O.Product.Name,
                       Size = O.Product.Size,

                       Price = O.Product.Price,
                       Description = O.Product.Description,


                   });
        }
    }
}
