using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MvcQuanLyNhanVien.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Certificates = new HashSet<Certificate>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Họ tên không được để trống")]
        [Display(Name = "Họ tên")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "Ngày sinh không được để trống")]
        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Giới tính không được để trống")]
        [Display(Name = "Giới tính")]
        public string? Sex { get; set; }

        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        [Display(Name = "Địa chỉ")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [Display(Name = "Số điện thoại")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [Display(Name = "Địa chỉ email")]
        public string? Email { get; set; }

        [Display(Name = "Số năm kinh nghiệm")]
        public int? ExpInYear { get; set; }

        [Display(Name = "Kỹ năng chuyên môn")]
        public string? ProSkill { get; set; }

        [Display(Name = "Ngày tốt nghiệp")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? GraduationDate { get; set; }

        [Display(Name = "Xếp hạng tốt nghiệp")]
        public string? GraduationRank { get; set; }

        [Display(Name = "Trường tốt nghiệp")]
        public string? Education { get; set; }

        [Display(Name = "Chuyên ngành đang học")]
        public string? Majors { get; set; }

        [Display(Name = "Học kì đang học")]
        public int? Semester { get; set; }

        [Display(Name = "Trường đang học")]
        public string? UniversityName { get; set; }

        [Display(Name = "Số bằng cấp")]
        public virtual ICollection<Certificate> Certificates { get; set; }
    }
}
