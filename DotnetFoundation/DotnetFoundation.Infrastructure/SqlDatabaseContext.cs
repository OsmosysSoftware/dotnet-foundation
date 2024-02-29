namespace DotnetFoundation.Infrastructure;

using DotnetFoundation.Domain.Entities;
using DotnetFoundation.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class SqlDatabaseContext : IdentityDbContext<IdentityApplicationUser>
{
    public SqlDatabaseContext(DbContextOptions<SqlDatabaseContext> options) : base(options)
    {

    }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<TaskDetails> TaskDetails { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>()
        .HasKey(au => au.Id);
        builder.Entity<ApplicationUser>().ToTable("users");

        builder.Entity<ApplicationUser>()
        .HasOne(au => au.IdentityApplicationUser)
        .WithOne(iu => iu.ApplicationUser)
        .HasForeignKey<ApplicationUser>(au => au.IdentityApplicationUserId);

        // for tasks
        builder.Entity<TaskDetails>()
        .HasKey(t => t.Id);

        builder.Entity<TaskDetails>()
        .HasIndex(t => t.Status);

        builder.Entity<TaskDetails>()
        .HasIndex(t => t.AssignedTo);

        builder.Entity<TaskDetails>().ToTable("tasks");
    }
}

