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
    public class EmpProjsController : Controller
    {
        private readonly ApplicationContext _context;
        public static List<EmpProj> employers = new List<EmpProj>() { new EmpProj() };
        public static Project project = new Project();


        public EmpProjsController(ApplicationContext context)
        {
            _context = context;
        }

        private void FillData()
        {
            var empList = _context.Employers.Select(e => new { e.Id, e.Name }).ToList();
            ViewBag.Employers = empList;
        }

        public IActionResult Add(List<EmpProj> empLists)
        {
            int projectId = 0;
            if (!ModelState.IsValid)
            {
                FillData();
                projectId = empLists.FirstOrDefault().ProjectID;
                ViewBag.EmpList = project.Employers;
                return View("ShowEmployers", new { projectId });
            }

            employers = empLists;
            project.Employers = empLists;
            projectId = 1;
            employers.Add(new EmpProj { ProjectID = projectId, EmployeeID = 1 });  

            FillData();
            return RedirectToAction("ShowEmployers", new { projectId });
        }

        public async Task<IActionResult> ShowEmployers(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            FillData();

            if (employers.Count == 0)
                employers = _context.EmpProjs
                    .Include(e => e.Employee)
                    .Where(p => p.ProjectID == id)
                    .ToList();
            ViewBag.EmpList = employers;
            ViewBag.ProjectID = id;

            return View();
        }

        public async Task<IActionResult> ShowProject(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var applicationContext = _context.EmpProjs.Where(e => e.EmployeeID == id.Value)
                .Include(e => e.Employee)
                .Include(e => e.Project);
            return View(await applicationContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empProj = await _context.EmpProjs
                .Include(e => e.Employee)
                .Include(e => e.Project)
                .FirstOrDefaultAsync(m => m.EmployeeID == id);
            if (empProj == null)
            {
                return NotFound();
            }

            return View(empProj);
        }

        public IActionResult Create()
        {
            ViewData["EmployeeID"] = new SelectList(_context.Employers, "Id", "Id");
            ViewData["ProjectID"] = new SelectList(_context.Projects, "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectID,EmployeeID")] EmpProj empProj)
        {
            if (ModelState.IsValid)
            {
                _context.Add(empProj);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employers, "Id", "Id", empProj.EmployeeID);
            ViewData["ProjectID"] = new SelectList(_context.Projects, "Id", "Id", empProj.ProjectID);
            return View(empProj);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empProj = await _context.EmpProjs.FindAsync(id);
            if (empProj == null)
            {
                return NotFound();
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employers, "Id", "Id", empProj.EmployeeID);
            ViewData["ProjectID"] = new SelectList(_context.Projects, "Id", "Id", empProj.ProjectID);
            return View(empProj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectID,EmployeeID")] EmpProj empProj)
        {
            if (id != empProj.EmployeeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empProj);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpProjExists(empProj.EmployeeID))
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
            ViewData["EmployeeID"] = new SelectList(_context.Employers, "Id", "Id", empProj.EmployeeID);
            ViewData["ProjectID"] = new SelectList(_context.Projects, "Id", "Id", empProj.ProjectID);
            return View(empProj);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empProj = await _context.EmpProjs
                .Include(e => e.Employee)
                .Include(e => e.Project)
                .FirstOrDefaultAsync(m => m.EmployeeID == id);
            if (empProj == null)
            {
                return NotFound();
            }

            return View(empProj);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empProj = await _context.EmpProjs.FindAsync(id);
            if (empProj != null)
            {
                _context.EmpProjs.Remove(empProj);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpProjExists(int id)
        {
            return _context.EmpProjs.Any(e => e.EmployeeID == id);
        }
    }
}
