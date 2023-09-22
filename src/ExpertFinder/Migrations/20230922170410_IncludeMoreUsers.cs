using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExpertFinder.Migrations
{
    /// <inheritdoc />
    public partial class IncludeMoreUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FullName" },
                values: new object[,]
                {
                    { new Guid("20b18172-f06b-454b-bd48-3c5573fc4a1c"), "Lucia Conde Moreno" },
                    { new Guid("589ee3c2-4e59-48fb-a5ca-24dd5960d30a"), "Rowan Terinathe" },
                    { new Guid("bef98bd4-795c-46b7-9295-427de33cb6a6"), "Emiel Stoelinga" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("20b18172-f06b-454b-bd48-3c5573fc4a1c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("589ee3c2-4e59-48fb-a5ca-24dd5960d30a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bef98bd4-795c-46b7-9295-427de33cb6a6"));
        }
    }
}
