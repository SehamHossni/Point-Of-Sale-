using Dal.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
   public class ProductVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]

        public string Size { get; set; }
        [Required]
        public double Price { get; set; }


        public string Description { get; set; }

        public IFormFile? ImgFile { get; set; }
        public string? Img { get; set; }
        public virtual List<OrderProduct>? OrderProducts { get; set; }
    }
}
