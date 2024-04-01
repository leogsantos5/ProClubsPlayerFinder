using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProClubsPlayerFinder.API.Migrations
{
    /// <inheritdoc />
    public partial class DeletedStatusColumnFromInvitesAndRequestsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Invites");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2ec68e2f-fa24-4abf-8bdf-d82f530adee0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4adc856b-adf3-4da9-903f-235950dca2eb", "AQAAAAIAAYagAAAAENx/LIf5nkb14C5BKC0QbtYf9AAkIcTvzSelCaB4uCRzCkyYBkh99eJoWLIAH7RBzg==", "2d7d7c02-8635-4c7d-b31a-d2c068b51200" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "71a8b6ac-6395-41d8-94d4-e103350110c0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8e862851-b26f-42b3-aebd-dcebf97a65db", "AQAAAAIAAYagAAAAEBWD0z0TororXji/55EPA9HOiM+zsrozAfdm/ZAHawrXUVxbGtQa2UyH/Id0ImmFzg==", "2f9e8157-3411-48b9-910f-82623e5becd7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7b867b03-fd4a-4410-a9fd-abdc32bda959",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d86175a9-0786-40ef-af88-59b92ca93c0b", "AQAAAAIAAYagAAAAEGsoRSSC6hLYXzCXoiYR/zZ1BG9TSI1HyYT9YFKMdTaMJTmBb3Ads0h9twoDDFq8OQ==", "e6c39fa7-f042-444a-ac76-5060e2eb1025" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Requests",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "Pending");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Invites",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "Pending");

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
    }
}
