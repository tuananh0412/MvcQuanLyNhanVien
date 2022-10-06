using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcQuanLyNhanVien.Models;

namespace QuanLyNhanVien.Controllers
{
    public class FreshersController : Controller
    {
        private readonly EmployeeContext _context;

        public FreshersController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: Freshers/CreateExperience
        public IActionResult CreateFresher()
        {
            return View();
        }

        // POST: Freshers/CreateExperience
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFresher([Bind("Id,FullName,DateOfBirth,Sex,Address,PhoneNumber,Email,GraduationDate,GraduationRank,Education")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Employees");
            }
            return View(employee);
        }

        // GET: Freshers/Edit/5
        public async Task<IActionResult> EditFresher(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Freshers/EditExperience/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFresher(int id, [Bind("Id,FullName,DateOfBirth,Sex,Address,PhoneNumber,Email,GraduationDate,GraduationRank,Education")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Employees");
            }
            return View(employee);
        }

        // GET: Freshers/DetailsExperience/5
        public async Task<IActionResult> DetailsFresher(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Freshers/DeleteExperience/5
        public async Task<IActionResult> DeleteFresher(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Freshers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Employees == null)
            {
                return Problem("Entity set 'EmployeeContext.Employees'  is null.");
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Employees");
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
