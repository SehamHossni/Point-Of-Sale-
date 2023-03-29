using AutoMapper;
using BLL.inteface;
using BLL.ViewModel;
using Dal.Entities;
using DAL.ReportItems;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApplication4.ReportItems;

namespace WebApplication4.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReportsController : Controller
    {
        private readonly IGenericRepository<Expense> genericRepository;
        private readonly IMapper mapper;
        private readonly IExpenseRepository expenseRepository;
        private readonly IGenericRepository<Order> genericRepository2;
        private readonly IGenericRepository<Customer> genericRepository3;

        public ReportsController(IGenericRepository<Customer> genericRepository3,IGenericRepository<Order> genericRepository2, IGenericRepository<Expense> genericRepository, IMapper mapper, IExpenseRepository expenseRepository)
        {
            this.genericRepository = genericRepository;
            this.mapper = mapper;
            this.expenseRepository = expenseRepository;
            this.genericRepository2 = genericRepository2;
            this.genericRepository3 = genericRepository3;
        }


        public async Task<IActionResult> Expenses(string DropdownName = "Day")
        {
            ExpensesReport exp=new ExpensesReport();
            IEnumerable<Expense> expense = await genericRepository.MakeReport(DropdownName);
            exp.Explist = expense.ToList();
            double total = 0;
            for (int i = 0; i < exp.Explist.Count; i++)
            {
                total += exp.Explist[i].Amount;
            }

            exp.totalExp = total;
            foreach (var item in exp.Explist) {
                exp.dataPoints.Add(new dataPoints(item.Amount, item.Amount));
            }
            exp.currentRep = DropdownName;

            return View(exp);
        }


        public async Task<IActionResult> Orders(string DropdownName = "Day")
        {
            OrdersReport exp = new OrdersReport();
            IEnumerable<Order> expense = await genericRepository2.MakeReport(DropdownName);
            exp.Explist = expense.ToList();
            double total = 0;
            for (int i = 0; i < exp.Explist.Count; i++)
            {
                total += exp.Explist[i].Price;
            }

            exp.totalExp = total;
            foreach (var item in exp.Explist)
            {
                exp.dataPoints.Add(new dataPoints(item.Price, item.Price));
            }
            exp.currentRep = DropdownName;

            return View(exp);
        }




        public async Task<IActionResult> Custmores(string custid)
        {
            if (custid != null)
            {

                OrdersReport exp = new OrdersReport();
                IEnumerable<Order> expense = await genericRepository2.MakeReportforcust(custid);
                exp.Explist = expense.ToList();
                double total = 0;
                for (int i = 0; i < exp.Explist.Count; i++)
                {
                    total += exp.Explist[i].Price;
                }

                exp.totalExp = total;
                foreach (var item in exp.Explist)
                {
                    exp.dataPoints.Add(new dataPoints(item.Price, item.Price));
                }
                exp.currentRep = custid;
                exp.Peroid.Clear();
                IEnumerable<Customer> result = await genericRepository3.GetAll();
                foreach (var item in result) {
                    exp.Peroid.Add(item.Name);
                }
                

                return View(exp);

            }
            else {
                OrdersReport exp = new OrdersReport();
                IEnumerable<Order> expense = await genericRepository2.MakeReportforcustnoone();
                exp.Explist = expense.ToList();
                double total = 0;
                for (int i = 0; i < exp.Explist.Count; i++)
                {
                    total += exp.Explist[i].Price;
                }

                exp.totalExp = total;
                exp.Peroid.Clear();
                foreach (var item in exp.Explist)
                {
                    exp.dataPoints.Add(new dataPoints(item.Price, item.Price));
                }
                exp.currentRep = custid;

                IEnumerable<Customer> result = await genericRepository3.GetAll();
                foreach (var item in result)
                {
                    exp.Peroid.Add(item.Name);
                }

                return View(exp);

            }
        }


    }
}
