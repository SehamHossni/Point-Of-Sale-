using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Entities
{
    public class Order: BaseEntity
    {

        public int Id { get; set; }
        public DateTime OrderTime { get; set; } = DateTime.Now;
        [ForeignKey("Customer")]
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public double Price { get; set; }
        public virtual List<OrderProduct> OrderProducts { get; set; }

    }

    
}
