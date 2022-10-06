using System;
using System.Collections.Generic;

namespace MvcQuanLyNhanVien.Models
{
    public partial class Certificate
    {
        public Certificate()
        {
            Employees = new HashSet<Employee>();
        }

        public int CertificateId { get; set; }
        public string? CertificateName { get; set; }
        public string? CertificateRank { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
