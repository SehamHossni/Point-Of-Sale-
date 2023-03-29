using Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.inteface
{
   public interface IOrderProductRepository
    {
        public Task<int> Create(int ProductId, int OrderId);
        public Task<List<Product>> GetProduct(int OrderId);
    }
}
