using Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication4.ReportItems;

namespace DAL.ReportItems
{
    class ExpensesReport
    {
        public List<Expense> Explist = new List<Expense>();
        public double totalExp {get; set;}
        public List<dataPoints> dataPoints = new List<dataPoints>();
        public List<string> Peroid = new List<string>() {"Day","Week","Month" };
        public string currentRep { get; set; }



    }
}
