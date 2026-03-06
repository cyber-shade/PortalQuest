using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalQuest.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class book : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Sources_SourceId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_SpellClass_Sources_SourceId",
                table: "SpellClass");

            migrationBuilder.DropForeignKey(
                name: "FK_Spells_Sources_SourceId",
                table: "Spells");

            migrationBuilder.DropTable(
                name: "Sources");

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ShortName = table.Column<string>(type: "text", nullable: false),
                    PublishedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Author = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Books_SourceId",
                table: "Classes",
                column: "SourceId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SpellClass_Books_SourceId",
                table: "SpellClass",
                column: "SourceId",
                principalTable: "Books",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Spells_Books_SourceId",
                table: "Spells",
                column: "SourceId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Books_SourceId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_SpellClass_Books_SourceId",
                table: "SpellClass");

            migrationBuilder.DropForeignKey(
                name: "FK_Spells_Books_SourceId",
                table: "Spells");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.CreateTable(
                name: "Sources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ShortName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sources", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Sources_SourceId",
                table: "Classes",
                column: "SourceId",
                principalTable: "Sources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SpellClass_Sources_SourceId",
                table: "SpellClass",
                column: "SourceId",
                principalTable: "Sources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Spells_Sources_SourceId",
                table: "Spells",
                column: "SourceId",
                principalTable: "Sources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
