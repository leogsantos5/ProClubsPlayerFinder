using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProClubsPlayerFinder.API.Migrations
{
    /// <inheritdoc />
    public partial class AddInvitesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Invites",
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
                    table.PrimaryKey("PK_Invites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invites_Clubs_Sender",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invites_Players_Receiver",
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

            migrationBuilder.CreateIndex(
                name: "IX_Invites_ApiUserId",
                table: "Invites",
                column: "ApiUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invites_ClubId",
                table: "Invites",
                column: "ClubId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invites");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2ec68e2f-fa24-4abf-8bdf-d82f530adee0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "410590f6-7bd8-430a-8ef7-f59bbbb6db46", "AQAAAAIAAYagAAAAEPNAcPx2MxCE639ceBcckJA1/1Ui9T/ih4/u4Qz/TDmEnG9ZIIKxZGkAWzlCAmlo4w==", "1c02d2bc-ac4e-4bf5-938c-cb73763bd049" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "71a8b6ac-6395-41d8-94d4-e103350110c0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "325c735b-7e2d-40d4-b271-ea29a0f0cdd7", "AQAAAAIAAYagAAAAEH7FhHWTWtEPKoQ4fUNiNkkrVgp3Od0tSydXrOVmZhOy/IfMMas3pfdZt3xyfSJ+BQ==", "8271aa10-de44-485d-9971-8aa65a91d28d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7b867b03-fd4a-4410-a9fd-abdc32bda959",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f1911eaa-6135-46e4-84c1-08fd8e8fa695", "AQAAAAIAAYagAAAAEDKACLej6RycycZD9OV8Ge+3BLxIatt6cDthZJj4GDS8gjJVd8jkEwYyTD90VOtmXg==", "a2a13875-5793-4c28-9686-53acb649b057" });
        }
    }
}
