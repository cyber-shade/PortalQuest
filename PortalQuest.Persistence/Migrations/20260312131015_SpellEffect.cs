using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalQuest.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SpellEffect : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EffectSpell",
                columns: table => new
                {
                    ConditionsId = table.Column<Guid>(type: "uuid", nullable: false),
                    SpellsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EffectSpell", x => new { x.ConditionsId, x.SpellsId });
                    table.ForeignKey(
                        name: "FK_EffectSpell_Effects_ConditionsId",
                        column: x => x.ConditionsId,
                        principalTable: "Effects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EffectSpell_Spells_SpellsId",
                        column: x => x.SpellsId,
                        principalTable: "Spells",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EffectSpell_SpellsId",
                table: "EffectSpell",
                column: "SpellsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EffectSpell");
        }
    }
}
