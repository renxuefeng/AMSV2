using amsv2.Common;
using amsv2.Model.Entitys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace amsv2.Repository
{
    public class AMSV2DbContext : DbContext
    {
        public AMSV2DbContext(DbContextOptions<AMSV2DbContext> options) : base(options)
        {
        }
        public virtual DbSet<UserInfo> UserInfo { get; set; }
        public virtual DbSet<ModuleInfo> ModuleInfo { get; set; }
        public virtual DbSet<RoleInfo> RoleInfo { get; set; }
        public virtual DbSet<RoleInModule> RoleInModule { get; set; }
        public virtual DbSet<UserInModule> UserInModule { get; set; }
        public virtual DbSet<UserInRole> UserInRole { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfo>()
            .HasIndex(b => b.UserName)
            .IsUnique();
            modelBuilder.Entity<RoleInfo>()
            .HasIndex(b => b.RoleName)
            .IsUnique();
            // 用户角色关联表
            modelBuilder.Entity<UserInRole>()
            .HasKey(pt => new { pt.UserId, pt.RoleId });
            modelBuilder.Entity<UserInRole>()
                .HasOne(pt => pt.User)
                .WithMany(p => p.Roles)
                .HasForeignKey(pt => pt.UserId);
            modelBuilder.Entity<UserInRole>()
                .HasOne(pt => pt.Role)
                .WithMany(t => t.Users)
                .HasForeignKey(pt => pt.RoleId);
            // 用户菜单权限关联表
            modelBuilder.Entity<UserInModule>()
            .HasKey(pt => new { pt.UserId, pt.moduleId });
            modelBuilder.Entity<UserInModule>()
                .HasOne(pt => pt.User)
                .WithMany(p => p.Modules)
                .HasForeignKey(pt => pt.UserId);
            modelBuilder.Entity<UserInModule>()
                .HasOne(pt => pt.Module)
                .WithMany(t => t.Users)
                .HasForeignKey(pt => pt.moduleId);
            // 角色菜单权限关联表
            modelBuilder.Entity<RoleInModule>()
            .HasKey(pt => new { pt.RoleId, pt.ModuleId });
            modelBuilder.Entity<RoleInModule>()
                .HasOne(pt => pt.roleInfo)
                .WithMany(p => p.Modules)
                .HasForeignKey(pt => pt.RoleId);
            modelBuilder.Entity<RoleInModule>()
                .HasOne(pt => pt.moduleInfo)
                .WithMany(t => t.Roles)
                .HasForeignKey(pt => pt.ModuleId);
            SeedData(modelBuilder);
        }
        /// <summary>
        /// 初始化种子数据
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void SeedData(ModelBuilder modelBuilder)
        {
            // 人员信息
            modelBuilder.Entity<UserInfo>().HasData(new UserInfo
            {
                Id = 1511,
                UserName = "admin",
                Password = "123456",
                UserType = (int)UserTypeEnum.超级管理员,
                Gender = (int)GenderType.男,
                Status = (int)UserStatus.启用,
                CreateUserID = 0000,
                CreateUserTime = DateTime.Now
            });
            modelBuilder.Entity<UserInfo>().HasData(new UserInfo
            {
                Id = 1512,
                UserName = "rxf",
                Password = "123456",
                UserType = (int)UserTypeEnum.普通用户,
                Gender = (int)GenderType.男,
                Status = (int)UserStatus.启用,
                CreateUserID = 0000,
                CreateUserTime = DateTime.Now,
            });
            // 角色信息
            modelBuilder.Entity<RoleInfo>().HasData(new RoleInfo
            {
                Id = 1,
                RoleName = "默认角色",
                CreateUser = "System",
            });
            // 权限菜单信息
            int id = 1;
            List<ModuleInfo> moduleInfos = new List<ModuleInfo>();
            foreach (ModulesType mt in Enum.GetValues(typeof(ModulesType)))
            {
                ModuleInfo moduleInfo = new ModuleInfo();
                moduleInfo.Id = id;
                moduleInfo.isEnable = true;
                moduleInfo.ModuleName = mt.ToString();
                moduleInfo.ModuleNO = (int)mt;
                moduleInfos.Add(moduleInfo);
                id++;
            }
            modelBuilder.Entity<ModuleInfo>().HasData(moduleInfos);
            // 角色菜单关联信息
            id = 1;
            List<RoleInModule> roleInModules = new List<RoleInModule>();
            foreach (ModulesType mt in Enum.GetValues(typeof(ModulesType)))
            {
                RoleInModule roleInModule = new RoleInModule();
                roleInModule.Id = id;
                roleInModule.RoleId = 1;
                roleInModule.ModuleId = id;
                roleInModules.Add(roleInModule);
                id++;
            }
            modelBuilder.Entity<RoleInModule>().HasData(roleInModules);

            // 用户角色关联信息
            modelBuilder.Entity<UserInRole>().HasData(
                new UserInRole()
                {
                    Id = 1,
                    UserId = 1512,
                    RoleId = 1
                }
            );
        }
    }
}
