using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pr4.Models;

namespace pr4.Controllers
{
    [Authorize(Roles = "Администратор")]
    public class AdminController : Controller
    {
        private readonly ShopDbContext _context;

        public AdminController(ShopDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            ViewBag.Products = _context.Products.Count();
            ViewBag.Users = _context.Users.Count();
            ViewBag.Orders = _context.Orders.Count();

            return View();
        }


        public IActionResult Reports()
        {
            ViewBag.Products = _context.Products.Count();
            ViewBag.Users = _context.Users.Count();
            ViewBag.Orders = _context.Orders.Count();

            return View();
        }
    }
}