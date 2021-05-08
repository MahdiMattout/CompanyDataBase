using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Company.Models
{
    public partial class CompanyContext : DbContext
    {
        public CompanyContext(DbContextOptions<CompanyContext> options)
            : base(options)
        {
        }
        public virtual DbSet<AdminsOfDangerousPc> AdminsOfDangerousPcs { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<CompanyPc> CompanyPcs { get; set; }
        public virtual DbSet<CountRelativesOfLowSalaryEmployee> CountRelativesOfLowSalaryEmployees { get; set; }
        public virtual DbSet<DangerousPcsView> DangerousPcsViews { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<HealthyPc> HealthyPcs { get; set; }
        public virtual DbSet<HourlyPaid> HourlyPaids { get; set; }
        public virtual DbSet<MonthlyPaid> MonthlyPaids { get; set; }
        public virtual DbSet<Relative> Relatives { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL("Server=localhost;port=5555;Database=Company;username=root;password=omar123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminsOfDangerousPc>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("admins_of_dangerous_pcs");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.EmployeeId).HasColumnName("employeeID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.IsAdmin).HasColumnType("tinyint");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Position)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Ssn).HasColumnName("SSN");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PRIMARY");

                entity.ToTable("company");

                entity.Property(e => e.Name).HasMaxLength(45);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.ContactEmail)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("contactEmail");

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Zip).HasColumnName("ZIP");
            });

            modelBuilder.Entity<CompanyPc>(entity =>
            {
                entity.HasKey(e => e.PcId)
                    .HasName("PRIMARY");

                entity.ToTable("company_pcs");

                entity.HasIndex(e => e.EmployeeId, "fk_COMPANY_PCS_EMPLOYEE1_idx");

                entity.HasIndex(e => e.NameofCompany, "nameofCompany_idx");

                entity.Property(e => e.PcId).HasColumnName("PcID");

                entity.Property(e => e.AdminId).HasColumnName("AdminID");

                entity.Property(e => e.CpuModel)
                    .HasMaxLength(45)
                    .HasColumnName("cpuModel");

                entity.Property(e => e.Cpumanufacturer)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("CPUmanufacturer");

                entity.Property(e => e.DiskModel)
                    .HasMaxLength(45)
                    .HasColumnName("diskModel");

                entity.Property(e => e.EmployeeId).HasColumnName("employeeID");

                entity.Property(e => e.MemoryModel).HasMaxLength(45);

                entity.Property(e => e.NameOfUser).HasMaxLength(45);

                entity.Property(e => e.NameofCompany)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("nameofCompany");

                entity.Property(e => e.ReadWriteSpeed).HasColumnName("read-writeSpeed");

                entity.Property(e => e.TotalMemory).HasColumnName("totalMemory");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.CompanyPcs)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_COMPANY_PCS_EMPLOYEE1");

                entity.HasOne(d => d.NameofCompanyNavigation)
                    .WithMany(p => p.CompanyPcs)
                    .HasForeignKey(d => d.NameofCompany)
                    .HasConstraintName("nameofCompany");
            });

            modelBuilder.Entity<CountRelativesOfLowSalaryEmployee>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("count_relatives_of_low_salary_employees");

                entity.Property(e => e.CountRelativesName).HasColumnName("Count(relatives.Name)");

                entity.Property(e => e.EmployeeId).HasColumnName("employeeID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<DangerousPcsView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("dangerous_pcs_view");

                entity.Property(e => e.AdminId).HasColumnName("AdminID");

                entity.Property(e => e.CpuModel)
                    .HasMaxLength(45)
                    .HasColumnName("cpuModel");

                entity.Property(e => e.Cpumanufacturer)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("CPUmanufacturer");

                entity.Property(e => e.DiskModel)
                    .HasMaxLength(45)
                    .HasColumnName("diskModel");

                entity.Property(e => e.EmployeeId).HasColumnName("employeeID");

                entity.Property(e => e.MemoryModel).HasMaxLength(45);

                entity.Property(e => e.NameOfUser).HasMaxLength(45);

                entity.Property(e => e.NameofCompany)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("nameofCompany");

                entity.Property(e => e.PcId).HasColumnName("PcID");

                entity.Property(e => e.ReadWriteSpeed).HasColumnName("read-writeSpeed");

                entity.Property(e => e.TotalMemory).HasColumnName("totalMemory");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employee");

                entity.HasIndex(e => e.CompanyName, "CompanyName_idx");

                entity.HasIndex(e => e.Ssn, "SSN_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.EmployeeId, "employeeID_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.EmployeeId).HasColumnName("employeeID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.IsAdmin).HasColumnType("tinyint");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Position)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Ssn).HasColumnName("SSN");

                entity.HasOne(d => d.CompanyNameNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.CompanyName)
                    .HasConstraintName("CompanyName");
            });

            modelBuilder.Entity<HealthyPc>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("healthy_pcs");

                entity.Property(e => e.AdminId).HasColumnName("AdminID");

                entity.Property(e => e.CpuModel)
                    .HasMaxLength(45)
                    .HasColumnName("cpuModel");

                entity.Property(e => e.Cpumanufacturer)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("CPUmanufacturer");

                entity.Property(e => e.DiskModel)
                    .HasMaxLength(45)
                    .HasColumnName("diskModel");

                entity.Property(e => e.EmployeeId).HasColumnName("employeeID");

                entity.Property(e => e.MemoryModel).HasMaxLength(45);

                entity.Property(e => e.NameOfUser).HasMaxLength(45);

                entity.Property(e => e.NameofCompany)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("nameofCompany");

                entity.Property(e => e.PcId).HasColumnName("PcID");

                entity.Property(e => e.ReadWriteSpeed).HasColumnName("read-writeSpeed");

                entity.Property(e => e.TotalMemory).HasColumnName("totalMemory");
            });

            modelBuilder.Entity<HourlyPaid>(entity =>
            {
                entity.HasKey(e => e.HourlyPaidEmployeeId)
                    .HasName("PRIMARY");

                entity.ToTable("hourly_paid");

                entity.HasIndex(e => e.HourlyPaidEmployeeId, "HourlyPaidEmployeeID_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.HourlyPaidEmployeeId).HasColumnName("HourlyPaidEmployeeID");

                entity.HasOne(d => d.HourlyPaidEmployee)
                    .WithOne(p => p.HourlyPaid)
                    .HasForeignKey<HourlyPaid>(d => d.HourlyPaidEmployeeId)
                    .HasConstraintName("fk_HOURLY_PAID_EMPLOYEE1");
            });

            modelBuilder.Entity<MonthlyPaid>(entity =>
            {
                entity.HasKey(e => e.MonthlyPaidEmployeeId)
                    .HasName("PRIMARY");

                entity.ToTable("monthly_paid");

                entity.HasIndex(e => e.MonthlyPaidEmployeeId, "MonthlyPaidEmployeeID_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.MonthlyPaidEmployeeId).HasColumnName("MonthlyPaidEmployeeID");

                entity.HasOne(d => d.MonthlyPaidEmployee)
                    .WithOne(p => p.MonthlyPaid)
                    .HasForeignKey<MonthlyPaid>(d => d.MonthlyPaidEmployeeId)
                    .HasConstraintName("fk_MONTHLY_PAID_EMPLOYEE1");
            });

            modelBuilder.Entity<Relative>(entity =>
            {
                entity.HasKey(e => new { e.EmployeeId, e.Name })
                    .HasName("PRIMARY");

                entity.ToTable("relatives");

                entity.HasIndex(e => e.EmployeeId, "ID_idx");

                entity.Property(e => e.EmployeeId).HasColumnName("employeeID");

                entity.Property(e => e.Name).HasMaxLength(45);

                entity.Property(e => e.Relationship)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Relatives)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("employeeID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
