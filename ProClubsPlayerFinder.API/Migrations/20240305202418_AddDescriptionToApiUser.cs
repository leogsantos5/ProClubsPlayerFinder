using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProClubsPlayerFinder.API.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionToApiUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AspNetUsers",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2ec68e2f-fa24-4abf-8bdf-d82f530adee0",
                columns: new[] { "ConcurrencyStamp", "Description", "PasswordHash", "SecurityStamp" },
                values: new object[] { "410590f6-7bd8-430a-8ef7-f59bbbb6db46", null, "AQAAAAIAAYagAAAAEPNAcPx2MxCE639ceBcckJA1/1Ui9T/ih4/u4Qz/TDmEnG9ZIIKxZGkAWzlCAmlo4w==", "1c02d2bc-ac4e-4bf5-938c-cb73763bd049" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "71a8b6ac-6395-41d8-94d4-e103350110c0",
                columns: new[] { "ConcurrencyStamp", "Description", "PasswordHash", "SecurityStamp" },
                values: new object[] { "325c735b-7e2d-40d4-b271-ea29a0f0cdd7", null, "AQAAAAIAAYagAAAAEH7FhHWTWtEPKoQ4fUNiNkkrVgp3Od0tSydXrOVmZhOy/IfMMas3pfdZt3xyfSJ+BQ==", "8271aa10-de44-485d-9971-8aa65a91d28d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7b867b03-fd4a-4410-a9fd-abdc32bda959",
                columns: new[] { "ConcurrencyStamp", "Description", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f1911eaa-6135-46e4-84c1-08fd8e8fa695", null, "AQAAAAIAAYagAAAAEDKACLej6RycycZD9OV8Ge+3BLxIatt6cDthZJj4GDS8gjJVd8jkEwYyTD90VOtmXg==", "a2a13875-5793-4c28-9686-53acb649b057" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2ec68e2f-fa24-4abf-8bdf-d82f530adee0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "eb8611d0-89a4-43f2-9a8a-2d2f12dc00fc", "AQAAAAIAAYagAAAAEPErjwNr8OIKHtid7DNn+OCPSrCwJnSw9vJw1omvnUYtfcPsO7F/i/IY/MV2mW6+PA==", "851d1cc2-ed7a-4332-93ee-08637fed06a9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "71a8b6ac-6395-41d8-94d4-e103350110c0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0f1a47ff-aadc-4960-8fdc-0284d6df09b9", "AQAAAAIAAYagAAAAEKx1xPVACj3XawLXuVmwfh7qx/yu4rX8ljI7rKiYzXgWzCdDIJL7JiPY5lJc0/qjOQ==", "63375f83-86dd-4032-8f77-e5aa0d984353" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7b867b03-fd4a-4410-a9fd-abdc32bda959",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3a0e88bd-842f-4f16-99a2-4ff8ce726e70", "AQAAAAIAAYagAAAAED8cc4kBG4TyI8y5BcAtbL75Unsdg5l/P4fYkasM/30RTLcUHYIJrRDDHLL3XwTXRg==", "17e95833-d5ac-4a48-ac36-f1599a3db5ae" });
        }
    }
}
