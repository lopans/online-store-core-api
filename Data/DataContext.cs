using Base;
using Base.DAL;
using Base.Identity.Entities;
using Data.Entities;
using Data.Entities.Store;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class DataContext : IdentityDbContext<User, Role, int>, IDataContext
    {
        public DataContext(): base()
        {
            Database.EnsureCreated();
        }
        public DataContext(DbContextOptions options): base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Database.SetInitializer<DataContext>(new MigrateDatabaseToLatestVersion<DataContext, Configuration>());
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TestObject>();
            //modelBuilder.Entity<IdentityUserRole<Guid>>().HasKey(p => new { p.UserId, p.RoleId });

            modelBuilder.Entity<Category>()
                .HasIndex(x => x.Link)
                .IsUnique(true);
            modelBuilder.Entity<FileData>()
                .HasIndex(x => x.FileID)
                .IsUnique(true);
            modelBuilder.Entity<SubCategory>()
                .HasIndex(x => x.Link)
                .IsUnique(true);
            modelBuilder.Entity<Item>();
            modelBuilder.Entity<SaleItem>();

            //modelBuilder.Entity<AccessLevel>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
