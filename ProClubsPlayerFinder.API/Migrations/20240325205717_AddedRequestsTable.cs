using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProClubsPlayerFinder.API.Migrations
{
    /// <inheritdoc />
    public partial class AddedRequestsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApiUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClubId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Pending")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requests_Clubs_Receiver",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Requests_Players_Sender",
                        column: x => x.ApiUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2ec68e2f-fa24-4abf-8bdf-d82f530adee0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "feee9db8-7bf8-480a-9ebc-90a3ed76929d", "AQAAAAIAAYagAAAAEGnmr50ZTYYLtndEMLtSvjjArpTftIuWo7ZXJ4tN2QkBXQQ/QVJ3fofsdSqxNQtx1g==", "5036e883-1e45-48ee-92eb-38600f3e216f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "71a8b6ac-6395-41d8-94d4-e103350110c0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b10f3d74-2a9d-4f5e-9691-81f253f45427", "AQAAAAIAAYagAAAAEPy3uMQncRABx4Ybw9a54pW8Idt4VSdgfahPQHyH6rYIrIEnSe2ENTsD3Rd1mtlW3Q==", "681db98f-fbf6-42c4-9a89-7ea19562c4fc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7b867b03-fd4a-4410-a9fd-abdc32bda959",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d23ce6ea-26f0-45a9-b2d1-5f8d22f54879", "AQAAAAIAAYagAAAAEBx1tpIZAykmcEjhHZLMU1nIPducKkNYA0T6byjz3V36RO7EYpxSI2RziBv69BShSw==", "a51bf218-cae7-464e-be63-dae255ef3cff" });

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ApiUserId",
                table: "Requests",
                column: "ApiUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ClubId",
                table: "Requests",
                column: "ClubId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2ec68e2f-fa24-4abf-8bdf-d82f530adee0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c0d81856-e99f-4197-8e7f-c4e00553c267", "AQAAAAIAAYagAAAAEFfhThIGVXAE2ADA1PpqsWhzI9sPr3PdtXFtJH7QoHPAGwgMzSyaMuIwZxHR5U3MUQ==", "1875d1b2-a2ba-4650-94d6-1ef2133d010a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "71a8b6ac-6395-41d8-94d4-e103350110c0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2d251b27-0d60-4be2-965d-32a79dcbc977", "AQAAAAIAAYagAAAAENEUvKAzE34w/t8CxSUQxSHMu8tb08w6jHDzdswbDOf83d8HqUDifibkuDcAoWgGNg==", "774d0be8-4953-450a-9932-84f7105459e1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7b867b03-fd4a-4410-a9fd-abdc32bda959",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ba4ec34e-63f9-4341-bea9-bdad12d0c2ba", "AQAAAAIAAYagAAAAEEdUIXX8zK1Hjw0a8XCSeb8SULErGpE+TqKpUwYKTnnVSzuH68liAVJO8YPF6TdOUA==", "25643341-c14c-4377-a39f-2784241031c1" });
        }
    }
}
