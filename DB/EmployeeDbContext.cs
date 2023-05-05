using EmployeeDetails.Model;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDetails.DB
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<EmployeeProject> EmployeeProjects { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<SignUp> SignUps { get; set; }

        public DbSet<SignupRole> SignUpsRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeProject>()
                .HasKey(ep => new { ep.EmployeeId, ep.ProjectId });

            modelBuilder.Entity<EmployeeProject>()
                .HasOne(ep => ep.Employee)
                .WithMany(e => e.EmployeeProjects)
                .HasForeignKey(ep => ep.EmployeeId);

            modelBuilder.Entity<EmployeeProject>()
                .HasOne(ep => ep.Project)
                .WithMany(p => p.EmployeeProjects)
                .HasForeignKey(ep => ep.ProjectId)
                .IsRequired(false);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId);

            modelBuilder.Entity<SignupRole>()
                .HasKey(sr => new { sr.SignUpId, sr.RoleId });

            modelBuilder.Entity<SignupRole>()
                .HasOne(sr => sr.SignUp)
                .WithMany(s => s.SignupRoles)
                .HasForeignKey(sr => sr.SignUpId);


            modelBuilder.Entity<SignupRole>()
                .HasOne(sr => sr.Role)
                .WithMany(r => r.SignupRoles)
                .HasForeignKey(sr => sr.RoleId)
                .IsRequired(false);


        }


    }
}
