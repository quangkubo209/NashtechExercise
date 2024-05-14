using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class QuangDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=KuboNguyenPC;Database=EFCore1;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False");
        }
        public QuangDbContext()
        {
        }

        public QuangDbContext(DbContextOptions<QuangDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Salary)
                .WithOne(s => s.Employee)
                .HasForeignKey<Salaries>(x => x.EmployeeId);



            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(x => x.DepartmentId);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.Employees)
                .WithMany(e => e.Projects)
                .UsingEntity<Project_Employee>();


            modelBuilder.Entity<Project_Employee>().HasKey(pe => new { pe.ProjectId, pe.EmployeeId });
            modelBuilder.Entity<Department>().HasData(
                new Department
                {
                    Id = 1,
                    Name = "Software Development",
                    CreatedAt = DateTime.Now
                },
                new Department
                {
                    Id = 2,
                    Name = "Finance",
                    CreatedAt = DateTime.Now
                },
                new Department
                {
                    Id = 3,
                    Name = "Accountant",
                    CreatedAt = DateTime.Now
                },
                new Department
                {
                    Id = 4,
                    Name = "HR",
                    CreatedAt = DateTime.Now
                }
            );
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project_Employee> ProjectEmployees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Salaries> Salaries { get; set; }
    }
}
