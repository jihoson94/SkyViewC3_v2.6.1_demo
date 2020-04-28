using Microsoft.EntityFrameworkCore;
using RobotStoreEntitiesLib;
using System.Linq;
namespace RobotStoreContextLib
{
    public class RobotStoreContext : DbContext
    {
        public RobotStoreContext(DbContextOptions<RobotStoreContext> options) : base(options)
        {
            Database.Migrate();
        }
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
            base.OnModelCreating(modelBuilder);

            #region User
            modelBuilder.Entity<User>().HasKey(u => u.UserID);
            modelBuilder.Entity<User>().Property(u => u.Name).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Email).HasMaxLength(100);
            modelBuilder.Entity<User>().Property(u => u.Password).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.IsDelete).HasDefaultValue(false);
            modelBuilder.Entity<User>()
                .HasOne<Grade>(u => u.Grade)
                .WithMany(g => g.Users)
                .HasForeignKey(u => u.GradeID);

            modelBuilder.Entity<User>()
                .HasMany<UserPermission>(u => u.Permissions)
                .WithOne(up => up.User)
                .HasForeignKey(up => up.UserID);
            #endregion

            #region UserHistory
            modelBuilder.Entity<UserHistory>().HasKey(uh => uh.UserHistoryID);
            modelBuilder.Entity<UserHistory>().HasOne<User>(uh => uh.User).WithMany().HasForeignKey(uh => uh.UserID);
            modelBuilder.Entity<UserHistory>().HasOne<Grade>(uh => uh.Grade).WithMany().HasForeignKey(uh => uh.GradeID);
            modelBuilder.Entity<UserHistory>().HasOne<User>(uh => uh.AddBy).WithMany();
            modelBuilder.Entity<UserHistory>().Property(uh => uh.AddDate).HasDefaultValueSql("getdate()");
            #endregion

            #region UserAction
            modelBuilder.Entity<UserAction>().HasKey(ua => ua.UserActionID);
            modelBuilder.Entity<UserAction>().HasOne<User>(ua => ua.User).WithMany().HasForeignKey(ua => ua.UserID);
            #endregion

            #region UserPermission
            modelBuilder.Entity<UserPermission>().HasKey(up => new { up.UserID, up.PermissionID });
            modelBuilder.Entity<UserPermission>()
                .HasOne<User>(up => up.User)
                .WithMany(u => u.Permissions)
                .HasForeignKey(up => up.UserID);

            modelBuilder.Entity<UserPermission>()
                .HasOne<Permission>(up => up.Permission)
                .WithMany()
                .HasForeignKey(p => p.PermissionID);
            #endregion

            #region UserPermissionHistory
            modelBuilder.Entity<UserPermissionHistory>().HasKey(uph => uph.UserPermissionHistoryID);
            modelBuilder.Entity<UserPermissionHistory>()
                .HasOne<User>(uph => uph.User)
                .WithMany()
                .HasForeignKey(uph => uph.UserID);

            modelBuilder.Entity<UserPermissionHistory>()
                .HasOne<Permission>(uph => uph.Permission)
                .WithMany()
                .HasForeignKey(uph => uph.PermissionID);

            modelBuilder.Entity<UserPermissionHistory>()
                .Property(uph => uph.Action)
                .HasMaxLength(10);
            modelBuilder.Entity<UserPermissionHistory>()
                .HasOne<User>(uph => uph.AddBy).WithMany();
            modelBuilder.Entity<UserPermissionHistory>()
                .Property(uph => uph.AddDate).HasDefaultValueSql("getdate()");

            #endregion

            #region Grade
            modelBuilder.Entity<Grade>().HasKey(g => g.GradeID);
            modelBuilder.Entity<Grade>()
                .HasMany<User>(g => g.Users)
                .WithOne(u => u.Grade)
                .HasForeignKey(u => u.GradeID);
            #endregion

            #region GradeInitialPermission
            modelBuilder.Entity<GradeInitialPermission>()
                .HasKey(g => new { g.GradeID, g.PermissionID });
            modelBuilder.Entity<GradeInitialPermission>()
                .HasOne<Grade>(gip => gip.Grade)
                .WithMany(g => g.Permissions)
                .HasForeignKey(gip => gip.GradeID);

            modelBuilder.Entity<GradeInitialPermission>()
                .HasOne<Permission>(gip => gip.Permission)
                .WithMany()
                .HasForeignKey(gip => gip.PermissionID);
            #endregion
        }
    }
}
