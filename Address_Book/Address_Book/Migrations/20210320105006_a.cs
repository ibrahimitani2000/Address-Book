using Microsoft.EntityFrameworkCore.Migrations;

namespace Address_Book.Migrations
{
    public partial class a : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Organization_OrganizationId",
                table: "Person");

            migrationBuilder.AlterColumn<int>(
                name: "OrganizationId",
                table: "Person",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Organization_OrganizationId",
                table: "Person",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Organization_OrganizationId",
                table: "Person");

            migrationBuilder.AlterColumn<int>(
                name: "OrganizationId",
                table: "Person",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Organization_OrganizationId",
                table: "Person",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
