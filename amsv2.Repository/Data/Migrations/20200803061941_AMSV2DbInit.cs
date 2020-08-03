using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace amsv2.Repository.Data.Migrations
{
    public partial class AMSV2DbInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ModuleInfo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuleNO = table.Column<int>(nullable: false),
                    ModuleName = table.Column<string>(nullable: false),
                    isEnable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleInfo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(maxLength: 20, nullable: false),
                    Description = table.Column<string>(maxLength: 20, nullable: true),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    CreateUserName = table.Column<string>(maxLength: 20, nullable: false),
                    LastUpdateDateTime = table.Column<DateTime>(nullable: false),
                    LastUpdateUserName = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserInfo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(maxLength: 14, nullable: false),
                    Password = table.Column<string>(maxLength: 32, nullable: true),
                    Name = table.Column<string>(maxLength: 20, nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    UserPic = table.Column<string>(maxLength: 100, nullable: true),
                    UserType = table.Column<int>(nullable: false),
                    WorkUnit = table.Column<string>(maxLength: 30, nullable: true),
                    CreateUserID = table.Column<long>(nullable: false),
                    CreateUserTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleInModule",
                columns: table => new
                {
                    RoleId = table.Column<long>(nullable: false),
                    ModuleId = table.Column<long>(nullable: false),
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleInModule", x => new { x.RoleId, x.ModuleId });
                    table.ForeignKey(
                        name: "FK_RoleInModule_ModuleInfo_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "ModuleInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleInModule_RoleInfo_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RoleInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserInModule",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false),
                    moduleId = table.Column<long>(nullable: false),
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInModule", x => new { x.UserId, x.moduleId });
                    table.ForeignKey(
                        name: "FK_UserInModule_UserInfo_UserId",
                        column: x => x.UserId,
                        principalTable: "UserInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserInModule_ModuleInfo_moduleId",
                        column: x => x.moduleId,
                        principalTable: "ModuleInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserInRole",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false),
                    RoleId = table.Column<long>(nullable: false),
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserInRole_RoleInfo_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RoleInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserInRole_UserInfo_UserId",
                        column: x => x.UserId,
                        principalTable: "UserInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ModuleInfo",
                columns: new[] { "Id", "ModuleNO", "ModuleName", "isEnable" },
                values: new object[,]
                {
                    { 1L, 1, "系统管理_用户管理_新建", true },
                    { 2L, 2, "系统管理_用户管理_编辑", true },
                    { 3L, 3, "系统管理_用户管理_删除", true },
                    { 4L, 4, "系统管理_用户管理_修改密码", true },
                    { 5L, 5, "系统管理_用户管理_查看", true },
                    { 6L, 6, "系统管理_角色管理_查看", true },
                    { 7L, 7, "系统管理_角色管理_新增", true },
                    { 8L, 8, "系统管理_角色管理_编辑", true },
                    { 9L, 9, "系统管理_角色管理_删除", true }
                });

            migrationBuilder.InsertData(
                table: "RoleInfo",
                columns: new[] { "Id", "CreateDateTime", "CreateUserName", "Description", "LastUpdateDateTime", "LastUpdateUserName", "RoleName" },
                values: new object[] { 1L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "默认角色" });

            migrationBuilder.InsertData(
                table: "UserInfo",
                columns: new[] { "Id", "CreateUserID", "CreateUserTime", "Gender", "Name", "Password", "Status", "UserName", "UserPic", "UserType", "WorkUnit" },
                values: new object[,]
                {
                    { 1511L, 0L, new DateTime(2020, 8, 3, 14, 19, 39, 209, DateTimeKind.Local).AddTicks(2178), 0, null, "123456", 0, "admin", null, 1, null },
                    { 1512L, 0L, new DateTime(2020, 8, 3, 14, 19, 39, 222, DateTimeKind.Local).AddTicks(2616), 0, null, "123456", 0, "rxf", null, 0, null }
                });

            migrationBuilder.InsertData(
                table: "RoleInModule",
                columns: new[] { "RoleId", "ModuleId", "Id" },
                values: new object[,]
                {
                    { 1L, 1L, 1L },
                    { 1L, 2L, 2L },
                    { 1L, 3L, 3L },
                    { 1L, 4L, 4L },
                    { 1L, 5L, 5L },
                    { 1L, 6L, 6L },
                    { 1L, 7L, 7L },
                    { 1L, 8L, 8L },
                    { 1L, 9L, 9L }
                });

            migrationBuilder.InsertData(
                table: "UserInRole",
                columns: new[] { "UserId", "RoleId", "Id" },
                values: new object[] { 1512L, 1L, 1L });

            migrationBuilder.CreateIndex(
                name: "IX_RoleInfo_RoleName",
                table: "RoleInfo",
                column: "RoleName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleInModule_ModuleId",
                table: "RoleInModule",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_UserName",
                table: "UserInfo",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserInModule_moduleId",
                table: "UserInModule",
                column: "moduleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInRole_RoleId",
                table: "UserInRole",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleInModule");

            migrationBuilder.DropTable(
                name: "UserInModule");

            migrationBuilder.DropTable(
                name: "UserInRole");

            migrationBuilder.DropTable(
                name: "ModuleInfo");

            migrationBuilder.DropTable(
                name: "RoleInfo");

            migrationBuilder.DropTable(
                name: "UserInfo");
        }
    }
}
