using DAL.Entities;

namespace Dal.Entities
{
    public class Customer: BaseEntity
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public string Phone { get; set; }
        public string Address { get; set; }
        public List<Order> Order { get; set; }
    }
}