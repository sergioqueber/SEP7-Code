using Microsoft.EntityFrameworkCore;

using Model;
using Microsoft.EntityFrameworkCore.Sqlite;

public class ApplicationDbContext : DbContext
{
    public DbSet<Activity> Activities { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<CarPool> CarPools { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<Reward> Rewards { get; set; }
    public DbSet<SavingFood> SavingFoods { get; set; }
    public DbSet<Staff> Staffs { get; set; }
    public DbSet<Stairs> Stairs { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Transport> Transports { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Volunteering> Volunteerings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=application.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // TPC Inheritance Configuration for Users
        modelBuilder.Entity<Admin>()
            .ToTable("Admins")
            .HasBaseType<User>();

        modelBuilder.Entity<Staff>()
            .ToTable("Staffs")
            .HasBaseType<User>();

        // TPC Inheritance Configuration for Activities
        modelBuilder.Entity<CarPool>()
            .ToTable("CarPools")
            .HasBaseType<Activity>();

        modelBuilder.Entity<SavingFood>()
            .ToTable("SavingFoods")
            .HasBaseType<Activity>();

        modelBuilder.Entity<Stairs>()
            .ToTable("Stairs")
            .HasBaseType<Activity>();

        modelBuilder.Entity<Volunteering>()
            .ToTable("Volunteerings")
            .HasBaseType<Activity>();
            
        modelBuilder.Entity<Transport>()
            .ToTable("Transports")
            .HasBaseType<Activity>();

        // Explicit configurations for other models
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(d => d.Id);
            entity.Property(d => d.Name).IsRequired().HasMaxLength(100);
            entity.HasMany<Team>().WithOne().HasForeignKey("DepartmentId");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Name).IsRequired().HasMaxLength(100);
            entity.HasMany<Staff>().WithOne().HasForeignKey("TeamId");
        });

        modelBuilder.Entity<Reward>(entity =>
        {
            entity.HasKey(r => r.Id);
            entity.Property(r => r.Description).HasMaxLength(250);
            entity.Property(r => r.PointsRequired).IsRequired();
        });


        modelBuilder.Entity<Report>(entity =>
 {
     entity.HasKey(r => r.Id);
     entity.Property(r => r.StartDate).IsRequired();
     entity.Property(r => r.EndDate).IsRequired();
     entity.Property(r => r.EmissionsSaved).IsRequired();

     entity.HasMany(r => r.ActiveParticipants)
           .WithOne()
           .HasForeignKey("ReportId")
           .OnDelete(DeleteBehavior.Cascade);

     entity.HasMany(r => r.AwardedRewards)
           .WithOne()
           .HasForeignKey("ReportId")
           .OnDelete(DeleteBehavior.Cascade);

     entity.HasMany(r => r.CompletedActivities)
           .WithOne()
           .HasForeignKey("ReportId")
           .OnDelete(DeleteBehavior.Cascade);
 });
    }
}