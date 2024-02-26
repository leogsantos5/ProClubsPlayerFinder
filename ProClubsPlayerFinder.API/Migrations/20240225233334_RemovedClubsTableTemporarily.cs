using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProClubsPlayerFinder.API.Migrations
{
    /// <inheritdoc />
    public partial class RemovedClubsTableTemporarily : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Players_Clubs",
            //    table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Clubs");

            //migrationBuilder.DropIndex(
            //    name: "IX_AspNetUsers_ClubId",
            //    table: "AspNetUsers");

            //migrationBuilder.DropColumn(
            //    name: "ClubId",
            //    table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2ec68e2f-fa24-4abf-8bdf-d82f530adee0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b44f931a-5605-452a-85b8-2b86160d9a99", "AQAAAAIAAYagAAAAEJIz5ZQlCwmp5aQmOh8DJYw+ZaSdL024tKahWOKy00IeRDnlSQvodPLVA1fG6xmiPg==", "b110c464-d663-4909-a9e7-08dc81329c77" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "71a8b6ac-6395-41d8-94d4-e103350110c0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3222a14e-26cd-4431-bac5-5b100812f020", "AQAAAAIAAYagAAAAEBrlGXcUQPhYRbhtczbip4zjwjSEs/24kvrc7g69+N10fNX5PNB+lK9HVrvuA30rlQ==", "b3c2b730-302f-4ee9-ac71-10c1f5e4c61d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7b867b03-fd4a-4410-a9fd-abdc32bda959",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a19aa28c-90d1-4f6e-9516-68f582473db7", "AQAAAAIAAYagAAAAEAeSpvTiuUU7/v0VL306k6tRwjVc7rFtPUi/FxzOVv5BUDst+N5c0hNAbogrgSX4Yg==", "cb950bad-a128-4eed-b872-7089d25124f8" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClubId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Clubs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    OwnerPlayerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClubName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Console = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Clubs__D35058E7947E6D35", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Clubs__OwnerPlay__52593CB8",
                        column: x => x.OwnerPlayerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2ec68e2f-fa24-4abf-8bdf-d82f530adee0",
                columns: new[] { "ClubId", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { null, "f956be7d-cc63-459f-b4e5-edd5018900f4", "AQAAAAIAAYagAAAAEPo9LcS32rSfoLlyjY+jAKiSDzG9H17f1jN0+0y1vHCkuNRzmS5rHN5p/+fEX4DYoA==", "527c862a-3819-4f13-8e09-e4508f895cb8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "71a8b6ac-6395-41d8-94d4-e103350110c0",
                columns: new[] { "ClubId", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { null, "f389bd46-3df9-4959-b2f0-0f5239a31ba6", "AQAAAAIAAYagAAAAEGXyz1KX4pwrBsfRIGXAHj5sasuXoTZQBoKCUp6E4kjl6zoCHimUMbr5gxVztk5whQ==", "5c1d6e10-73eb-4331-8744-a59eca818fbe" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7b867b03-fd4a-4410-a9fd-abdc32bda959",
                columns: new[] { "ClubId", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { null, "6fe61217-b9fc-43bf-8843-61f0591f67ba", "AQAAAAIAAYagAAAAEDDmAw7zssvGTp+zLcmBjBFbRW0zqAvFyoYNcT8TBTo5CH801p8aMvL0z/+mLSjYXA==", "ae10a0db-c9f9-4858-bfeb-d893badae563" });

            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetUsers_ClubId",
            //    table: "AspNetUsers",
            //    column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_Clubs_OwnerPlayerId",
                table: "Clubs",
                column: "OwnerPlayerId",
                unique: true);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Players_Clubs",
            //    table: "AspNetUsers",
            //    column: "ClubId",
            //    principalTable: "Clubs",
            //    principalColumn: "Id");
        }
    }
}
