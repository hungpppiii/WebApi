using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data
{
    public class WebDbContext : DbContext
    {
        public WebDbContext(DbContextOptions<WebDbContext> options) : base(options) { }

        #region DbSet
        public DbSet<Course> Courses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Payment> Payments { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var allEntities = modelBuilder.Model.GetEntityTypes();

            foreach (var entity in allEntities)
            {
                entity.AddProperty("CreatedDate", typeof(DateTime));
                entity.AddProperty("UpdatedDate", typeof(DateTime));
            }

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Teacher)
                .WithMany(u => u.TaughtCourses)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(c => c.TeacherId);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Students)
                .WithMany(u => u.EnrolledCourses)
                .UsingEntity(j => j.ToTable("StudentCourses"));
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e =>
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified);

            foreach (var entityEntry in entries)
            {
                entityEntry.Property("UpdatedAt").CurrentValue = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    entityEntry.Property("CreatedAt").CurrentValue = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }
    }
}
