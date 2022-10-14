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
    public class EmployeesController : Controller
    {
        private readonly EmployeeContext _context;

        public EmployeesController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index(string searchString)
        {
            var employees = await _context.Employees.ToListAsync();
            var empIds =  employees.Select(e=>e.Id).ToList();
            var certificates = await _context.Certificates.Where(c => empIds.Contains(c.Id)).ToListAsync();
            foreach (var employee in employees)
            {
                employee.Certificates = certificates.Where(c => c.Id == employee.Id).ToList();
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(s => s.FullName!.Contains(searchString)).ToList();
            }

            return View(employees);
        }
    }
}
