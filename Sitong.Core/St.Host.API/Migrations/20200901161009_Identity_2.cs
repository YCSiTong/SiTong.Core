using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace St.Host.API.Migrations
{
    public partial class Identity_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "APIManagement",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    LastModifierTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(type: "varchar(30)", nullable: false, comment: "功能名称"),
                    ApiUrl = table.Column<string>(type: "varchar(50)", nullable: false, comment: "接口地址"),
                    Description = table.Column<string>(type: "varchar(200)", nullable: false, comment: "详细说明"),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false, comment: "是否启用")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APIManagement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleAPIManagement",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    LastModifierTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "角色编号"),
                    APIId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "接口编号")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleAPIManagement", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "APIManagement");

            migrationBuilder.DropTable(
                name: "RoleAPIManagement");
        }
    }
}
