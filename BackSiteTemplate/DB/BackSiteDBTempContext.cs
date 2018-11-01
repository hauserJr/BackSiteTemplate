using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BackSiteTemplate.DB
{
    public partial class BackSiteDBTempContext : DbContext
    {
        public BackSiteDBTempContext()
        {
        }

        public BackSiteDBTempContext(DbContextOptions<BackSiteDBTempContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountList> AccountList { get; set; }
        public virtual DbSet<AjaxFilterLogs> AjaxFilterLogs { get; set; }
        public virtual DbSet<AjaxFunctionList> AjaxFunctionList { get; set; }
        public virtual DbSet<ErrorTimes> ErrorTimes { get; set; }
        public virtual DbSet<LoginLogs> LoginLogs { get; set; }
        public virtual DbSet<MainNavs> MainNavs { get; set; }
        public virtual DbSet<PageFilterLogs> PageFilterLogs { get; set; }
        public virtual DbSet<RoleGroup> RoleGroup { get; set; }
        public virtual DbSet<RoleScope> RoleScope { get; set; }
        public virtual DbSet<SubNavs> SubNavs { get; set; }
        public virtual DbSet<UserRoleList> UserRoleList { get; set; }
        public virtual DbSet<WhiteList> WhiteList { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=BackSiteDBTemp;Integrated Security=False;User ID=sa;Password=1qaz2wsx;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountList>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AccountEnable)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LoginAccount)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LoginPwd)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.OnLock)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.SuperAdmin).HasDefaultValueSql("((0))");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<AjaxFilterLogs>(entity =>
            {
                entity.Property(e => e.ClientIp)
                    .IsRequired()
                    .HasColumnName("ClientIP")
                    .HasMaxLength(25);

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Para).IsRequired();

                entity.Property(e => e.PublicIp)
                    .IsRequired()
                    .HasColumnName("PublicIP")
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<AjaxFunctionList>(entity =>
            {
                entity.Property(e => e.AjaxAction)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AjaxController)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Note)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<ErrorTimes>(entity =>
            {
                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ErrorTimes1).HasColumnName("ErrorTimes");
            });

            modelBuilder.Entity<LoginLogs>(entity =>
            {
                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LoginTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<MainNavs>(entity =>
            {
                entity.Property(e => e.MainNavsName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Sn).HasColumnName("SN");
            });

            modelBuilder.Entity<PageFilterLogs>(entity =>
            {
                entity.Property(e => e.ClientIp)
                    .HasColumnName("ClientIP")
                    .HasMaxLength(25);

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PublicIp)
                    .HasColumnName("PublicIP")
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<RoleGroup>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.GroupEnable)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Sn).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<RoleScope>(entity =>
            {
                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.RoleGroup)
                    .WithMany(p => p.RoleScope)
                    .HasForeignKey(d => d.RoleGroupId)
                    .HasConstraintName("FK_UserRoleScope_RoleGroup");

                entity.HasOne(d => d.SubNavs)
                    .WithMany(p => p.RoleScope)
                    .HasForeignKey(d => d.SubNavsId)
                    .HasConstraintName("FK_UserRoleScope_SubNavs");
            });

            modelBuilder.Entity<SubNavs>(entity =>
            {
                entity.Property(e => e.ActionName).HasMaxLength(50);

                entity.Property(e => e.ControllerName).HasMaxLength(50);

                entity.Property(e => e.Sn).HasColumnName("SN");

                entity.Property(e => e.SubNavsName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.MainNavs)
                    .WithMany(p => p.SubNavs)
                    .HasForeignKey(d => d.MainNavsId)
                    .HasConstraintName("FK_SubNavs_MainNavs");
            });

            modelBuilder.Entity<UserRoleList>(entity =>
            {
                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.RoleGroup)
                    .WithMany(p => p.UserRoleList)
                    .HasForeignKey(d => d.RoleGroupId)
                    .HasConstraintName("FK_UserRoleList_RoleGroup");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoleList)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserRoleList_UserRoleList");
            });

            modelBuilder.Entity<WhiteList>(entity =>
            {
                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Ip)
                    .IsRequired()
                    .HasColumnName("IP")
                    .HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(50);
            });
        }
    }
}
