using AutoMapper;
using BLL.inteface;
using BLL.ViewModel;
using Dal.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication4.Controllers
{
    
    public class ProductController : Controller
    {
        private readonly IGenericRepository<Product> genericRepository;
        private readonly IMapper mapper;
        private readonly IOrderProductRepository orderProduct;
        private readonly IWebHostEnvironment webHostEnvironment;

        public IProductRepository ProductRepository { get; }

        public ProductController(IGenericRepository<Product> genericRepository, IProductRepository productRepository, IMapper mapper, IOrderProductRepository orderProduct, IWebHostEnvironment webHostEnvironment)
        {
            this.genericRepository = genericRepository;
            ProductRepository = productRepository;
            this.mapper = mapper;
            this.orderProduct = orderProduct;
            this.webHostEnvironment = webHostEnvironment;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> result = await genericRepository.GetAll();
            var Data = mapper.Map<IEnumerable<Product>, IEnumerable<ProductVM>>(result);

            return View(Data);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {

            Product result = await genericRepository.GetById(id);
            if (result != null)
            {
                var Data = mapper.Map<Product, ProductVM>(result);

                return View(Data);
            }


            return NotFound();

        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {

            return View(new ProductVM());

        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductVM orderVM)
        {
            if (ModelState.IsValid)
            {
                string folder = Path.Combine(webHostEnvironment.WebRootPath, "Image");
                string ImageName = Guid.NewGuid().ToString() + "_" + orderVM.ImgFile.FileName;
                string filePath = Path.Combine(folder, ImageName);
                using var fs = new FileStream(filePath, FileMode.Create);
                orderVM.Img = ImageName;
                orderVM.ImgFile.CopyTo(fs);
                var result = mapper.Map<ProductVM, Product>(orderVM);

                await genericRepository.Create(result);
                return RedirectToAction("Index");

            }
            return View(orderVM);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]

        public async Task<IActionResult> Update(int id)
        {
            Product result = await genericRepository.GetById(id);
            if (result != null)
            {
                //string folder = Path.Combine(webHostEnvironment.WebRootPath, "Image");

                //string filePath = Path.Combine(folder,result.Img);


                //if (System.IO.File.Exists(filePath))
                //{

                //    System.IO.File.Delete(filePath);
                //}


                var x = mapper.Map<Product, ProductVM>(result);

                return View(x);

            }
            return NotFound();

        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ProductVM order)
        {
            if (ModelState.IsValid)
            {

                //string folder = Path.Combine(webHostEnvironment.WebRootPath, "Image");
                //string ImageName = Guid.NewGuid().ToString() + "_" + order.ImgFile.FileName;
                //string filePath = Path.Combine(folder, ImageName);
                //using var fs = new FileStream(filePath, FileMode.Create);

                //order.Img = ImageName;
                //order.ImgFile.CopyTo(fs);
                var Data = mapper.Map<ProductVM, Product>(order);
                await genericRepository.Update(Data);




                return RedirectToAction("Index");
            }
            return View(order);

        }


        //[HttpGet]
        //public async Task<IActionResult> InStock()
        //{

        //  var data=await  genericRepository.GetAll();
        //    return View(data);
        //}
        //[HttpPost]
        //public  JsonResult InStock(int id)
        //{
        //   var data=ProductRepository.ProductInStock(id);
        //    return Json(data);
        //}
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await genericRepository.GetById(id);
            if (data != null)
            {

                await genericRepository.Delete(data);
                string folder = Path.Combine(webHostEnvironment.WebRootPath, "Image");

                string filePath = Path.Combine(folder, data.Img);

                if (System.IO.File.Exists(filePath))
                {

                    System.IO.File.Delete(filePath);
                }



                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");

        }


        [HttpPost]
        public async Task<IActionResult> calcprces([FromBody] string[] selectedOptions)
        {
            // Process the selected options here
            List<ProductWithPrcesVM> prces = new List<ProductWithPrcesVM>();
            foreach (string s in selectedOptions) {
                Product res = await genericRepository.GetById(Convert.ToInt32(s));
                prces.Add(new ProductWithPrcesVM(res.Name,res.Price));

            }
            

            return Json(prces);
        }


    }
}
