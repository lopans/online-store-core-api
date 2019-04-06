using Base;
using Base.DAL;
using Base.Identity;
using Data.Entities;
using Data.Entities.Store;
using Base.Identity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class DataContext : IdentityDbContext<User>, IDataContext
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

            modelBuilder.Entity<Category>();
            modelBuilder.Entity<FileData>()
                .HasIndex(x => x.FileID);
            modelBuilder.Entity<SubCategory>();
            modelBuilder.Entity<Item>();
            modelBuilder.Entity<SaleItem>();
            //modelBuilder.Entity<AccessLevel>();
            //modelBuilder.Entity<SpecialPermission>();
            //modelBuilder.Entity<RoleSpecialPermissions>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
