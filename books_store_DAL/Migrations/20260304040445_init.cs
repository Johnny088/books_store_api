using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace books_store_DAL.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Image = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Image = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Rating = table.Column<float>(type: "real", nullable: false, defaultValue: 0f),
                    Pages = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    PublishedYear = table.Column<int>(type: "integer", nullable: false),
                    AuthorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    BookEntityId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Genres_Books_BookEntityId",
                        column: x => x.BookEntityId,
                        principalTable: "Books",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BookEntityGenreEntity",
                columns: table => new
                {
                    BookEntityId = table.Column<int>(type: "integer", nullable: false),
                    GenreEntityId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookEntityGenreEntity", x => new { x.BookEntityId, x.GenreEntityId });
                    table.ForeignKey(
                        name: "FK_BookEntityGenreEntity_Books_BookEntityId",
                        column: x => x.BookEntityId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookEntityGenreEntity_Genres_GenreEntityId",
                        column: x => x.GenreEntityId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "BirthDate", "Image", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(1965, 7, 31, 0, 0, 0, 0, DateTimeKind.Utc), null, "J.K. Rowling" },
                    { 2, new DateTime(1948, 9, 20, 0, 0, 0, 0, DateTimeKind.Utc), null, "George R.R. Martin" },
                    { 3, new DateTime(1892, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, "J.R.R. Tolkien" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "BookEntityId", "Name" },
                values: new object[,]
                {
                    { 1, null, "Fantasy" },
                    { 2, null, "Adventure" },
                    { 3, null, "Drama" }
                });

            migrationBuilder.InsertData(
                table: "BookEntityGenreEntity",
                columns: new[] { "BookEntityId", "GenreEntityId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 3 },
                    { 3, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookEntityGenreEntity_GenreEntityId",
                table: "BookEntityGenreEntity",
                column: "GenreEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_BookEntityId",
                table: "Genres",
                column: "BookEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookEntityGenreEntity");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
