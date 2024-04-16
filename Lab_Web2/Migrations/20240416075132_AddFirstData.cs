using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab_Web2.Migrations
{
    /// <inheritdoc />
    public partial class AddFirstData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Vien" });

            migrationBuilder.InsertData(
                table: "Publisher",
                columns: new[] { "PublisherId", "Name" },
                values: new object[] { 1, "Kim Dong" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "CoverURI", "DateAdded", "Description", "Genre", "PublisherId", "Title" },
                values: new object[] { 1, "/SomeUri", new DateTime(2024, 4, 16, 14, 51, 31, 995, DateTimeKind.Local).AddTicks(9732), "Samurai Manga", "Action", 1, "SamuraiX" });

            migrationBuilder.InsertData(
                table: "Book_Authors",
                columns: new[] { "AuthorId", "BookId" },
                values: new object[] { 1, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Book_Authors",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Publisher",
                keyColumn: "PublisherId",
                keyValue: 1);
        }
    }
}
