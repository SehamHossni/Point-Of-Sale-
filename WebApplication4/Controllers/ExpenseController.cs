using AutoMapper;
using BLL.inteface;
using BLL.ViewModel;
using Dal.Entities;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication4.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ExpenseController : Controller
    {
        private readonly IGenericRepository<Expense> genericRepository;
        private readonly IMapper mapper;
        private readonly IExpenseRepository expenseRepository;

        public ExpenseController(IGenericRepository<Expense> genericRepository, IMapper mapper, IExpenseRepository expenseRepository)
        {
            this.genericRepository = genericRepository;
            this.mapper = mapper;
            this.expenseRepository = expenseRepository;
        }



        public async Task<IActionResult> Index(string searching)
        {

            IEnumerable<Expense> expense = await genericRepository.GetAll();
            var Data = mapper.Map<IEnumerable<Expense>, IEnumerable<ExpenseVM>>(expense);
            return View(Data);
        }
        

        public async Task<IActionResult> Details(int? id)
        {

            Expense result = await genericRepository.GetById(id);
            if (result != null)
            {
                var Data = mapper.Map<Expense,  ExpenseVM>(result);

                return View(Data);
            }

            return NotFound();
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View(new ExpenseVM());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExpenseVM   expenseVM)
        {
            if (ModelState.IsValid)
            {
                var result = mapper.Map<ExpenseVM, Expense>(expenseVM);

                await genericRepository.Create(result);
                return RedirectToAction("Index");

            }
            return View(expenseVM);
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Expense result = await genericRepository.GetById(id);
            if (result != null)
            {
                return View(mapper.Map<Expense, ExpenseVM>(result));
            }
            return NotFound();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ExpenseVM expense)
        {
            if (ModelState.IsValid)
            {
                var Data = mapper.Map<ExpenseVM, Expense>(expense);
                await genericRepository.Update(Data);

                return RedirectToAction("Index");
            }
            return View(expense);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var result = await genericRepository.GetById(id);

            await genericRepository.Delete(result);

            return RedirectToAction("Index");

        }


    }
}
