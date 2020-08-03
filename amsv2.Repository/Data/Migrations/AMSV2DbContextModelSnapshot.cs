﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using amsv2.Repository;

namespace amsv2.Repository.Data.Migrations
{
    [DbContext(typeof(AMSV2DbContext))]
    partial class AMSV2DbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("amsv2.Model.Entitys.ModuleInfo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ModuleNO")
                        .HasColumnType("int");

                    b.Property<string>("ModuleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isEnable")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("ModuleInfo");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            ModuleNO = 1,
                            ModuleName = "系统管理_用户管理_新建",
                            isEnable = true
                        },
                        new
                        {
                            Id = 2L,
                            ModuleNO = 2,
                            ModuleName = "系统管理_用户管理_编辑",
                            isEnable = true
                        },
                        new
                        {
                            Id = 3L,
                            ModuleNO = 3,
                            ModuleName = "系统管理_用户管理_删除",
                            isEnable = true
                        },
                        new
                        {
                            Id = 4L,
                            ModuleNO = 4,
                            ModuleName = "系统管理_用户管理_修改密码",
                            isEnable = true
                        },
                        new
                        {
                            Id = 5L,
                            ModuleNO = 5,
                            ModuleName = "系统管理_用户管理_查看",
                            isEnable = true
                        },
                        new
                        {
                            Id = 6L,
                            ModuleNO = 6,
                            ModuleName = "系统管理_角色管理_查看",
                            isEnable = true
                        },
                        new
                        {
                            Id = 7L,
                            ModuleNO = 7,
                            ModuleName = "系统管理_角色管理_新增",
                            isEnable = true
                        },
                        new
                        {
                            Id = 8L,
                            ModuleNO = 8,
                            ModuleName = "系统管理_角色管理_编辑",
                            isEnable = true
                        },
                        new
                        {
                            Id = 9L,
                            ModuleNO = 9,
                            ModuleName = "系统管理_角色管理_删除",
                            isEnable = true
                        });
                });

            modelBuilder.Entity("amsv2.Model.Entitys.RoleInModule", b =>
                {
                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<long>("ModuleId")
                        .HasColumnType("bigint");

                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("RoleId", "ModuleId");

                    b.HasIndex("ModuleId");

                    b.ToTable("RoleInModule");

                    b.HasData(
                        new
                        {
                            RoleId = 1L,
                            ModuleId = 1L,
                            Id = 1L
                        },
                        new
                        {
                            RoleId = 1L,
                            ModuleId = 2L,
                            Id = 2L
                        },
                        new
                        {
                            RoleId = 1L,
                            ModuleId = 3L,
                            Id = 3L
                        },
                        new
                        {
                            RoleId = 1L,
                            ModuleId = 4L,
                            Id = 4L
                        },
                        new
                        {
                            RoleId = 1L,
                            ModuleId = 5L,
                            Id = 5L
                        },
                        new
                        {
                            RoleId = 1L,
                            ModuleId = 6L,
                            Id = 6L
                        },
                        new
                        {
                            RoleId = 1L,
                            ModuleId = 7L,
                            Id = 7L
                        },
                        new
                        {
                            RoleId = 1L,
                            ModuleId = 8L,
                            Id = 8L
                        },
                        new
                        {
                            RoleId = 1L,
                            ModuleId = 9L,
                            Id = 9L
                        });
                });

            modelBuilder.Entity("amsv2.Model.Entitys.RoleInfo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreateUserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<DateTime>("LastUpdateDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastUpdateUserName")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("RoleName")
                        .IsUnique();

                    b.ToTable("RoleInfo");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreateDateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreateUserName = "System",
                            LastUpdateDateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RoleName = "默认角色"
                        });
                });

            modelBuilder.Entity("amsv2.Model.Entitys.UserInModule", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long>("moduleId")
                        .HasColumnType("bigint");

                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("UserId", "moduleId");

                    b.HasIndex("moduleId");

                    b.ToTable("UserInModule");
                });

            modelBuilder.Entity("amsv2.Model.Entitys.UserInRole", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserInRole");

                    b.HasData(
                        new
                        {
                            UserId = 1512L,
                            RoleId = 1L,
                            Id = 1L
                        });
                });

            modelBuilder.Entity("amsv2.Model.Entitys.UserInfo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CreateUserID")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreateUserTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(14)")
                        .HasMaxLength(14);

                    b.Property<string>("UserPic")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.Property<string>("WorkUnit")
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("UserInfo");

                    b.HasData(
                        new
                        {
                            Id = 1511L,
                            CreateUserID = 0L,
                            CreateUserTime = new DateTime(2020, 8, 3, 14, 19, 39, 209, DateTimeKind.Local).AddTicks(2178),
                            Gender = 0,
                            Password = "123456",
                            Status = 0,
                            UserName = "admin",
                            UserType = 1
                        },
                        new
                        {
                            Id = 1512L,
                            CreateUserID = 0L,
                            CreateUserTime = new DateTime(2020, 8, 3, 14, 19, 39, 222, DateTimeKind.Local).AddTicks(2616),
                            Gender = 0,
                            Password = "123456",
                            Status = 0,
                            UserName = "rxf",
                            UserType = 0
                        });
                });

            modelBuilder.Entity("amsv2.Model.Entitys.RoleInModule", b =>
                {
                    b.HasOne("amsv2.Model.Entitys.ModuleInfo", "moduleInfo")
                        .WithMany("Roles")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("amsv2.Model.Entitys.RoleInfo", "roleInfo")
                        .WithMany("Modules")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("amsv2.Model.Entitys.UserInModule", b =>
                {
                    b.HasOne("amsv2.Model.Entitys.UserInfo", "User")
                        .WithMany("Modules")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("amsv2.Model.Entitys.ModuleInfo", "Module")
                        .WithMany("Users")
                        .HasForeignKey("moduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("amsv2.Model.Entitys.UserInRole", b =>
                {
                    b.HasOne("amsv2.Model.Entitys.RoleInfo", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("amsv2.Model.Entitys.UserInfo", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
