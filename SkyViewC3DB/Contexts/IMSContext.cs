using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using SkyViewC3DB.Models;

namespace SkyViewC3DB.Contexts
{
    public class IMSContext : DbContext
    {

        public DbSet<User> Users { get; set; }

        public DbSet<GradeType> GradeTypes { get; set; }
        public DbSet<Grade> Grades { get; set; }

        public DbSet<PermissionType> PermissionTypes { get; set; }
        public DbSet<Permission> Permissions { get; set; }


        public IMSContext()
        {
            Database.EnsureCreated();
        }

        /// <summary>
        /// for Testing, constructor
        /// </summary>
        /// <param name="options"></param>
        public IMSContext(DbContextOptions<IMSContext> options)
        : base(options)
        { }

        /// <summary>
        /// DB Connection Setting
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string path = System.IO.Path.Combine(
                                System.Environment.CurrentDirectory, "SkyView.db"
                            );
                optionsBuilder.UseSqlite($"FileName={path}");
            }

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent API
        }
    }
}