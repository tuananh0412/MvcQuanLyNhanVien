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
    [Route("[Controller]/[Action]/{id?}")]
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

            var employees = await _context.Employees.ToListAsync();
            var empIds = employees.Select(e => e.Id).ToList();
            var certificates = await _context.Certificates.Where(c => empIds.Contains(c.Id)).ToListAsync();
            foreach (var e in employees)
            {
                e.Certificates = certificates.Where(c => c.Id == e.Id).ToList();
            }

            var employee = employees.FirstOrDefault(m => m.Id == id);

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

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
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

            var employees = await _context.Employees.ToListAsync();
            var empIds = employees.Select(e => e.Id).ToList();
            var certificates = await _context.Certificates.Where(c => empIds.Contains(c.Id)).ToListAsync();
            foreach (var e in employees)
            {
                e.Certificates = certificates.Where(c => c.Id == e.Id).ToList();
            }

            var employee = await _context.Employees.FirstOrDefaultAsync(m => m.Id == id);
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
            var certificates = await _context.Certificates.Where(c => c.Id == id).ToListAsync();
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.Certificates.RemoveRange(certificates);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Employees");
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }

        // GET: Experiences/CreateCertificate
        public IActionResult CreateCertificateExperience()
        {
            return View();
        }

        //POST: Experiences/CreateCertificateExperience/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCertificateExperience([Bind("CertificateId, CertificateName, CertificateRank")] Certificate certificate, int id)
        {
            if (ModelState.IsValid)
            {
                certificate.Id = id;
                _context.Add(certificate);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(DetailsExperience), new {certificate.Id});
            }
            return View(certificate);
        }

        // GET: Experiences/EditCertificateExperience/5
        public async Task<IActionResult> EditCertificateExperience(int? id)
        {
            if (id == null || _context.Certificates == null)
            {
                return NotFound();
            }

            var certificate = await _context.Certificates
                .FirstOrDefaultAsync(c => c.CertificateId == id);
            if (certificate == null)
            {
                return NotFound();
            }

            return View(certificate);
        }

        // POST: Experiences/EditCertificateExperience/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCertificateExperience(int CertificateId, [Bind("CertificateId, CertificateName, CertificateRank")] Certificate certificate)
        {
            if (CertificateId != certificate.CertificateId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var ctfcate = await _context.Certificates
                .FirstOrDefaultAsync(c => c.CertificateId == CertificateId);
                    ctfcate.CertificateName = certificate.CertificateName;
                    ctfcate.CertificateRank = certificate.CertificateRank;
                    certificate.Id = ctfcate.Id;
                    _context.Update(ctfcate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(certificate.CertificateId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(DetailsExperience), new { certificate.Id });
            }
            return View(certificate);
        }

        // GET: Experiences/DeleteCertificateExperience/5
        public async Task<IActionResult> DeleteCertificateExperience(int? id)
        {
            if (id == null || _context.Certificates == null)
            {
                return NotFound();
            }

            var certificate = await _context.Certificates
                .FirstOrDefaultAsync(c => c.CertificateId == id);
            if (certificate == null)
            {
                return NotFound();
            }

            return View(certificate);
        }

        // POST: Experiences/DeleteCertificateExperience/5
        [HttpPost, ActionName("DeleteCertificateExperience")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCertificateExperienceConfirmed(int id)
        {
            if (_context.Certificates == null)
            {
                return Problem("Entity set 'CertificateContext.Certificates'  is null.");
            }
            var certificate = await _context.Certificates.FindAsync(id);
            if (certificate != null)
            {
                _context.Certificates.Remove(certificate);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(DetailsExperience), new { certificate.Id });
        }
    }
}
