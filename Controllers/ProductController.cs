using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pr4.Models;

namespace pr4.Controllers
{
    public class ProductController : Controller
    {
        private readonly ShopDbContext _context;

        public ProductController(ShopDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string searchString)
        {
            var products = _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(p =>
                    p.Name.Contains(searchString) ||
                    p.Brand.BrandName.Contains(searchString));
            }

            return View(products);
        }
    }
}
