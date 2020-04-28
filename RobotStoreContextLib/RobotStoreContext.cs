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

        #region DbSet About User
        public DbSet<User> Users;
        public DbSet<UserHistory> UserHistories;
        public DbSet<Permission> Permissions;
        public DbSet<UserPermission> UserPermissions;
        public DbSet<UserPermissionHistory> UserPermissionHistories;
        public DbSet<Grade> Grades;
        public DbSet<GradeInitialPermission> GradeInitialPermissions;
        public DbSet<UserAction> UserActions;

        #endregion

        #region DbSet About IMS
        public DbSet<Rack> Racks;
        public DbSet<RackType> RackType;
        public DbSet<Box> Boxes;
        public DbSet<BoxType> BoxTypes;
        public DbSet<BoxHistory> BoxHistories;
        public DbSet<Vial> Vials;
        public DbSet<VialType> VialTypes;
        public DbSet<VialHistory> VialHistories;
        public DbSet<SpaceOwnership> SpaceOwnerships;
        public DbSet<SpaceOwnerShipHistory> SpaceOwnerShipHistories;
        #endregion

        #region DbSet About Config
        DbSet<TankConfig> TankConfigs;
        DbSet<TankConfigHistory> TankConfigHistories;
        DbSet<SystemConfig> SystemConfigs;
        DbSet<SystemConfigHistory> SystemConfigHistories;
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region DB Configure About User

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

            #endregion

            #region DB Configure About IMS

            #region Rack
            modelBuilder.Entity<Rack>().HasKey(r => r.RackID);
            modelBuilder.Entity<Rack>()
                .HasOne<RackType>(r => r.RackType)
                .WithMany()
                .HasForeignKey(r => r.RackTypeName);

            modelBuilder.Entity<Rack>()
                .HasMany<Box>(r => r.Boxes)
                .WithOne(b => b.Rack)
                .HasForeignKey(b => b.RackID);
            #endregion
            #region RackType
            modelBuilder.Entity<RackType>().HasKey(rt => rt.Name);
            #endregion

            #region Box
            modelBuilder.Entity<Box>().HasKey(b => b.BoxID);
            modelBuilder.Entity<Box>()
                .HasOne(b => b.Rack)
                .WithMany(r => r.Boxes)
                .HasForeignKey(b => b.RackID);
            modelBuilder.Entity<Box>()
                .HasOne(b => b.BoxType)
                .WithMany()
                .HasForeignKey(b => b.BoxTypeName);
            modelBuilder.Entity<Box>()
                .HasMany(b => b.Vials)
                .WithOne(v => v.Box)
                .HasForeignKey(v => v.BoxID);
            #endregion
            #region BoxType
            modelBuilder.Entity<BoxType>().HasKey(bt => bt.Name);
            #endregion
            #region BoxHistory
            modelBuilder.Entity<BoxHistory>().HasKey(bh => bh.BoxHistoryID);
            modelBuilder.Entity<BoxHistory>().HasOne<Box>(bh => bh.Box).WithMany().HasForeignKey(bh => bh.BoxID);
            modelBuilder.Entity<BoxHistory>().HasOne<Rack>(bh => bh.Rack).WithMany().HasForeignKey(bh => bh.RackID);
            modelBuilder.Entity<BoxHistory>().HasOne<BoxType>(bh => bh.BoxType).WithMany().HasForeignKey(bh => bh.BoxTypeName);
            modelBuilder.Entity<BoxHistory>().HasOne<User>(bh => bh.AddBy).WithMany();
            modelBuilder.Entity<BoxHistory>().Property(bh => bh.AddDate).HasDefaultValueSql("getdate()");
            #endregion

            #region Vial
            modelBuilder.Entity<Vial>().HasKey(v => v.VialID);
            modelBuilder.Entity<Vial>().HasOne<Box>(v => v.Box).WithMany(b => b.Vials).HasForeignKey(v => v.BoxID);
            modelBuilder.Entity<Vial>().HasOne<VialType>(v => v.VialType).WithMany().HasForeignKey(v => v.VialTypeName);
            #endregion
            #region VialType
            modelBuilder.Entity<VialType>().HasKey(vt => vt.Name);
            #endregion
            #region VialHistory
            modelBuilder.Entity<VialHistory>().HasKey(vh => vh.VialHistoryID);
            modelBuilder.Entity<VialHistory>().HasOne<Vial>(vh => vh.Vial).WithMany().HasForeignKey(vh => vh.VialID);
            modelBuilder.Entity<VialHistory>().HasOne<Box>(vh => vh.Box).WithMany().HasForeignKey(vh => vh.BoxID);
            modelBuilder.Entity<VialHistory>().HasOne<VialType>(vh => vh.VialType).WithMany().HasForeignKey(vh => vh.VialTypeName);
            modelBuilder.Entity<VialHistory>().HasOne<User>(vh => vh.AddBy).WithMany();
            modelBuilder.Entity<VialHistory>().Property(vh => vh.AddDate).HasDefaultValueSql("getdate()");
            #endregion

            #region SpaceOwnerShip
            modelBuilder.Entity<SpaceOwnership>().HasKey(sos => new { sos.RackID, sos.Slot, sos.UserID });
            modelBuilder.Entity<SpaceOwnership>().HasOne(sos => sos.Rack).WithMany().HasForeignKey(sos => sos.RackID);
            modelBuilder.Entity<SpaceOwnership>().HasOne(sos => sos.User).WithMany().HasForeignKey(sos => sos.UserID);
            #endregion
            #region SpaceOwnerShipHistory
            modelBuilder.Entity<SpaceOwnerShipHistory>().HasKey(sosh => sosh.SpaceOwnerShipHistoryID);
            modelBuilder.Entity<SpaceOwnerShipHistory>().HasOne<Rack>(sosh => sosh.Rack).WithMany().HasForeignKey(sosh => sosh.RackID);
            modelBuilder.Entity<SpaceOwnerShipHistory>().HasOne<User>(sosh => sosh.User).WithMany().HasForeignKey(sosh => sosh.UserID);
            modelBuilder.Entity<SpaceOwnerShipHistory>().HasOne<User>(sosh => sosh.AddBy).WithMany();
            modelBuilder.Entity<SpaceOwnerShipHistory>().Property(sosh => sosh.AddDate).HasDefaultValueSql("getdate()");
            #endregion

            #endregion

            #region DB Configure About Configuration

            #region TankConfig
            modelBuilder.Entity<TankConfig>().HasKey(tc => tc.ConfigName);
            #endregion

            #region TankConfigHistory
            modelBuilder.Entity<TankConfigHistory>().HasKey(tch => tch.TankConfigHistoryID);
            modelBuilder.Entity<TankConfigHistory>().HasOne<User>(tch => tch.AddBy).WithMany();
            modelBuilder.Entity<TankConfigHistory>().Property(tch => tch.AddDate).HasDefaultValueSql("getdate()");
            #endregion

            #region SystemConfig
            modelBuilder.Entity<SystemConfig>().HasKey(sc => sc.ConfigName);
            #endregion

            #region SystemConfigHistory
            modelBuilder.Entity<SystemConfigHistory>().HasKey(sch => sch.SystemConfigHistoryID);
            modelBuilder.Entity<SystemConfigHistory>().HasOne<User>(sch => sch.AddBy).WithMany();
            modelBuilder.Entity<SystemConfigHistory>().Property(sch => sch.AddDate).HasDefaultValueSql("getdate()");
            #endregion

            #endregion
        }
    }
}
