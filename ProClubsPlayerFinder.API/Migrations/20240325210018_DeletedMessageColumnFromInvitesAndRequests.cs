using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProClubsPlayerFinder.API.Migrations
{
    /// <inheritdoc />
    public partial class DeletedMessageColumnFromInvitesAndRequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Message",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "Invites");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2ec68e2f-fa24-4abf-8bdf-d82f530adee0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4c2ac97c-8827-4fad-8ff8-386ff225bc85", "AQAAAAIAAYagAAAAEG6EHHF+4sVkfXtAhBpKSlH5AwARhvNogNQDSbG3uKdpyF5rkWlILcnAGGzKtD59jg==", "629f0e78-0bab-461b-a45f-5307c97cc06c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "71a8b6ac-6395-41d8-94d4-e103350110c0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5c23db3c-b46d-499a-be63-8dd3a186b9b8", "AQAAAAIAAYagAAAAEH5+OQd6faZiU1uriouuuIMVkV8xffm4v1ZSxkjsdMtoY1iGH3bgjDWPrbaf1BXKgQ==", "1d5fdc0c-7d44-431d-9bcc-00084375b4e1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7b867b03-fd4a-4410-a9fd-abdc32bda959",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "70c71c72-94d5-4a95-af32-ff7ae6ad79d0", "AQAAAAIAAYagAAAAEJD6QUUcXAd/UDYDYtOo39QMf1aipqMr1CA0sWfKWZfzfZ+pxGWL9BG/qnbgJBMhBQ==", "6bc4b35d-3f59-40f2-9c6f-7ff6257eb5e7" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "Requests",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "Invites",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

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
        }
    }
}
