using efcore1.Models;
using Microsoft.EntityFrameworkCore;

namespace efcore1;

public class CompanyContext : DbContext
{
    private IConfiguration _configuration;

    public DbSet<Department> Departments { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Salary> Salaries { get; set; }

    public CompanyContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_configuration["ConnectionString"]);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>()
            .Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(255);

        modelBuilder.Entity<Employee>()
            .Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(255);

        modelBuilder.Entity<Project>()
            .Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(255);

        modelBuilder.Entity<Salary>()
            .Property(d => d.Amount)
            .IsRequired();

        modelBuilder.Entity<ProjectEmployee>()
            .HasKey(t => new { t.ProjectId, t.EmployeeId });

        modelBuilder.Entity<Employee>()
            .HasMany(t => t.Projects)
            .WithMany(t => t.Employees)
            .UsingEntity<ProjectEmployee>();

        modelBuilder.Entity<Department>()
            .HasData(
                new Department { Id = 1, Name = "Software Development" },
                new Department { Id = 2, Name = "Finance" },
                new Department { Id = 3, Name = "Accountant" },
                new Department { Id = 4, Name = "HR" }
            );
    }
}