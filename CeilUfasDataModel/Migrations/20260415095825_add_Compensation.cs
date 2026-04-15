using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataModel.Migrations
{
    /// <inheritdoc />
    public partial class add_Compensation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Compensations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CourseRegistrationId = table.Column<int>(type: "integer", nullable: false),
                    AbsenceDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AbsenceFrom = table.Column<TimeSpan>(type: "interval", nullable: false),
                    AbsenceTo = table.Column<TimeSpan>(type: "interval", nullable: false),
                    MakeupDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MakeupFrom = table.Column<TimeSpan>(type: "interval", nullable: false),
                    MakeupTo = table.Column<TimeSpan>(type: "interval", nullable: false),
                    MakeupTeacherId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compensations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Compensations_CourseRegistrations_CourseRegistrationId",
                        column: x => x.CourseRegistrationId,
                        principalTable: "CourseRegistrations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Compensations_CourseRegistrationId",
                table: "Compensations",
                column: "CourseRegistrationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Compensations");
        }
    }
}
