using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Entities
{
    public class OrderProduct: BaseEntity
    {
        [ForeignKey("Order")]

        public int OrderId { get; set; }
        [ForeignKey("Product")]

        public int ProductId { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }

    }
}
