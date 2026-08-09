using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pr4;
using pr4.Models;

[Authorize(Roles = "Администратор")]
public class ProductsController : Controller
{
    private readonly ShopDbContext _context;

    public ProductsController(ShopDbContext context)
    {
        _context = context;
    }

    // GET: Products
    public async Task<IActionResult> Index()
    {
        AppLogger.LogMethod("ProductsController.Index", "Открытие каталога товаров");


        var products = await _context.Products
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .ToListAsync();

        return View(products);
    }

    // GET: Products/Details/5
    public async Task<IActionResult> Details(int? productid)
    {
        if (productid == null)
            return NotFound();

        var product = await _context.Products
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.ProductId == productid);

        if (product == null)
            return NotFound();

        return View(product);
    }

    // GET: Products/Create
    public IActionResult Create()
    {
        ViewBag.Brands = _context.Brands.ToList();
        ViewBag.Categories = _context.Categories.ToList();

        return View();
    }

    // POST: Products/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("ProductId,CategoryId,Name,BrandId,Price,InStock,Description")] Product product)
    {
        if (ModelState.IsValid)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        ViewBag.Brands = _context.Brands.ToList();
        ViewBag.Categories = _context.Categories.ToList();

        return View(product);
    }

    // GET: Products/Edit/5
    public async Task<IActionResult> Edit(int? productid)
    {
        if (productid == null)
            return NotFound();

        var product = await _context.Products.FindAsync(productid);

        if (product == null)
            return NotFound();

        ViewBag.Brands = _context.Brands.ToList();
        ViewBag.Categories = _context.Categories.ToList();

        return View(product);
    }

    // POST: Products/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int productid,
        [Bind("ProductId,CategoryId,Name,BrandId,Price,InStock,Description")] Product product)
    {
        if (productid != product.ProductId)
            return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Products.Update(product);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.ProductId))
                    return NotFound();

                throw;
            }
        }

        ViewBag.Brands = _context.Brands.ToList();
        ViewBag.Categories = _context.Categories.ToList();

        return View(product);
    }

    // GET: Products/Delete/5
    public async Task<IActionResult> Delete(int? productid)
    {
        if (productid == null)
            return NotFound();

        var product = await _context.Products
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.ProductId == productid);

        if (product == null)
            return NotFound();

        return View(product);
    }

    // POST: Products/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int productid)
    {
        AppLogger.LogMethod(
        "ProductsController.DeleteConfirmed",
        $"Удаление товара с ID {productid}");

        try
        {
            var product = await _context.Products.FindAsync(productid);

            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException)
        {
            TempData["Error"] =
                "Нельзя удалить товар, так как он используется в существующих заказах.";

            return RedirectToAction(nameof(Index));
        }
    }

    private bool ProductExists(int productid)
    {
        return _context.Products.Any(p => p.ProductId == productid);
    }
}