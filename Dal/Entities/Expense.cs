using DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Entities
{
    public class Expense :BaseEntity
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int Amount { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;


        [Column(TypeName = "nvarchar(80)")]
        public string? Notes { get; set; }

        
    }
}
