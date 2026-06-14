using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalQuest.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Translation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "SubClass");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "Spells");

            migrationBuilder.DropColumn(
                name: "MaterialDescription",
                table: "Spells");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Spells");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "Effects");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Effects");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "ClassFeature");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "SubClass",
                newName: "NameInSRD");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Classes",
                newName: "NameInSRD");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ClassFeature",
                newName: "NameInSRD");

            migrationBuilder.AddColumn<bool>(
                name: "BasicRules",
                table: "SubClass",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SRD",
                table: "SubClass",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "SourceId",
                table: "SubClass",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "SourcePage",
                table: "SubClass",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "BasicRules",
                table: "Classes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SRD",
                table: "Classes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SourcePage",
                table: "Classes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "BasicRules",
                table: "ClassFeature",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SRD",
                table: "ClassFeature",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "SourceId",
                table: "ClassFeature",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "SourcePage",
                table: "ClassFeature",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BookTranslation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    OriginId = table.Column<Guid>(type: "uuid", nullable: false),
                    LanguageCode = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookTranslation_Books_OriginId",
                        column: x => x.OriginId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassFeatureTranslation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    OriginId = table.Column<Guid>(type: "uuid", nullable: false),
                    LanguageCode = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassFeatureTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassFeatureTranslation_ClassFeature_OriginId",
                        column: x => x.OriginId,
                        principalTable: "ClassFeature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassTranslation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    OriginId = table.Column<Guid>(type: "uuid", nullable: false),
                    LanguageCode = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassTranslation_Classes_OriginId",
                        column: x => x.OriginId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EffectTranslation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    OriginId = table.Column<Guid>(type: "uuid", nullable: false),
                    LanguageCode = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EffectTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EffectTranslation_Effects_OriginId",
                        column: x => x.OriginId,
                        principalTable: "Effects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpellTranslation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MaterialDescription = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    OriginId = table.Column<Guid>(type: "uuid", nullable: false),
                    LanguageCode = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpellTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpellTranslation_Spells_OriginId",
                        column: x => x.OriginId,
                        principalTable: "Spells",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubClassTranslation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    OriginId = table.Column<Guid>(type: "uuid", nullable: false),
                    LanguageCode = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubClassTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubClassTranslation_SubClass_OriginId",
                        column: x => x.OriginId,
                        principalTable: "SubClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubClass_SourceId",
                table: "SubClass",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassFeature_SourceId",
                table: "ClassFeature",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_BookTranslation_OriginId",
                table: "BookTranslation",
                column: "OriginId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassFeatureTranslation_OriginId",
                table: "ClassFeatureTranslation",
                column: "OriginId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassTranslation_OriginId",
                table: "ClassTranslation",
                column: "OriginId");

            migrationBuilder.CreateIndex(
                name: "IX_EffectTranslation_OriginId",
                table: "EffectTranslation",
                column: "OriginId");

            migrationBuilder.CreateIndex(
                name: "IX_SpellTranslation_OriginId",
                table: "SpellTranslation",
                column: "OriginId");

            migrationBuilder.CreateIndex(
                name: "IX_SubClassTranslation_OriginId",
                table: "SubClassTranslation",
                column: "OriginId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassFeature_Books_SourceId",
                table: "ClassFeature",
                column: "SourceId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubClass_Books_SourceId",
                table: "SubClass",
                column: "SourceId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassFeature_Books_SourceId",
                table: "ClassFeature");

            migrationBuilder.DropForeignKey(
                name: "FK_SubClass_Books_SourceId",
                table: "SubClass");

            migrationBuilder.DropTable(
                name: "BookTranslation");

            migrationBuilder.DropTable(
                name: "ClassFeatureTranslation");

            migrationBuilder.DropTable(
                name: "ClassTranslation");

            migrationBuilder.DropTable(
                name: "EffectTranslation");

            migrationBuilder.DropTable(
                name: "SpellTranslation");

            migrationBuilder.DropTable(
                name: "SubClassTranslation");

            migrationBuilder.DropIndex(
                name: "IX_SubClass_SourceId",
                table: "SubClass");

            migrationBuilder.DropIndex(
                name: "IX_ClassFeature_SourceId",
                table: "ClassFeature");

            migrationBuilder.DropColumn(
                name: "BasicRules",
                table: "SubClass");

            migrationBuilder.DropColumn(
                name: "SRD",
                table: "SubClass");

            migrationBuilder.DropColumn(
                name: "SourceId",
                table: "SubClass");

            migrationBuilder.DropColumn(
                name: "SourcePage",
                table: "SubClass");

            migrationBuilder.DropColumn(
                name: "BasicRules",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "SRD",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "SourcePage",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "BasicRules",
                table: "ClassFeature");

            migrationBuilder.DropColumn(
                name: "SRD",
                table: "ClassFeature");

            migrationBuilder.DropColumn(
                name: "SourceId",
                table: "ClassFeature");

            migrationBuilder.DropColumn(
                name: "SourcePage",
                table: "ClassFeature");

            migrationBuilder.RenameColumn(
                name: "NameInSRD",
                table: "SubClass",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "NameInSRD",
                table: "Classes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "NameInSRD",
                table: "ClassFeature",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "SubClass",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Spells",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MaterialDescription",
                table: "Spells",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Spells",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Effects",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Effects",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Classes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "ClassFeature",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Books",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Books",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
