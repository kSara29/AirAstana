using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class SeadedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_RoleId",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                table: "Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Flight",
                table: "Flight");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Role",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "Flight",
                newName: "Flights");

            migrationBuilder.RenameIndex(
                name: "IX_User_Username",
                table: "Users",
                newName: "IX_Users_Username");

            migrationBuilder.RenameIndex(
                name: "IX_User_RoleId",
                table: "Users",
                newName: "IX_Users_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Role_Code",
                table: "Roles",
                newName: "IX_Roles_Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Flights",
                table: "Flights",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "Id", "Arrival", "Departure", "Destination", "Origin", "Status" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2025, 9, 1, 11, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 5, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 9, 1, 9, 15, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 5, 0, 0, 0)), "Astana", "Almaty", "InTime" },
                    { 2, new DateTimeOffset(new DateTime(2025, 9, 1, 20, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 5, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 9, 1, 18, 45, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 5, 0, 0, 0)), "Almaty", "Astana", "InTime" },
                    { 3, new DateTimeOffset(new DateTime(2025, 9, 2, 8, 50, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 5, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 9, 2, 7, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 5, 0, 0, 0)), "Shymkent", "Almaty", "InTime" },
                    { 4, new DateTimeOffset(new DateTime(2025, 9, 2, 12, 20, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 5, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 9, 2, 10, 20, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 5, 0, 0, 0)), "Aktau", "Astana", "InTime" },
                    { 5, new DateTimeOffset(new DateTime(2025, 9, 3, 8, 40, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 5, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 9, 3, 6, 40, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 5, 0, 0, 0)), "Astana", "Atyrau", "Delayed" },
                    { 6, new DateTimeOffset(new DateTime(2025, 9, 3, 23, 55, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 4, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 9, 3, 22, 10, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 5, 0, 0, 0)), "Dubai", "Almaty", "InTime" },
                    { 7, new DateTimeOffset(new DateTime(2025, 9, 4, 14, 25, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 9, 4, 12, 35, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 5, 0, 0, 0)), "Istanbul", "Astana", "InTime" },
                    { 8, new DateTimeOffset(new DateTime(2025, 9, 5, 11, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 9, 5, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 5, 0, 0, 0)), "Frankfurt", "Almaty", "InTime" },
                    { 9, new DateTimeOffset(new DateTime(2025, 9, 6, 16, 20, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 9, 6, 13, 15, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 5, 0, 0, 0)), "London", "Astana", "Cancelled" },
                    { 10, new DateTimeOffset(new DateTime(2025, 9, 7, 10, 5, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 9, 7, 1, 20, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 5, 0, 0, 0)), "Seoul", "Almaty", "InTime" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Code" },
                values: new object[,]
                {
                    { 1, "Moderator" },
                    { 2, "User" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "RoleId", "Username" },
                values: new object[,]
                {
                    { 1, "AQAAAAEAACcQAAAAED4COiUJIz8MY1SzLargwTtdw5arAP+UUhe+8no3XCIg46HmVw4intrViB2Md8KWng==", 1, "moderator1" },
                    { 2, "AQAAAAEAACcQAAAAEFyoYQZZiOBXYt/uP1pKlq5zQLqgC6HF17T4IWzV3LJp/f6y0bdYB0HCZpIzx09Wvg==", 2, "user1" },
                    { 3, "AQAAAAEAACcQAAAAEEp7JWIfuPQY7jh3d38eyqfuKoe3m6Tt0I/jQwOcCcapq/C/2LuL5G2TrhPAKIVGTQ==", 2, "user2" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Flights",
                table: "Flights");

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Role");

            migrationBuilder.RenameTable(
                name: "Flights",
                newName: "Flight");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Username",
                table: "User",
                newName: "IX_User_Username");

            migrationBuilder.RenameIndex(
                name: "IX_Users_RoleId",
                table: "User",
                newName: "IX_User_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Roles_Code",
                table: "Role",
                newName: "IX_Role_Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                table: "Role",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Flight",
                table: "Flight",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_RoleId",
                table: "User",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
