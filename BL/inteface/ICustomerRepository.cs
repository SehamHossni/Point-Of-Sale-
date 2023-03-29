using Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.inteface
{
   public interface ICustomerRepository
    {
        public IEnumerable<Product> GetProductByCustomerId(int id);
    }
}
