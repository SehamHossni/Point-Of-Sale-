using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Entities
{
    public class Product: BaseEntity
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public double Price { get; set; }

        public string? Img { get; set; }
        public string Description { get; set; }

        public virtual List<OrderProduct>? OrderProducts { get; set; }
    }
}
