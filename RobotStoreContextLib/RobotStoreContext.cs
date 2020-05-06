using Microsoft.EntityFrameworkCore;
using RobotStoreEntitiesLib;
using System;
using System.Linq;
namespace RobotStoreContextLib
{
    public class RobotStoreContext : DbContext
    {
        public RobotStoreContext(DbContextOptions<RobotStoreContext> options) : base(options)
        {
            // Database.Migrate();
        }

        #region DbSet About User
        public DbSet<User> Users { get; set; }
        public DbSet<UserHistory> UserHistories { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<UserPermissionHistory> UserPermissionHistories { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<GradeInitialPermission> GradeInitialPermissions { get; set; }
        public DbSet<UserAction> UserActions { get; set; }

        #endregion

        #region DbSet About IMS
        public DbSet<Rack> Racks { get; set; }
        public DbSet<RackType> RackType { get; set; }
        public DbSet<Box> Boxes { get; set; }
        public DbSet<BoxType> BoxTypes { get; set; }
        public DbSet<BoxHistory> BoxHistories { get; set; }
        public DbSet<Vial> Vials { get; set; }
        public DbSet<VialType> VialTypes { get; set; }
        public DbSet<VialHistory> VialHistories { get; set; }
        public DbSet<SpaceOwnership> SpaceOwnerships { get; set; }
        public DbSet<SpaceOwnerShipHistory> SpaceOwnerShipHistories { get; set; }
        #endregion

        #region DbSet About Config
        public DbSet<TankConfig> TankConfigs { get; set; }
        public DbSet<TankConfigHistory> TankConfigHistories { get; set; }
        public DbSet<SystemConfig> SystemConfigs { get; set; }
        public DbSet<SystemConfigHistory> SystemConfigHistories { get; set; }
        #endregion

        #region DbSet About Calibration
        public DbSet<LN2LevelCalibration> LN2LevelCalibrations { get; set; }
        public DbSet<LN2LevelCalibratoinHistory> LN2LevelCalibratoinHistories { get; set; }
        public DbSet<TopTempCalibration> TopTempCalibrations { get; set; }
        public DbSet<TopTempCalibrationHistory> topTempCalibrationHistories { get; set; }
        public DbSet<BottomTempCalibration> BottomTempCalibrations { get; set; }
        public DbSet<BottomTempCalibrationHistory> BottomTempCalibrationHistories { get; set; }
        public DbSet<ByPassTempCalibration> ByPassTempCalibrations { get; set; }
        public DbSet<ByPassTempCalibrationHistory> ByPassTempCalibrationHistories { get; set; }
        #endregion

        #region DbSet About Log
        public DbSet<TankStatusLog> TankStatusLogs { get; set; }
        public DbSet<AlarmLog> AlarmLogs { get; set; }
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
            modelBuilder.Entity<UserHistory>().Property(uh => uh.AddDate).HasDefaultValueSql("date('now')");
            #endregion

            #region UserAction
            modelBuilder.Entity<UserAction>().HasKey(ua => ua.UserActionID);
            modelBuilder.Entity<UserAction>().HasOne<User>(ua => ua.User).WithMany().HasForeignKey(ua => ua.UserID);
            #endregion

            #region Permission
            modelBuilder.Entity<Permission>().HasKey(p => p.PermissionID);
            initPermission(modelBuilder);
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
                .Property(uph => uph.AddDate).HasDefaultValueSql("date('now')");

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
            modelBuilder.Entity<BoxHistory>().Property(bh => bh.AddDate).HasDefaultValueSql("date('now')");
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
            modelBuilder.Entity<VialHistory>().Property(vh => vh.AddDate).HasDefaultValueSql("date('now')");
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
            modelBuilder.Entity<SpaceOwnerShipHistory>().Property(sosh => sosh.AddDate).HasDefaultValueSql("date('now')");
            #endregion

            #endregion

            #region DB Configure About Configuration

            #region TankConfig
            modelBuilder.Entity<TankConfig>().HasKey(tc => tc.ConfigName);
            #endregion

            #region TankConfigHistory
            modelBuilder.Entity<TankConfigHistory>().HasKey(tch => tch.TankConfigHistoryID);
            modelBuilder.Entity<TankConfigHistory>().HasOne<User>(tch => tch.AddBy).WithMany();
            modelBuilder.Entity<TankConfigHistory>().Property(tch => tch.AddDate).HasDefaultValueSql("date('now')");
            #endregion

            #region SystemConfig
            modelBuilder.Entity<SystemConfig>().HasKey(sc => sc.ConfigName);
            #endregion

            #region SystemConfigHistory
            modelBuilder.Entity<SystemConfigHistory>().HasKey(sch => sch.SystemConfigHistoryID);
            modelBuilder.Entity<SystemConfigHistory>().HasOne<User>(sch => sch.AddBy).WithMany();
            modelBuilder.Entity<SystemConfigHistory>().Property(sch => sch.AddDate).HasDefaultValueSql("date('now')");
            #endregion

            #endregion

            #region DB Configure About Calibration

            #region LN2LevelCalibration
            modelBuilder.Entity<LN2LevelCalibration>().HasKey(c => new { c.Reference, c.Value });
            #endregion
            #region LN2LevelCalibrationHistory
            modelBuilder.Entity<LN2LevelCalibratoinHistory>().HasKey(h => h.LN2LevelCalibratoinHistoryID);
            modelBuilder.Entity<LN2LevelCalibratoinHistory>().HasOne(h => h.AddBy).WithMany();
            modelBuilder.Entity<LN2LevelCalibratoinHistory>().Property(h => h.AddDate).HasDefaultValueSql("date('now')");
            #endregion

            #region TopTempCalibration
            modelBuilder.Entity<TopTempCalibration>().HasKey(c => new { c.Reference, c.Value });
            #endregion
            #region TopTempCalibrationHistory
            modelBuilder.Entity<TopTempCalibrationHistory>().HasKey(h => h.TopTempCalibrationHistoryID);
            modelBuilder.Entity<TopTempCalibrationHistory>().HasOne(h => h.AddBy).WithMany();
            modelBuilder.Entity<TopTempCalibrationHistory>().Property(h => h.AddDate).HasDefaultValueSql("date('now')");
            #endregion 

            #region BottomTempCalibration
            modelBuilder.Entity<BottomTempCalibration>().HasKey(c => new { c.Reference, c.Value });
            #endregion
            #region BottomTempCalibrationHistory
            modelBuilder.Entity<BottomTempCalibrationHistory>().HasKey(h => h.BottomTempCalibrationHistoryID);
            modelBuilder.Entity<BottomTempCalibrationHistory>().HasOne(h => h.AddBy).WithMany();
            modelBuilder.Entity<BottomTempCalibrationHistory>().Property(h => h.AddDate).HasDefaultValueSql("date('now')");
            #endregion

            #region ByPassCalibration
            modelBuilder.Entity<ByPassTempCalibration>().HasKey(c => new { c.Reference, c.Value });
            #endregion
            #region ByPassCalibrationHistory
            modelBuilder.Entity<ByPassTempCalibrationHistory>().HasKey(h => h.ByPassTempCalibrationHistoryID);
            modelBuilder.Entity<ByPassTempCalibrationHistory>().HasOne(h => h.AddBy).WithMany();
            modelBuilder.Entity<ByPassTempCalibrationHistory>().Property(h => h.AddDate).HasDefaultValueSql("date('now')");
            #endregion

            #endregion

            #region DB Configure About Log

            #region TankStatusLog
            modelBuilder.Entity<TankStatusLog>().HasKey(tl => tl.TankStatusLogID);
            modelBuilder.Entity<TankStatusLog>().Property(tl => tl.Created).HasDefaultValueSql("date('now')");
            #endregion

            #region AlarmLog
            modelBuilder.Entity<AlarmLog>().HasKey(al => al.AlarmLogID);
            modelBuilder.Entity<AlarmLog>().Property(al => al.Created).HasDefaultValueSql("date('now')");
            modelBuilder.Entity<AlarmLog>().Property(al => al.AlarmCode).IsRequired();
            #endregion

            #endregion
        }

        private void initPermission(ModelBuilder modelBuilder)
        {
            // TODO: Add Permissions.
            modelBuilder.Entity<Permission>().HasData(
                new Permission[]{
                    new Permission() { PermissionID = "SuperAdmin" },
                });
        }
    }
}
