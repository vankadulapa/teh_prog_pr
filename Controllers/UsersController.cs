using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pr4.Models;

namespace pr4.Controllers
{
    [Authorize(Roles = "Администратор")]
    public class UsersController : Controller
    {
        private readonly ShopDbContext _context;

    public UsersController(ShopDbContext context)
        {
            _context = context;
        }

        // =========================
        // Список пользователей
        // =========================
        public async Task<IActionResult> Index()
        {
            var users = await _context.Users
                .Include(u => u.Role)
                .ToListAsync();

            return View(users);
        }

        // =========================
        // Подробности
        // =========================
        public async Task<IActionResult> Details(int? userid)
        {
            if (userid == null)
                return NotFound();

            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserId == userid);

            if (user == null)
                return NotFound();

            return View(user);
        }

        // =========================
        // ДОБАВЛЕНИЕ
        // =========================

        // GET: Users/Create
        public IActionResult Create()
        {
            ViewBag.Roles = _context.Roles.ToList();

            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Login,Password,FullName,PhoneNum,Email,DateOfBirth,RoleId")]
        User user)
        {
            // Role не приходит из формы.
            // Роль загружается отдельно по RoleId.
            ModelState.Remove("Role");

            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            // Если есть ошибка, снова загружаем список ролей
            ViewBag.Roles = _context.Roles.ToList();

            return View(user);
        }

        // =========================
        // РЕДАКТИРОВАНИЕ
        // =========================

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? userid)
        {
            if (userid == null)
                return NotFound();

            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserId == userid);

            if (user == null)
                return NotFound();

            ViewBag.Roles = _context.Roles.ToList();

            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int userid,
            [Bind("UserId,Login,Password,FullName,PhoneNum,Email,DateOfBirth,RoleId")]
        User user)
        {
            if (userid != user.UserId)
                return NotFound();

            // Role не приходит из формы
            ModelState.Remove("Role");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
                        return NotFound();

                    throw;
                }
            }

            ViewBag.Roles = _context.Roles.ToList();

            return View(user);
        }

        // =========================
        // УДАЛЕНИЕ
        // =========================

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? userid)
        {
            if (userid == null)
                return NotFound();

            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserId == userid);

            if (user == null)
                return NotFound();

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int userid)
        {
            try
            {
                var user = await _context.Users.FindAsync(userid);

                if (user != null)
                {
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();
                }
            }
            catch
            {
                TempData["Error"] =
                    "Нельзя удалить пользователя, у которого есть связанные заказы.";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int userid)
        {
            return _context.Users.Any(e => e.UserId == userid);
        }
    }

}
