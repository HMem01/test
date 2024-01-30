using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;
using test.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace test.Controllers
{
    public class ProjectsController : Controller
    {

        private readonly ApplicationContext _context;

        // Конструктор контроллера, принимающий экземпляр ApplicationContext в качестве зависимости
        public ProjectsController(ApplicationContext context)
        {
            _context = context;
        }

        // Метод для заполнения данных о сотрудниках и компаниях в представлении
        private void FillData()
        {
            var empList = _context.Employers.Select(e => new { e.Id, e.Name }).ToList();
            ViewBag.Employers = empList;

            var compList = _context.Companys.Select(e => new { e.Id, e.Name }).ToList();
            ViewBag.Companys = compList;
        }

        // Метод для отображения списка проектов с дополнительной информацией о менеджере, заказчике и исполнителе
        public async Task<IActionResult> Index()
        {
            FillData();
            var project = await _context.Projects
                .Include(e => e.Manager)
                .Include(c => c.Customer)
                .Include(p => p.Perfomer)
                .ToListAsync();
            return View(project);
        }

        // Метод для отображения деталей проекта с дополнительной информацией о менеджере, заказчике, исполнителе и сотрудниках
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = _context.Projects
                .Include(e => e.Manager)
                .Include(c => c.Customer)
                .Include(p => p.Perfomer)
                .Include(ep => ep.Employers)
                .FirstOrDefault(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            FillData();

            return View(project);
        }

        // Метод для отображения формы создания нового проекта с заполненными данными о сотрудниках и компаниях
        public IActionResult Create()
        {
            FillData();
            return View();
        }

        // Метод для отображения списка сотрудников, привязанных к проекту
        public IActionResult EmployersList(int projectId)
        {
            var emploers = _context.EmpProjs.
                Include(p => p.Employee)
                .Where(e => e.ProjectID == projectId);
            return PartialView(emploers);
        }

        // Метод для обработки данных формы создания нового проекта
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Project project)
        {
            if (ModelState.IsValid)
            {
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // Метод для отображения формы редактирования проекта с заполненными данными о сотрудниках и компаниях
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            FillData();

            return View(project);
        }

        // Метод для обработки данных формы редактирования проекта
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
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
            return View(project);
        }

        // Метод для отображения формы удаления проекта с заполненными данными о сотрудниках и компаниях
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }
            FillData();
            return View(project);
        }

        // Метод для обработки данных формы подтверждения удаления проекта
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Вспомогательный метод для проверки существования компании по указанному идентификатору
        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}
