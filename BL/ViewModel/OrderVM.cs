using Dal.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
    public class OrderVM
    {
        public int Id { get; set; }

        public DateTime OrderTime { get; set; } = DateTime.Now;

        public int? CustomerId { get; set; }
        public List<int> ProductId { get; set; }
        public double Price { get; set; }
        public Customer? Customer { get; set; }

        public virtual List<Product>? Products
        {
            get; set;
        }
        public virtual List<OrderProduct>? OrderProducts
        {
            get; set;
        }
    }
}
