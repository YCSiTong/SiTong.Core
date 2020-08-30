using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace St.Host.API.Migrations
{
    public partial class Identity_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    LastModifierTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(type: "varchar(30)", nullable: false, comment: "菜单名称"),
                    Url = table.Column<string>(type: "varchar(50)", nullable: false, comment: "ICon矢量图标"),
                    Icon = table.Column<string>(type: "varchar(20)", nullable: false, comment: "ICon矢量图标"),
                    SuperiorId = table.Column<string>(type: "varchar(100)", nullable: false, comment: "父级编号"),
                    OrderId = table.Column<int>(type: "int", nullable: false, comment: "排列顺序"),
                    IsLock = table.Column<bool>(type: "bit", nullable: false, comment: "是否锁定"),
                    MenuType = table.Column<int>(type: "int", nullable: false, comment: "菜单类型 2模块 4方法/接口一类"),
                    Description = table.Column<string>(type: "varchar(200)", nullable: false, comment: "详细说明")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    LastModifierTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(type: "varchar(40)", nullable: false, comment: "角色名称"),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false, comment: "是否管理员 True=是/Flase=不是")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleMenu",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    LastModifierTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "角色编号"),
                    MenuId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "菜单编号")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleMenu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    LastModifierTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Account = table.Column<string>(type: "varchar(50)", nullable: false, comment: "账户名"),
                    PassWord = table.Column<string>(type: "varchar(40)", nullable: false, comment: "密码"),
                    NickName = table.Column<string>(type: "varchar(26)", nullable: false, comment: "昵称"),
                    Sex = table.Column<bool>(type: "bit", nullable: false, comment: "性别,True=男/False=女"),
                    Email = table.Column<string>(type: "varchar(40)", nullable: false, comment: "邮箱账户"),
                    HeadPic = table.Column<string>(type: "varchar(150)", nullable: true, comment: "用户头像"),
                    PhoneNumber = table.Column<string>(type: "varchar(20)", nullable: false, comment: "手机号码"),
                    TwoFactorVerifyEnable = table.Column<bool>(type: "bit", nullable: false, comment: "是否开启双因子验证"),
                    IsFreeze = table.Column<bool>(type: "bit", nullable: false, comment: "是否冻结,True=冻结/False=未冻结")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    LastModifierTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "管理员主键"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "角色主键")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "RoleMenu");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "UserRole");
        }
    }
}
