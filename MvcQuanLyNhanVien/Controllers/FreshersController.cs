using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcQuanLyNhanVien.Models;

namespace QuanLyNhanVien.Controllers
{
    [Route("[Controller]/[Action]/{id?}")]
    public class FreshersController : Controller
    {
        private readonly EmployeeContext _context;

        public FreshersController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: Freshers/CreateFresher
        public IActionResult CreateFresher()
        {
            return View();
        }

        // POST: Freshers/CreateFresher
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

        // POST: Freshers/EditFresher/5
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

        // GET: Freshers/DetailsFresher/5
        public async Task<IActionResult> DetailsFresher(int? id)
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

        // GET: Freshers/DeleteFresher/5
        public async Task<IActionResult> DeleteFresher(int? id)
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

        // GET: Freshers/CreateCertificateFresher
        public IActionResult CreateCertificateFresher()
        {
            return View();
        }

        //POST: Freshers/CreateCertificateFresher/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCertificateFresher([Bind("CertificateId, CertificateName, CertificateRank")] Certificate certificate, int id)
        {
            if (ModelState.IsValid)
            {
                certificate.Id = id;
                _context.Add(certificate);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(DetailsFresher), new { certificate.Id });
            }
            return View(certificate);
        }

        // GET: Freshers/EditCertificateFresher/5
        public async Task<IActionResult> EditCertificateFresher(int? id)
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

        // POST: Freshers/EditCertificateFresher/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCertificateFresher(int CertificateId, [Bind("CertificateId, CertificateName, CertificateRank")] Certificate certificate)
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
                return RedirectToAction(nameof(DetailsFresher), new { certificate.Id });
            }
            return View(certificate);
        }

        // GET: Freshers/DeleteCertificateFresher/5
        public async Task<IActionResult> DeleteCertificateFresher(int? id)
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

        // POST: Freshers/DeleteCertificateExperience/5
        [HttpPost, ActionName("DeleteCertificateFresher")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCertificateFresherConfirmed(int id)
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
            return RedirectToAction(nameof(DetailsFresher), new { certificate.Id });
        }
    }
}
