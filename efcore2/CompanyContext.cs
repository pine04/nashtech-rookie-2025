using efcore2.Models;
using Microsoft.EntityFrameworkCore;

namespace efcore2;

public class CompanyContext : DbContext
{
    private IConfiguration _configuration;

    public DbSet<Department> Departments { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Salary> Salaries { get; set; }
    public DbSet<ProjectEmployee> ProjectEmployees { get; set; }

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

        // This is what I did in EF Core Assignment 1 but this time I wanted to rename the Join table to ProjectEmployees (with s).
        // Passing the new name into UsingEntity results in an error while creating a migration.
        // Idk why lol.

        // modelBuilder.Entity<ProjectEmployee>()
        //     .HasKey(t => new { t.ProjectId, t.EmployeeId });

        // modelBuilder.Entity<Employee>()
        //     .HasMany(t => t.Projects)
        //     .WithMany(t => t.Employees)
        //     .UsingEntity<ProjectEmployee>();

        // For some reason I have to do it EXPLICITLY like this... Idk its weird.
        // modelBuilder.Entity<Employee>()
        //     .HasMany(t => t.Projects)
        //     .WithMany(t => t.Employees)
        //     .UsingEntity<ProjectEmployee>(
        //         "ProjectEmployees",
        //         l => l.HasOne(e => e.Project).WithMany(e => e.ProjectEmployees).HasForeignKey(e => e.ProjectId).HasPrincipalKey(e => e.Id),
        //         r => r.HasOne(e => e.Employee).WithMany(e => e.ProjectEmployees).HasForeignKey(e => e.EmployeeId).HasPrincipalKey(e => e.Id),
        //         j => j.HasKey(e => new { e.ProjectId, e.EmployeeId })
        //     );

        modelBuilder.Entity<ProjectEmployee>()
            .HasKey(pe => new { pe.ProjectId, pe.EmployeeId });

        modelBuilder.Entity<ProjectEmployee>()
            .HasOne(pe => pe.Project)
            .WithMany(p => p.ProjectEmployees)
            .HasForeignKey(pe => pe.ProjectId);

        modelBuilder.Entity<ProjectEmployee>()
            .HasOne(pe => pe.Employee)
            .WithMany(e => e.ProjectEmployees)
            .HasForeignKey(pe => pe.EmployeeId);


        modelBuilder.Entity<Department>()
            .HasData(
                new Department { Id = 1, Name = "Software Development" },
                new Department { Id = 2, Name = "Finance" },
                new Department { Id = 3, Name = "Accountant" },
                new Department { Id = 4, Name = "HR" }
            );
    }
}