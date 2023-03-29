using Dal.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
    public class ExpenseVM
    {
        public int ID { get; set; }


        [Required]
        public string Name { get; set; }


        [Required]
        public int Amount { get; set; }


        [Required]
        public DateTime Date { get; set; } = DateTime.Now;

        public string? Notes { get; set; }
    }
}
