using Microsoft.EntityFrameworkCore;
using RobotStoreEntitiesLib;

namespace RobotStoreContextLib
{
    public class RobotStoreContext : DbContext
    {
        public DbSet<User> Users;
        public DbSet<UserHistory> UserHistories;
        public DbSet<Permission> Permissions;
        public DbSet<UserPermission> UserPermissions;
        public DbSet<UserPermissionHistory> UserPermissionHistories;
        public DbSet<Grade> Grades;
        public DbSet<GradeInitialPermission> GradeInitialPermissions;
        public DbSet<UserAction> UserActions;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // GradeInitialPermission MultiKey
            modelBuilder.Entity<GradeInitialPermission>().HasKey(g => new { g.Grade, g.Permission });
        }
    }
}
