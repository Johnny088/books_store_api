using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace books_store_DAL.Migrations
{
    /// <inheritdoc />
    public partial class addAuthorCountryColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Authors",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Authors");
        }
    }
}
