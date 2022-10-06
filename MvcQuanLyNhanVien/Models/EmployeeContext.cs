using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MvcQuanLyNhanVien.Models
{
    public partial class EmployeeContext : DbContext
    {
        public EmployeeContext()
        {
        }

        public EmployeeContext(DbContextOptions<EmployeeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Certificate> Certificates { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Certificate>(entity =>
            {
                entity.ToTable("Certificate");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.HasMany(d => d.Certificates)
                    .WithMany(p => p.Employees)
                    .UsingEntity<Dictionary<string, object>>(
                        "EmployeeCertificate",
                        l => l.HasOne<Certificate>().WithMany().HasForeignKey("CertificateId"),
                        r => r.HasOne<Employee>().WithMany().HasForeignKey("EmployeeId"),
                        j =>
                        {
                            j.HasKey("EmployeeId", "CertificateId");

                            j.ToTable("EmployeeCertificate");

                            j.HasIndex(new[] { "CertificateId" }, "IX_EmployeeCertificate_CertificateId");
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
