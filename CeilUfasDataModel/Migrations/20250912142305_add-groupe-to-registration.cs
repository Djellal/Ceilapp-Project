using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataModel.Migrations
{
    /// <inheritdoc />
    public partial class addgroupetoregistration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Groupes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "CourseRegistrations",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseRegistrations_GroupId",
                table: "CourseRegistrations",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseRegistrations_Groupes_GroupId",
                table: "CourseRegistrations",
                column: "GroupId",
                principalTable: "Groupes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseRegistrations_Groupes_GroupId",
                table: "CourseRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_CourseRegistrations_GroupId",
                table: "CourseRegistrations");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Groupes");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "CourseRegistrations");
        }
    }
}
