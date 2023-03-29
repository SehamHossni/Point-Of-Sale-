using AutoMapper;
using BLL.inteface;
using BLL.ViewModel;
using Dal.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace WebApplication4.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CustomerController : Controller
    {
        private readonly IGenericRepository<Customer> genericRepository;
        private readonly IMapper mapper;
        private readonly ICustomerRepository customerRepository;
        private readonly IOrderProductRepository orderProduct;

        public CustomerController(IGenericRepository<Customer> genericRepository, IMapper mapper, ICustomerRepository customerRepository)
        {
            this.genericRepository = genericRepository;
            this.mapper = mapper;
            this.customerRepository = customerRepository;
            this.orderProduct = orderProduct;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Customer> result = await genericRepository.GetAll();
            var Data = mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerVM>>(result);

            return View(Data);
        }
        public async Task<IActionResult> Details(int? id)
        {

            Customer result = await genericRepository.GetById(id);
            if (result != null)
            {
                var Data = mapper.Map<Customer, CustomerVM>(result);

                return View(Data);
            }


            return NotFound();

        }

        [HttpGet]
        public IActionResult Create()
        {

            return View(new CustomerVM());

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerVM orderVM)
        {
            if (ModelState.IsValid)
            {

                var result = mapper.Map<CustomerVM, Customer>(orderVM);
                var data = await genericRepository.Create(result);


                return RedirectToAction("Index");

            }
            return View(orderVM);
        }

        [HttpGet]

        public async Task<IActionResult> Update(int id)
        {
            Customer result = await genericRepository.GetById(id);
            if (result != null)
            {
                return View(mapper.Map<Customer, CustomerVM>(result));
            }
            return NotFound();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CustomerVM order)
        {
            if (ModelState.IsValid)
            {
                var Data = mapper.Map<CustomerVM, Customer>(order);
                await genericRepository.Update(Data);


                return RedirectToAction("Index");
            }
            return View(order);

        }



        public async Task<IActionResult> Delete(int id)
        {
            var result = await genericRepository.GetById(id);

            await genericRepository.Delete(result);

            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult CheckProducts()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CheckProducts(int id)
        {
            var data = customerRepository.GetProductByCustomerId(id);
            //var result= mapper.Map<IEnumerable<Product>,IEnumerable<ProductVM>>(data);
            return Json(data);
        }
    }
}
