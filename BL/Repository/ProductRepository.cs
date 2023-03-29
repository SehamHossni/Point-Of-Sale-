using BLL.inteface;
using Dal.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly EcommerceContext context;

        public ProductRepository(EcommerceContext context)
        {
            this.context = context;
        }
        //public int ProductInStock(int id)
        //{
        //  var data=  context.Products.Where(p => p.Id == id).Select(p=>p.Quantity).FirstOrDefault();
        //    return data;
        //}
    }
}
