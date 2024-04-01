using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProClubsPlayerFinder.API.Migrations
{
    /// <inheritdoc />
    public partial class AddedOnDeleteCascadeToInvitesAndRequestsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invites_Clubs_Sender",
                table: "Invites");

            migrationBuilder.DropForeignKey(
                name: "FK_Invites_Players_Receiver",
                table: "Invites");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Clubs_Receiver",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Players_Sender",
                table: "Requests");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2ec68e2f-fa24-4abf-8bdf-d82f530adee0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "063d9a9f-a75b-41d0-9c69-ab07da557dba", "AQAAAAIAAYagAAAAEHgVsQbzMch/Xq0a2jKpvHjsjLMhBzEnq2wmj8hCQRAAqBVrtUe86Sv7vzioQLah5w==", "1efa8114-628a-4796-b05b-83e4142bd334" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "71a8b6ac-6395-41d8-94d4-e103350110c0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c4ee5469-cbb7-4f0d-8baa-de41d33502aa", "AQAAAAIAAYagAAAAEEuWiM14O14OkbDWyH8qGnG97tnUBI5QxP+VYnSLwjL5StmqtmsIbeC+k5yg0n2+gw==", "c96187fa-021d-4dce-8b50-8e020da6cdf0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7b867b03-fd4a-4410-a9fd-abdc32bda959",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6f39c986-2bee-4089-8563-696a971573fd", "AQAAAAIAAYagAAAAEJGZOLxKk9gxBlsqZOEzDK6gyvN5mKzZRYAnhvefE8bz9R+i674XQxH7epJGdl79Xw==", "ba58bd1a-dedc-4273-9355-8f02a283cacb" });

            migrationBuilder.AddForeignKey(
                name: "FK_Invites_Clubs_Sender",
                table: "Invites",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invites_Players_Receiver",
                table: "Invites",
                column: "ApiUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Clubs_Receiver",
                table: "Requests",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Players_Sender",
                table: "Requests",
                column: "ApiUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invites_Clubs_Sender",
                table: "Invites");

            migrationBuilder.DropForeignKey(
                name: "FK_Invites_Players_Receiver",
                table: "Invites");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Clubs_Receiver",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Players_Sender",
                table: "Requests");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Invites_Clubs_Sender",
                table: "Invites",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invites_Players_Receiver",
                table: "Invites",
                column: "ApiUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Clubs_Receiver",
                table: "Requests",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Players_Sender",
                table: "Requests",
                column: "ApiUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
