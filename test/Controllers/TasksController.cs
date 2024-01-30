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
    public class TasksController : Controller
    {
        private readonly ApplicationContext _context;

        // Метод для заполнения данных о сотрудниках и статусах задач в представлении
        private void FillData()
        {
            // Получаем список сотрудников и добавляем его в ViewBag для использования в представлении
            var empList = _context.Employers.Select(e => new { e.Id, e.Name }).ToList();
            ViewBag.Employers = empList;

            // Получаем список статусов задач из перечисления TaskStatus и добавляем его в ViewBag
            var statusList = from test.Models.TaskStatus ts in Enum.GetValues(typeof(test.Models.TaskStatus))
                             select new { Id = (int)ts, Name = ts.ToString() };
            ViewBag.Status = statusList;
        }

        // Конструктор контроллера, принимающий экземпляр ApplicationContext в качестве зависимости
        public TasksController(ApplicationContext context)
        {
            _context = context;
        }

        // Метод для отображения списка задач с дополнительной информацией об авторе, исполнителе и проекте
        public async Task<IActionResult> Index()
        {
            FillData();
            var project = await _context.Tasks
                .Include(a => a.Author)
                .Include(w => w.Worker)
                .Include(p => p.Project)
                .ToListAsync();
            return View(await _context.Task.ToListAsync());
        }

        // Метод для отображения деталей задачи с дополнительной информацией об авторе и исполнителе
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Task
                .Include(a => a.Author)
                .Include(w => w.Worker)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // Метод для отображения формы создания новой задачи с заполненными данными о сотрудниках и статусах
        public IActionResult Create()
        {
            FillData();
            return View();
        }

        // Метод для обработки данных формы создания новой задачи
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(test.Models.Task task)
        {
            if (ModelState.IsValid)
            {
                _context.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            FillData();
            return View(task);
        }

        // Метод для отображения формы редактирования задачи с заполненными данными о сотрудниках и статусах
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Task.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            FillData();

            return View(task);
        }

        // Метод для обработки данных формы редактирования задачи
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, test.Models.Task task)
        {
            if (id != task.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.Id))
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
            return View(task);
        }

        // Метод для отображения формы удаления задачи
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Task
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // Метод для обработки данных формы удаления задачи
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.Task.FindAsync(id);
            if (task != null)
            {
                _context.Task.Remove(task);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Метод для проверки существования задачи по идентификатору
        private bool TaskExists(int id)
        {
            return _context.Task.Any(e => e.Id == id);
        }
    }
}
