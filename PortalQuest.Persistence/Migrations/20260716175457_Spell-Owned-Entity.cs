using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalQuest.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SpellOwnedEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Durations_Times_TimeId",
                table: "Durations");

            migrationBuilder.DropForeignKey(
                name: "FK_Spells_Ranges_RangeId",
                table: "Spells");

            migrationBuilder.DropTable(
                name: "DurationSpell");

            migrationBuilder.DropTable(
                name: "Ranges");

            migrationBuilder.DropTable(
                name: "SpellTime");

            migrationBuilder.DropTable(
                name: "Times");

            migrationBuilder.DropIndex(
                name: "IX_Spells_RangeId",
                table: "Spells");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Durations",
                table: "Durations");

            migrationBuilder.DropIndex(
                name: "IX_Durations_TimeId",
                table: "Durations");

            migrationBuilder.DropColumn(
                name: "RangeId",
                table: "Spells");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Durations");

            migrationBuilder.DropColumn(
                name: "TimeId",
                table: "Durations");

            migrationBuilder.RenameTable(
                name: "Durations",
                newName: "SpellDurations");

            migrationBuilder.AddColumn<int>(
                name: "Range_Amount",
                table: "Spells",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Range_DistanceType",
                table: "Spells",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Range_Type",
                table: "Spells",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Ends",
                table: "SpellDurations",
                type: "text",
                nullable: false,
                oldClrType: typeof(List<string>),
                oldType: "text[]");

            migrationBuilder.AddColumn<Guid>(
                name: "SpellId",
                table: "SpellDurations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Time_Amount",
                table: "SpellDurations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Time_Condition",
                table: "SpellDurations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Time_Type",
                table: "SpellDurations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpellDurations",
                table: "SpellDurations",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "SpellCastingTimes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Condition = table.Column<string>(type: "text", nullable: false),
                    SpellId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpellCastingTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpellCastingTimes_Spells_SpellId",
                        column: x => x.SpellId,
                        principalTable: "Spells",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpellDurations_SpellId",
                table: "SpellDurations",
                column: "SpellId");

            migrationBuilder.CreateIndex(
                name: "IX_SpellCastingTimes_SpellId",
                table: "SpellCastingTimes",
                column: "SpellId");

            migrationBuilder.AddForeignKey(
                name: "FK_SpellDurations_Spells_SpellId",
                table: "SpellDurations",
                column: "SpellId",
                principalTable: "Spells",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpellDurations_Spells_SpellId",
                table: "SpellDurations");

            migrationBuilder.DropTable(
                name: "SpellCastingTimes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SpellDurations",
                table: "SpellDurations");

            migrationBuilder.DropIndex(
                name: "IX_SpellDurations_SpellId",
                table: "SpellDurations");

            migrationBuilder.DropColumn(
                name: "Range_Amount",
                table: "Spells");

            migrationBuilder.DropColumn(
                name: "Range_DistanceType",
                table: "Spells");

            migrationBuilder.DropColumn(
                name: "Range_Type",
                table: "Spells");

            migrationBuilder.DropColumn(
                name: "SpellId",
                table: "SpellDurations");

            migrationBuilder.DropColumn(
                name: "Time_Amount",
                table: "SpellDurations");

            migrationBuilder.DropColumn(
                name: "Time_Condition",
                table: "SpellDurations");

            migrationBuilder.DropColumn(
                name: "Time_Type",
                table: "SpellDurations");

            migrationBuilder.RenameTable(
                name: "SpellDurations",
                newName: "Durations");

            migrationBuilder.AddColumn<Guid>(
                name: "RangeId",
                table: "Spells",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<List<string>>(
                name: "Ends",
                table: "Durations",
                type: "text[]",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Durations",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "TimeId",
                table: "Durations",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Durations",
                table: "Durations",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "DurationSpell",
                columns: table => new
                {
                    DurationId = table.Column<Guid>(type: "uuid", nullable: false),
                    SpellsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DurationSpell", x => new { x.DurationId, x.SpellsId });
                    table.ForeignKey(
                        name: "FK_DurationSpell_Durations_DurationId",
                        column: x => x.DurationId,
                        principalTable: "Durations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DurationSpell_Spells_SpellsId",
                        column: x => x.SpellsId,
                        principalTable: "Spells",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ranges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    DistanceType = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ranges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Times",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    Condition = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Times", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpellTime",
                columns: table => new
                {
                    CastingTimeId = table.Column<Guid>(type: "uuid", nullable: false),
                    SpellsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpellTime", x => new { x.CastingTimeId, x.SpellsId });
                    table.ForeignKey(
                        name: "FK_SpellTime_Spells_SpellsId",
                        column: x => x.SpellsId,
                        principalTable: "Spells",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpellTime_Times_CastingTimeId",
                        column: x => x.CastingTimeId,
                        principalTable: "Times",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Spells_RangeId",
                table: "Spells",
                column: "RangeId");

            migrationBuilder.CreateIndex(
                name: "IX_Durations_TimeId",
                table: "Durations",
                column: "TimeId");

            migrationBuilder.CreateIndex(
                name: "IX_DurationSpell_SpellsId",
                table: "DurationSpell",
                column: "SpellsId");

            migrationBuilder.CreateIndex(
                name: "IX_SpellTime_SpellsId",
                table: "SpellTime",
                column: "SpellsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Durations_Times_TimeId",
                table: "Durations",
                column: "TimeId",
                principalTable: "Times",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Spells_Ranges_RangeId",
                table: "Spells",
                column: "RangeId",
                principalTable: "Ranges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
