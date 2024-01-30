using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using test.Models;

namespace test.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly ApplicationContext _context;

        // Конструктор контроллера, принимающий экземпляр ApplicationContext в качестве зависимости
        public CompaniesController(ApplicationContext context)
        {
            _context = context;
        }

        // Метод для отображения списка компаний
        public async Task<IActionResult> Index()
        {
            return View(await _context.Companys.ToListAsync());
        }

        // Метод для отображения деталей компании по указанному идентификатору
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Companys
                .FirstOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // Метод для отображения формы создания новой компании
        public IActionResult Create()
        {
            return View();
        }

        // Метод для обработки данных формы создания компании
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Company company)
        {
            if (ModelState.IsValid)
            {
                _context.Add(company);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        // Метод для отображения формы редактирования компании по указанному идентификатору
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Companys.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        // Метод для обработки данных формы редактирования компании
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Company company)
        {
            if (id != company.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(company);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(company.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        // Метод для отображения формы подтверждения удаления компании
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Companys
                .FirstOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // Метод для обработки данных формы подтверждения удаления компании
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var company = await _context.Companys.FindAsync(id);
            if (company != null)
            {
                _context.Companys.Remove(company);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Вспомогательный метод для проверки существования компании по указанному идентификатору
        private bool CompanyExists(int id)
        {
            return _context.Companys.Any(e => e.Id == id);
        }
    }
}
