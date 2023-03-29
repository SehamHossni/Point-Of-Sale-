using AutoMapper;
using BLL.inteface;
using BLL.ViewModel;
using Dal.Context;
using Dal.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace WebApplication4.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IGenericRepository<Order> genericRepository;

        private readonly IMapper mapper;
        private readonly IOrderProductRepository orderProduct;
        private readonly IGenericRepository<OrderProduct> orderproductRepo;

        
        public OrderController(IGenericRepository<Order> genericRepository, IMapper mapper, IOrderProductRepository orderProduct, IGenericRepository<OrderProduct> orderproductRepo)
        {
            this.genericRepository = genericRepository;
            this.mapper = mapper;
            this.orderProduct = orderProduct;
            this.orderproductRepo = orderproductRepo;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Order> result = await genericRepository.GetAll();

            List<OrderVM> data = new List<OrderVM>();
            OrderVM vm;
            foreach (var item in result)
            {
                List<Product> products = await orderProduct.GetProduct(item.Id);

                vm = new OrderVM()
                {
                    Id = item.Id,
                    OrderTime = item.OrderTime,
                    Products = products,
                    CustomerId = item.CustomerId,
                    Customer = item.Customer,
                    Price = item.Price,
                };

                data.Add(vm);
            }
            //var Data = mapper.Map<IEnumerable<Order>, IEnumerable<OrderVM>>(result);

            return View(data);
        }

        public async Task<IActionResult> Details(int? id)
        {

            Order result = await genericRepository.GetById(id);

            

            if (result != null)
            {
                List<Product> products = await orderProduct.GetProduct(result.Id);
                
                OrderVM vM = new OrderVM
                {
                    Id = result.Id,
                    OrderTime = result.OrderTime,
                    Products = products,
                    Price=result.Price,
                    CustomerId = result.CustomerId,
                    Customer = result.Customer,

                };

                return View(vM);
            }


            return NotFound();

        }
        [HttpGet]
        public IActionResult Create()
        {

            return View(new OrderVM());

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderVM orderVM)
        {
            if (ModelState.IsValid)
            {

                var result = mapper.Map<OrderVM, Order>(orderVM);
                var data = await genericRepository.Create(result);
                OrderProduct orderProduct;
                foreach (var item in orderVM.ProductId)
                {
                    orderProduct = new OrderProduct()
                    {
                        OrderId = result.Id,
                        ProductId = item,
                    };
                    await orderproductRepo.Create(orderProduct);


                }

                return RedirectToAction("Index");

            }
            return View(orderVM);
        }
        [HttpGet]

        public async Task<IActionResult> Update(int id)
        {
            Order result = await genericRepository.GetById(id);
            if (result != null)
            {
                return View(mapper.Map<Order, OrderVM>(result));
            }
            return NotFound();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(OrderVM order)
        {
            if (ModelState.IsValid)
            {
                var Data = mapper.Map<OrderVM, Order>(order);
                await genericRepository.Update(Data);


                return RedirectToAction("Index");
            }
            return View(order);

        }


        public async Task<IActionResult> Delete(int id)
        {
            var data = await genericRepository.GetById(id);
            //var result=await orderproductRepo.GetById(id);
            if (data != null)
            {

                await genericRepository.Delete(data);

                //await   orderproductRepo.Delete(result);

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");

        }


        

    }
}
