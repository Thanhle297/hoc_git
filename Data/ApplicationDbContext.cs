using DATN.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DATN.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<ApplicationUser> Users => Set<ApplicationUser>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.ToTable("Users");

            entity.HasKey(user => user.Id);

            entity.HasIndex(user => user.NormalizedEmail).IsUnique();

            entity.Property(user => user.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(user => user.LastName)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(user => user.Email)
                .HasMaxLength(256)
                .IsRequired();

            entity.Property(user => user.NormalizedEmail)
                .HasMaxLength(256)
                .IsRequired();

            entity.Property(user => user.PhoneNumber)
                .HasMaxLength(20);

            entity.Property(user => user.PasswordHash)
                .HasMaxLength(500)
                .IsRequired();

            entity.Property(user => user.Role)
                .HasMaxLength(20)
                .HasDefaultValue(ApplicationUser.UserRole)
                .IsRequired();
        });
    }
}
