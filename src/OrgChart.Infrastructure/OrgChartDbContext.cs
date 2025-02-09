using Microsoft.EntityFrameworkCore;
using OrgChart.Core.Models;
using OrgChart.Infrastructure.Entities;

namespace OrgChart.Infrastructure;

public class OrgChartDbContext : DbContext
{
    public OrgChartDbContext(DbContextOptions<OrgChartDbContext> options)
        : base(options)
    { }

    public DbSet<Employee> Employees => Set<Employee>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Self-referencing relationship.
        modelBuilder.Entity<Employee>()
            .ToTable("employees", "orgchart")
            .HasMany(e => e.Subordinates)
            .WithOne(e => e.Manager)
            .HasForeignKey(e => e.ManagerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Employee>().Property(e => e.Id).HasColumnName("id");
        modelBuilder.Entity<Employee>().Property(e => e.Name).HasColumnName("name");
        modelBuilder.Entity<Employee>().Property(e => e.ManagerId).HasColumnName("manager_id");

        modelBuilder.Entity<SubordinateCountResult>().HasNoKey().ToView(null);
        modelBuilder.Entity<HierarchyDepthResult>().HasNoKey().ToView(null);
        modelBuilder.Entity<CountResult>().HasNoKey().ToView(null);
    }
}
