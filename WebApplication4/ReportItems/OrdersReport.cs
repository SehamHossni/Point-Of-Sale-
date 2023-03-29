using Dal.Entities;

namespace WebApplication4.ReportItems
{
    public class OrdersReport
    {
        public List<Order> Explist = new List<Order>();
        public double totalExp { get; set; }
        public List<dataPoints> dataPoints = new List<dataPoints>();
        public List<string> Peroid = new List<string>() { "Day", "Week", "Month" };
        public string currentRep { get; set; }
    }
}
