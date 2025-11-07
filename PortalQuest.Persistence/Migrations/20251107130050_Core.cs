using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PortalQuest.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Core : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AbilityScores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbilityScores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AttackTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttackTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CastingTime",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CastingTime", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Components",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Components", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Conditions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conditions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DamageTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DamageTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Durations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Durations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Effects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Effects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MagicSchools",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MagicSchools", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ranges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ranges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Spells",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    Ritual = table.Column<bool>(type: "boolean", nullable: false),
                    Concentration = table.Column<bool>(type: "boolean", nullable: false),
                    DamageDices = table.Column<string>(type: "jsonb", nullable: false),
                    MagicSchoolId = table.Column<int>(type: "integer", nullable: false),
                    SaveId = table.Column<int>(type: "integer", nullable: true),
                    CastingTimeId = table.Column<int>(type: "integer", nullable: false),
                    DurationId = table.Column<int>(type: "integer", nullable: false),
                    DamageTypeId = table.Column<int>(type: "integer", nullable: true),
                    ConditionId = table.Column<int>(type: "integer", nullable: true),
                    RangeId = table.Column<int>(type: "integer", nullable: false),
                    AttackTypeId = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spells", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Spells_AbilityScores_SaveId",
                        column: x => x.SaveId,
                        principalTable: "AbilityScores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Spells_AttackTypes_AttackTypeId",
                        column: x => x.AttackTypeId,
                        principalTable: "AttackTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Spells_CastingTime_CastingTimeId",
                        column: x => x.CastingTimeId,
                        principalTable: "CastingTime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Spells_Conditions_ConditionId",
                        column: x => x.ConditionId,
                        principalTable: "Conditions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Spells_DamageTypes_DamageTypeId",
                        column: x => x.DamageTypeId,
                        principalTable: "DamageTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Spells_Durations_DurationId",
                        column: x => x.DurationId,
                        principalTable: "Durations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Spells_MagicSchools_MagicSchoolId",
                        column: x => x.MagicSchoolId,
                        principalTable: "MagicSchools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Spells_Ranges_RangeId",
                        column: x => x.RangeId,
                        principalTable: "Ranges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpellClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SpellId = table.Column<int>(type: "integer", nullable: false),
                    ClassId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpellClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpellClass_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpellClass_Spells_SpellId",
                        column: x => x.SpellId,
                        principalTable: "Spells",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpellTag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SpellId = table.Column<int>(type: "integer", nullable: false),
                    TagId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpellTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpellTag_Spells_SpellId",
                        column: x => x.SpellId,
                        principalTable: "Spells",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpellTag_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpellClass_ClassId",
                table: "SpellClass",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_SpellClass_SpellId",
                table: "SpellClass",
                column: "SpellId");

            migrationBuilder.CreateIndex(
                name: "IX_SpellTag_SpellId",
                table: "SpellTag",
                column: "SpellId");

            migrationBuilder.CreateIndex(
                name: "IX_SpellTag_TagId",
                table: "SpellTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Spells_AttackTypeId",
                table: "Spells",
                column: "AttackTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Spells_CastingTimeId",
                table: "Spells",
                column: "CastingTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Spells_ConditionId",
                table: "Spells",
                column: "ConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_Spells_DamageTypeId",
                table: "Spells",
                column: "DamageTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Spells_DurationId",
                table: "Spells",
                column: "DurationId");

            migrationBuilder.CreateIndex(
                name: "IX_Spells_MagicSchoolId",
                table: "Spells",
                column: "MagicSchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Spells_RangeId",
                table: "Spells",
                column: "RangeId");

            migrationBuilder.CreateIndex(
                name: "IX_Spells_SaveId",
                table: "Spells",
                column: "SaveId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Components");

            migrationBuilder.DropTable(
                name: "Effects");

            migrationBuilder.DropTable(
                name: "SpellClass");

            migrationBuilder.DropTable(
                name: "SpellTag");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Spells");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "AbilityScores");

            migrationBuilder.DropTable(
                name: "AttackTypes");

            migrationBuilder.DropTable(
                name: "CastingTime");

            migrationBuilder.DropTable(
                name: "Conditions");

            migrationBuilder.DropTable(
                name: "DamageTypes");

            migrationBuilder.DropTable(
                name: "Durations");

            migrationBuilder.DropTable(
                name: "MagicSchools");

            migrationBuilder.DropTable(
                name: "Ranges");
        }
    }
}
