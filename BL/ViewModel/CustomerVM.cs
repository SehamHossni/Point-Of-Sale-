using Dal.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
   public class CustomerVM
    {

        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Name max length 50")]
        [MinLength(2, ErrorMessage = "Name minimum length 2")]
        public string Name { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        public List<Order>? Order { get; set; }

    }
}
