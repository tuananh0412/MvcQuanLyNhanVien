using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MvcQuanLyNhanVien.Models
{
    public partial class Certificate
    {
        [Display(Name = "Mã bằng")]
        public int CertificateId { get; set; }

        [Display(Name = "Tên bằng")]
        public string? CertificateName { get; set; }

        [Display(Name = "Xếp loại")]
        public string? CertificateRank { get; set; }
        public int Id { get; set; }

        public virtual Employee? IdNavigation { get; set; }
    }
}
