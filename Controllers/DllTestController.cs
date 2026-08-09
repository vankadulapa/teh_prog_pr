using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElectroGeekLibrary;
using pr4.Models;

namespace pr4.Controllers
{
    public class DllTestController : Controller
    {
        private readonly ShopDbContext _context;

        public DllTestController(ShopDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .OrderBy(p => p.Name)
                .ToListAsync();

            return View(products);
        }

        [HttpPost]
        public async Task<IActionResult> Index(int productId)
        {
            var products = await _context.Products
                .OrderBy(p => p.Name)
                .ToListAsync();

            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == productId);

            if (product == null)
            {
                return NotFound();
            }

            var result = ShopFunctions.GetProductInfo(
                product.Name,
                product.Price,
                product.InStock);

            ViewBag.ProductInfo =
                ShopFunctions.FormatProductInfo(result);

            return View(products);
        }
    }
}