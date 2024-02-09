using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProClubsPlayerFinder.API.Migrations
{
    /// <inheritdoc />
    public partial class SeededDefaultUsersWithFreeAgentRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "19c543d9-cf25-41b2-a1d0-0838dfc0c2ec", null, "Player", "PLAYER" },
                    { "45ea90a9-22b4-40b3-9362-2eacc75a45bc", null, "Free Agent", "FREE AGENT" },
                    { "77ea7228-bcbd-4b43-8c7f-77db97f3c168", null, "Club Owner", "CLUB OWNER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ClubId", "ConcurrencyStamp", "Console", "Country", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "GamingPlatformAccountId", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "2ec68e2f-fa24-4abf-8bdf-d82f530adee0", 0, null, "1d624502-224e-4dc3-8df9-f486b0026dfa", "PS5", "Portugal", new DateTime(2003, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "bernastheking@gmail.com", false, "Bernardo", "Bernardo_S_14", "Santos", false, null, "BERNASTHEKING@GMAIL.COM", null, "AQAAAAIAAYagAAAAEB6arM0b1DFLZnfl36R+1xZ7rQTfXxdzakiaxkBfAT9cy03ZvZeM8/RiICd2SAK+WA==", null, false, "608f1c31-6e69-41ec-b1a9-d03a647d99a0", false, null },
                    { "71a8b6ac-6395-41d8-94d4-e103350110c0", 0, null, "0b37fdf2-c8eb-49b7-82e1-ac0f98fe4161", "PS5", "Portugal", new DateTime(2001, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "leogsantos5@gmail.com", false, "Leonardo", "leosantos2001", "Santos", false, null, "LEOGSANTOS5@GMAIL.COM", null, "AQAAAAIAAYagAAAAEE5tFlLF166FF13PqDpva6trUIkmO1YzPKUrGuEV+VCrkIVskjPOS8ILOKu5L8iq1g==", null, false, "c3e5c8e5-731b-49e0-988d-4fe64bc10abf", false, null },
                    { "7b867b03-fd4a-4410-a9fd-abdc32bda959", 0, null, "76ff8314-e88d-4ca6-adbd-f836b9ef2d4b", "PS5", "Portugal", new DateTime(2000, 11, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "ms2626242@gmail.com", false, "Marcelo", "Marcelocool181", "Silva", false, null, "MS2626242@GMAIL.COM", null, "AQAAAAIAAYagAAAAELUD4Dl3R+6CIW0fPTy7FElcViBwE39HTC9yx74QjgTsjO9mBX2/0jGO+Xb5oLWhLA==", null, false, "a522981d-971e-4628-8f2b-5850e60cb180", false, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "45ea90a9-22b4-40b3-9362-2eacc75a45bc", "2ec68e2f-fa24-4abf-8bdf-d82f530adee0" },
                    { "45ea90a9-22b4-40b3-9362-2eacc75a45bc", "71a8b6ac-6395-41d8-94d4-e103350110c0" },
                    { "45ea90a9-22b4-40b3-9362-2eacc75a45bc", "7b867b03-fd4a-4410-a9fd-abdc32bda959" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "19c543d9-cf25-41b2-a1d0-0838dfc0c2ec");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "77ea7228-bcbd-4b43-8c7f-77db97f3c168");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "45ea90a9-22b4-40b3-9362-2eacc75a45bc", "2ec68e2f-fa24-4abf-8bdf-d82f530adee0" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "45ea90a9-22b4-40b3-9362-2eacc75a45bc", "71a8b6ac-6395-41d8-94d4-e103350110c0" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "45ea90a9-22b4-40b3-9362-2eacc75a45bc", "7b867b03-fd4a-4410-a9fd-abdc32bda959" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45ea90a9-22b4-40b3-9362-2eacc75a45bc");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2ec68e2f-fa24-4abf-8bdf-d82f530adee0");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "71a8b6ac-6395-41d8-94d4-e103350110c0");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7b867b03-fd4a-4410-a9fd-abdc32bda959");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
