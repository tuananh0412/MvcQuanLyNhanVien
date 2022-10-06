using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcQuanLyNhanVien.Models;

namespace QuanLyNhanVien.Controllers
{
    public class ExperiencesController : Controller
    {
        private readonly EmployeeContext _context;

        public ExperiencesController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: Experiences/CreateExperience
        public IActionResult CreateExperience()
        {
            return View();
        }

        // POST: Experiences/CreateExperience
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateExperience([Bind("Id,FullName,DateOfBirth,Sex,Address,PhoneNumber,Email,ExpInYear,ProSkill")] Employee employee)
        {

            _context.Add(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Employees");
        }

        // GET: Experiences/DetailsExperience/5
        public async Task<IActionResult> DetailsExperience(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees/*.Include(a => a.EmployeeCertificates).ThenInclude(a => a.Certificate)*/
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Experiences/Edit/5
        public async Task<IActionResult> EditExperience(int? id)
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

        // POST: Employees/EditExperience/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditExperience(int id, [Bind("Id,FullName,DateOfBirth,Sex,Address,PhoneNumber,Email,ExpInYear,ProSkill")] Employee employee)
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

        // GET: Experiences/DeleteExperience/5
        public async Task<IActionResult> DeleteExperience(int? id)
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

        // POST: Experiences/Delete/5
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
